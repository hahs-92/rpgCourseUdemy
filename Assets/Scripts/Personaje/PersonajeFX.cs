using System;
using System.Collections;
using UnityEngine;

public enum TipoPersonaje
{
    Player,
    IA
}

public class PersonajeFX : MonoBehaviour
{
    [SerializeField] private GameObject canvasTextoAnimacionPrefab;
    [SerializeField] private Transform canvasTextoPosicion;

    [SerializeField] private ObjectPooler pooler;
    //private ObjectPooler pooler;

    [Header("Tipo")]
    [SerializeField] private TipoPersonaje tipoPersonaje;

    private void Awake()
    {
        //pooler = GetComponent<ObjectPooler>();
    }

    private void Start()
    {
        pooler.CrearPooler(canvasTextoAnimacionPrefab);
    }

    private IEnumerator IEMostrarTexto(float cantidad, Color color)
    {
        GameObject nuevoTextoGO = pooler.ObtenerInstancia();
        TextoAnimacion texto = nuevoTextoGO.GetComponent<TextoAnimacion>();
        texto.EstablecerTexto(cantidad, color);
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion);
        nuevoTextoGO.transform.position = canvasTextoPosicion.position;
        nuevoTextoGO.SetActive(true);

        yield return new WaitForSeconds(1f);
        nuevoTextoGO.SetActive(false);
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform);
    }

    private void RespuestaDa�oRecibidoHaciaPlayer(float da�o)
    {
        if (tipoPersonaje == TipoPersonaje.Player)
        {
            StartCoroutine(IEMostrarTexto(da�o, Color.black));
        }
    }

    private void RespuestaDa�oHaciaEnemigo(float da�o)
    {
        if (tipoPersonaje == TipoPersonaje.IA)
        {
            StartCoroutine(IEMostrarTexto(da�o, Color.red));
        }
    }

    private void OnEnable()
    {
        IAController.EventoDa�oRealizado += RespuestaDa�oRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDa�ado += RespuestaDa�oHaciaEnemigo;
    }

    private void OnDisable()
    {
        IAController.EventoDa�oRealizado -= RespuestaDa�oRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDa�ado -= RespuestaDa�oHaciaEnemigo;
    }
}