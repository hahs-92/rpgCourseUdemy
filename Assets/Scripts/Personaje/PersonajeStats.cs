using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats")]
public class PersonajeStats : ScriptableObject
{
    [Header("Stats")]
    public float Daño = 5f;
    public float Defensa = 2f;
    public float Velocidad = 5f;
    public float Nivel;
    public float ExpActual;
    public float ExpRequeridaSiguienteNivel;
    [Range(0f, 100f)] public float PorcentajeCritico;
    [Range(0f, 100f)] public float PorcentajeBloqueo;
}
