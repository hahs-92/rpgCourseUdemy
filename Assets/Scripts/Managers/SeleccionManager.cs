using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionManager : MonoBehaviour
{
    public static Action<EnemigoInteraccion> EventoEnemigoSeleccionado;
    public static Action EventoObjetoNoSeleccionado;

    private Camera camara;
    public EnemigoInteraccion EnemigoSeleccionado { get; set; }


    private void Start()
    {
        camara = Camera.main;
    }

    private void Update()
    {
        SeleccionarEnemigo();
    }

    private void SeleccionarEnemigo()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(camara.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemigo"));
            if (hit.collider != null)
            {
                EnemigoSeleccionado = hit.collider.GetComponent<EnemigoInteraccion>();
                EventoEnemigoSeleccionado?.Invoke(EnemigoSeleccionado);
            }
            else
            {
                EventoObjetoNoSeleccionado?.Invoke();
            }
        }
    }
}
