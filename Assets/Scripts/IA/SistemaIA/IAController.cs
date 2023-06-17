using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum TiposDeAtaque
{
    Melee,
    Embestida
}

public class IAController : MonoBehaviour
{
    public static Action<float> EventoDañoRealizado;

    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Estados")]
    [SerializeField] private IAEstado estadoInicial;
    [SerializeField] private IAEstado estadoDefault;

    [Header("Config")]
    [SerializeField] private float rangoDeAtaque;
    [SerializeField] private float rangoDeteccion;
    [SerializeField] private float rangoDeEmbestida;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadDeEmbestida;
    [SerializeField] private LayerMask personajeLayerMask;

    [Header("Ataque")]
    [SerializeField] private float daño;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private TiposDeAtaque tipoAtaque;

    [Header("Debug")]
    [SerializeField] private bool mostrarDeteccion;
    [SerializeField] private bool mostrarRangoAtaque;
    [SerializeField] private bool mostrarRangoEmbestida;

    private BoxCollider2D _boxCollider2D;

    private float tiempoParaSiguienteAtaque;
    public Transform PersonajeReferencia { get; set; }
    public IAEstado EstadoActual { get; set; }
    public EnemigoMovimiento EnemigoMovimiento { get; set; }


    public LayerMask PersonajeLayerMask => personajeLayerMask;
    public float RangoDeteccion => rangoDeteccion;
    public float RangoDeAtaqueDeterminado => tipoAtaque == TiposDeAtaque.Embestida ? rangoDeEmbestida : rangoDeAtaque;
    public float VelocidadMovimiento => velocidadMovimiento;
    public float Daño => daño;
    public TiposDeAtaque TipoAtaque => tipoAtaque;


    private void Awake()
    {
        EnemigoMovimiento = GetComponent<EnemigoMovimiento>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        EstadoActual = estadoInicial;
    }

    private void Update()
    {
        EstadoActual.EjecutarEstado(this);
    }

    public void CambiarEstado(IAEstado nuevoEstado)
    {
        if (nuevoEstado != estadoDefault)
        {
            EstadoActual = nuevoEstado;
        }
    }

    public void ActualizarTiempoEntreAtaques()
    {
        tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques;
    }

    public bool EsTiempoDeAtacar()
    {
        if (Time.time > tiempoParaSiguienteAtaque)
        {
            return true;
        }

        return false;
    }

    public bool PersonajeEnRangoDeAtaque(float rango)
    {
        float distanciaHaciaPersonaje = (PersonajeReferencia.position - transform.position).sqrMagnitude;
        if (distanciaHaciaPersonaje < Mathf.Pow(rango, 2))
        {
            return true;
        }

        return false;
    }

    public void AplicarDañoAlPersonaje(float cantidad)
    {
        float dañoPorRealizar = 0;
        if (Random.value < stats.PorcentajeBloqueo / 100)
        {
            return;
        }

        dañoPorRealizar = Mathf.Max(cantidad - stats.Defensa, 1f);
        PersonajeReferencia.GetComponent<PersonajeVida>().RecibirDaño(dañoPorRealizar);
        EventoDañoRealizado?.Invoke(dañoPorRealizar);
    }

    public void AtaqueMelee(float cantidad)
    {
        if (PersonajeReferencia != null)
        {
            AplicarDañoAlPersonaje(cantidad);
        }
    }

    public void AtaqueEmbestida(float cantidad)
    {
        if(PersonajeReferencia != null)
        {
            StartCoroutine(IEEmbestida(cantidad));
        }
    }

    private IEnumerator IEEmbestida(float cantidad)
    {
        Vector3 personajePosicion = PersonajeReferencia.position;
        Vector3 posicionInicial = transform.position;
        Vector3 direccionHaciaPersonaje = (personajePosicion - posicionInicial).normalized;
        Vector3 posicionDeAtaque = personajePosicion - direccionHaciaPersonaje * 0.5f;
        _boxCollider2D.enabled = false;

        float transicionDeAtaque = 0f;
        while (transicionDeAtaque <= 1f)
        {
            transicionDeAtaque += Time.deltaTime * velocidadDeEmbestida;
            float interpolacion = (-Mathf.Pow(transicionDeAtaque, 2) + transicionDeAtaque) * 4f;
            transform.position = Vector3.Lerp(posicionInicial, posicionDeAtaque, interpolacion);

            yield return null;
        }

        if (PersonajeReferencia != null)
        {
            AplicarDañoAlPersonaje(cantidad);
        }

        _boxCollider2D.enabled = true;
    }

    private void OnDrawGizmos()
    {
        if (mostrarDeteccion)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
        }

        if (mostrarRangoAtaque)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, rangoDeAtaque);
        }

        if (mostrarRangoEmbestida)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, rangoDeEmbestida);
        }
    }
}
