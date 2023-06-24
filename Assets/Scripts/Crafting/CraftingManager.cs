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
    [SerializeField] private Button buttonCraftear;

    [Header("Receta Item Resultado")]
    [SerializeField] private Image itemResultadoIcono;
    [SerializeField] private TextMeshProUGUI itemResulatdoNombre;
    [SerializeField] private TextMeshProUGUI itemResulatdoDescripcion;

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

        if(SetPuedeCraftear(receta))
        {
            recetaMensage.text = "Receta Dsiaponible";
            buttonCraftear.interactable= true;
        } else
        {
            recetaMensage.text = "Necesitas mas materiales";
            buttonCraftear.interactable = false;
        }

        itemResultadoIcono.sprite = receta.ItemResultado.Icono;
        itemResulatdoNombre.text = receta.ItemResultado.Nombre;
        itemResulatdoDescripcion.text = receta.ItemResultado.DescripcionItemCrafting();
    }

    public bool SetPuedeCraftear(Receta receta)
    {
        if(
            Inventario.Instance.ObtenerCantidadItems(receta.Item1.ID)  >= receta.Item1CantidadRequerida &&
            Inventario.Instance.ObtenerCantidadItems(receta.Item2.ID) >= receta.Item2CantidadRequerida
         )
        {
            return true;
        }

        return false;
    }

    public void Craftear()
    {
        for(int i = 0; i < RecetaSelecionada.Item1CantidadRequerida; i++)
        {
            Inventario.Instance.ConsumirItem(RecetaSelecionada.Item1.ID);
        }    
        
        for(int i = 0; i < RecetaSelecionada.Item2CantidadRequerida; i++)
        {
            Inventario.Instance.ConsumirItem(RecetaSelecionada.Item2.ID);
        }

        Inventario.Instance.AñadirItem(RecetaSelecionada.ItemResultado, RecetaSelecionada.ItemResultadoCantidad);
        MostarReceta(RecetaSelecionada);
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
