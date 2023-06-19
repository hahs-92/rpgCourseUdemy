using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    [Header("Config")]
    [SerializeField] private GameObject panelLoot;
    //[SerializeField] private LootButton lootButtonPrefab;
    //[SerializeField] private Transform lootContenedor;

    public void MostrarLoot()
    {
        panelLoot.SetActive(true);
        //if (ContenedorOcupado())
        //{
        //    foreach (Transform hijo in lootContenedor.transform)
        //    {
        //        Destroy(hijo.gameObject);
        //    }
        //}

        //for (int i = 0; i < enemigoLoot.LootSeleccionado.Count; i++)
        //{
        //    CargarLootPanel(enemigoLoot.LootSeleccionado[i]);
        //}
    }
}
