using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextoAnimacion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI da�oTexto;

    public void EstablecerTexto(float cantidad) 
    {
        da�oTexto.text = cantidad.ToString();
    }
}
