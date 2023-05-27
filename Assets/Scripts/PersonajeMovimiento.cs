using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMovimiento : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private Rigidbody2D _rb;
    private PersonajeVida _personajeVida;
    private Vector2 _input;
    private Vector2 _direccionMovimiento;

    public Vector2 DireccionMovimiento => _direccionMovimiento;
    public bool EnMovimiento => _direccionMovimiento.magnitude > 0f;
    

    private void Awake()
    {
        _rb= GetComponent<Rigidbody2D>();
        _personajeVida = GetComponent<PersonajeVida>();
    }
   
    void Update()
    {
        if(_personajeVida.Derrotado)
        {
            _direccionMovimiento = Vector2.zero;
            return;
        }

        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (_input.x > 0.1f)
        {
            _direccionMovimiento.x = 1f;
        }
        else if (_input.x < 0f)
        {
            _direccionMovimiento.x = -1f;
        } else
        {
            _direccionMovimiento.x = 0f;
        }

        if (_input.y > 0.1f)
        {
            _direccionMovimiento.y = 1f;
        }
        else if (_input.y < 0f)
        {
            _direccionMovimiento.y = -1f;
        } else
        {
            _direccionMovimiento.y = 0f;
        }
    }

    private void FixedUpdate()
    {
        // normalized lo utilizamos para evitar que cuando el player se mueva en vertical
        // su velocidad sea mayor
        _rb.MovePosition(_rb.position + _direccionMovimiento.normalized * velocidad * Time.fixedDeltaTime);
    }
}
