using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeAnimaciones : MonoBehaviour
{
    private Animator _animator;
    private PersonajeMovimiento _personajeMovimiento;
    private readonly int direccionX = Animator.StringToHash("x");
    private readonly int direccionY = Animator.StringToHash("y");
  

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _personajeMovimiento = GetComponent<PersonajeMovimiento>();
    }

    private void Update()
    {
        // cuando nos dejemos de mover, se quedara la ultima animacion aplicada
        // y no volveremos a la animacion inicial
        if (!_personajeMovimiento.EnMovimiento)
        {
            return;
        }

        _animator.SetFloat(direccionX, _personajeMovimiento.DireccionMovimiento.x);
        _animator.SetFloat(direccionY, _personajeMovimiento.DireccionMovimiento.y);
    }
}
