using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecetaTarjeta : MonoBehaviour
{
    [SerializeField] private Image recetaIcono;
    [SerializeField] private TextMeshProUGUI recetaNombre;

    public Receta RecetaCargada {  get; private set; }

    public void ConfigurarRecetaTarjeta(Receta receta)
    {
        RecetaCargada= receta;
        recetaIcono.sprite = receta.ItemResultado.Icono;
        recetaNombre.text = receta.ItemResultado.Nombre;
    }
}
