using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioUI : Singleton<InventarioUI>
{
    [SerializeField] private InventarioSlot slotPrefab;
    [SerializeField] private Transform contenedor;

    private List<InventarioSlot> slotsDisponibles = new List<InventarioSlot>();


    private void Start()
    {
        InicializarInventario();
    }


    private void InicializarInventario()
    {
        for (int i = 0; i < Inventario.Instance.NumeroDeSlots; i++)
        {
            InventarioSlot nuevoSlot = Instantiate(slotPrefab, contenedor);
            nuevoSlot.Index = i;
            slotsDisponibles.Add(nuevoSlot);
        }
    }

    public void DibujarItemEnInventario(InventarioItem itemPorAñadir, int cantidad, int itemIndex)
    {
        InventarioSlot slot = slotsDisponibles[itemIndex];
        if (itemPorAñadir != null)
        {
            slot.ActivarSlotUI(true);
            slot.ActualizarSlot(itemPorAñadir, cantidad);
        }
        else
        {
            slot.ActivarSlotUI(false);
        }
    }
}
