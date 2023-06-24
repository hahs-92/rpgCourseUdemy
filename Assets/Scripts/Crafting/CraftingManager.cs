using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : Singleton<CraftingManager>
{
    [Header("Config")]
    [SerializeField] private RecetaTarjeta recetaTarjetaPrefab;
    [SerializeField] private Transform recetaContendor;

    [Header("Recetas")]
    [SerializeField] private RecetaLista recetas;

    private void Start()
    {
        CargarRecetas();
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
