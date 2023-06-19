using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTienda : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemCosto;
    [SerializeField] private TextMeshProUGUI cantidadPorComprar;

    public ItemVenta ItemCargado { get; private set; }

    private int cantidad;
    private int costoInicial;
    private int costoActual;


    private void Update()
    {
        cantidadPorComprar.text = cantidad.ToString();
        itemCosto.text = costoActual.ToString();
    }

    public void ConfigurarItemVenta(ItemVenta itemVenta)
    {
        ItemCargado = itemVenta;
        itemIcono.sprite = itemVenta.Item.Icono;
        itemNombre.text = itemVenta.Item.Nombre;
        itemCosto.text = itemVenta.Costo.ToString();
        cantidad = 1;
        costoInicial = itemVenta.Costo;
        costoActual = itemVenta.Costo;
    }
}
