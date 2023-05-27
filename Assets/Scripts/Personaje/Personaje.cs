using UnityEngine;

public class Personaje : MonoBehaviour
{
    [SerializeField] private PersonajeStats stats;
    public PersonajeVida PersonajeVida { get; private set; }
    public PersonajeAnimaciones PersonajeAnimaciones { get; private set; }
    public PersonajeMana PersonajeMana { get; private set; }


    private void Awake()
    {
        PersonajeVida = GetComponent<PersonajeVida>();
        PersonajeAnimaciones = GetComponent<PersonajeAnimaciones>();
        PersonajeMana = GetComponent<PersonajeMana>();
    }

    private void OnEnable()
    {
        AtributoButton.EventoAgregarAtributo += AtributoRespuesta;
    }

    private void OnDisable()
    {
        AtributoButton.EventoAgregarAtributo -= AtributoRespuesta;
    }

    public void RestaurarPersonaje()
    {
        PersonajeVida.RestaurarPersonaje();
        PersonajeAnimaciones.RevivirPersonaje();
        PersonajeMana.RestablecerMana();
    }

    private void AtributoRespuesta(TipoAtributo tipo)
    {
        if (stats.PuntosDisponibles <= 0)
        {
            return;
        }

        switch (tipo)
        {
            case TipoAtributo.Fuerza:
                stats.Fuerza++;
                stats.A�adirBonudPorAtributoFuerza();
                break;
            case TipoAtributo.Inteligencia:
                stats.Inteligencia++;
                stats.A�adirBonusPorAtributoInteligencia();
                break;
            case TipoAtributo.Destreza:
                stats.Destreza++;
                stats.A�adirBonusPorAtributoDestreza();
                break;
        }

        stats.PuntosDisponibles -= 1;
    }
}
