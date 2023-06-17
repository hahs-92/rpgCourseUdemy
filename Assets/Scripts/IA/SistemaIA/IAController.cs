using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour
{
    [Header("Estados")]
    [SerializeField] private IAEstado estadoInicial;
    [SerializeField] private IAEstado estadoDefault;

    [Header("Config")]
    [SerializeField] private float rangoDeteccion;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private LayerMask personajeLayerMask;

    [Header("Debug")]
    [SerializeField] private bool mostrarDeteccion;

    public Transform PersonajeReferencia { get; set; }
    public IAEstado EstadoActual { get; set; }
    public EnemigoMovimiento EnemigoMovimiento { get; set; }


    public LayerMask PersonajeLayerMask => personajeLayerMask;
    public float RangoDeteccion => rangoDeteccion;
    public float VelocidadMovimiento => velocidadMovimiento;

    private void Awake()
    {
        EnemigoMovimiento = GetComponent<EnemigoMovimiento>();
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

    private void OnDrawGizmos()
    {
        if (mostrarDeteccion)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
        }
    }
}
