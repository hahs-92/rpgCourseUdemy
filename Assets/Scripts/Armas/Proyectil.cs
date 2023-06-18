using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float velocidad;

    public PersonajeAtaque PersonajeAtaque { get; private set; }

    private Rigidbody2D _rigidbody2D;
    private Vector2 direccion;
    private EnemigoInteraccion enemigoObjetivo;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (enemigoObjetivo == null)
        {
            return;
        }

        MoverProyectil();
    }

    public void InicializarProyectil(PersonajeAtaque ataque)
    {
        PersonajeAtaque = ataque;
        enemigoObjetivo = ataque.EnemigoObjetivo;
    }

    private void MoverProyectil()
    {
        direccion = enemigoObjetivo.transform.position - transform.position;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
        _rigidbody2D.MovePosition(_rigidbody2D.position + direccion.normalized * velocidad * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            //float da�o = PersonajeAtaque.ObtenerDa�o();
            //enemigoObjetivo.GetComponent<EnemigoVida>().RecibirDa�o(da�o);
            //PersonajeAtaque.EventoEnemigoDa�ado?.Invoke(da�o);
            gameObject.SetActive(false);
        }
    }
}
