using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : Singleton<CraftingManager>
{
    [Header("Config")]
    [SerializeField] private RecetaTarjeta recetaTarjetaPrefab;
    [SerializeField] private Transform recetaContendor;

    [Header("Receta Info")]
    [SerializeField] private Image primerMaterialIcono;
    [SerializeField] private Image segundoMaterialIcono;
    [SerializeField] private TextMeshProUGUI primerMaterialNombre;
    [SerializeField] private TextMeshProUGUI segundoMaterialNombre;
    [SerializeField] private TextMeshProUGUI primerMaterialCantidad;
    [SerializeField] private TextMeshProUGUI segundoMaterialCantidad;
    [SerializeField] private TextMeshProUGUI recetaMensage;
    [SerializeField] private Button buttonCraftiar;

    [Header("Recetas")]
    [SerializeField] private RecetaLista recetas;

    public Receta RecetaSelecionada { get; set; }


    private void Start()
    {
        CargarRecetas();
    }

    public void MostarReceta(Receta receta)
    {
        RecetaSelecionada = receta;
        primerMaterialIcono.sprite = receta.Item1.Icono;
        segundoMaterialIcono.sprite = receta.Item2.Icono;

        primerMaterialNombre.text = receta.Item1.Nombre;
        segundoMaterialNombre.text = receta.Item2.Nombre;

        primerMaterialCantidad.text = $"{Inventario.Instance.ObtenerCantidadItems(receta.Item1.ID)}/{receta.Item1CantidadRequerida}";
        segundoMaterialCantidad.text = $"{Inventario.Instance.ObtenerCantidadItems(receta.Item2.ID)}/{receta.Item2CantidadRequerida}";

    }


    private void CargarRecetas()
    {
        for(int i = 0; i < recetas.recetas.Length; i++)
        {
            RecetaTarjeta receta = Instantiate(recetaTarjetaPrefab, recetaContendor);
            receta.ConfigurarRecetaTarjeta(recetas.recetas[i]);
        }
    }
}
