using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMovimiento : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _input;
    private Vector2 _direccionMovimiento;
    [SerializeField] private float velocidad;


    private void Awake()
    {
        _rb= GetComponent<Rigidbody2D>();
    }
   
    void Update()
    {
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
