using UnityEngine;

public class PersonajeAnimaciones : MonoBehaviour
{
    [SerializeField] private string layerIdle;
    [SerializeField] private string layerCaminar;
    [SerializeField] private string layerAtacar;

    private Animator _animator;
    private PersonajeMovimiento _personajeMovimiento;
    private PersonajeAtaque _personajeAtaque;
    private readonly int direccionX = Animator.StringToHash("x");
    private readonly int direccionY = Animator.StringToHash("y");
    private readonly int derrotado = Animator.StringToHash("Derrotado");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _personajeMovimiento = GetComponent<PersonajeMovimiento>();
        _personajeAtaque = GetComponent<PersonajeAtaque>();
    }

    private void Update()
    {
        ActualizarLayers();

        // cuando nos dejemos de mover, se quedara la ultima animacion aplicada
        // y no volveremos a la animacion inicial
        if (!_personajeMovimiento.EnMovimiento)
        {
            return;
        }

        _animator.SetFloat(direccionX, _personajeMovimiento.DireccionMovimiento.x);
        _animator.SetFloat(direccionY, _personajeMovimiento.DireccionMovimiento.y);
    }

    private void OnEnable()
    {
        PersonajeVida.EventoPersonajeDerrotado += PersonajeDerrotadoRespuesta;
    }

    private void OnDisable()
    {
        PersonajeVida.EventoPersonajeDerrotado -= PersonajeDerrotadoRespuesta;
    }

    public void RevivirPersonaje()
    {
        ActivarLayer(layerIdle);
        _animator.SetBool(derrotado, false);
    }

    private void ActualizarLayers()
    {
        if(_personajeAtaque.Atacando)
        {
            ActivarLayer(layerAtacar);
        } else if (_personajeMovimiento.EnMovimiento)
        {
            ActivarLayer(layerCaminar);
        }
        else
        {
            ActivarLayer(layerIdle);
        }
    }

    private void ActivarLayer(string nombreLayer)
    {
        for (int i = 0; i < _animator.layerCount; i++)
        {
            // estamos desactivando los leyers (0 => desactivado, 1 => activado)
            _animator.SetLayerWeight(i, 0);
        }
        // activamos el layer 
        _animator.SetLayerWeight(_animator.GetLayerIndex(nombreLayer), 1);
    }

    private void PersonajeDerrotadoRespuesta()
    {
        if (_animator.GetLayerWeight(_animator.GetLayerIndex(layerIdle)) == 1)
        {
            _animator.SetBool(derrotado, true);
        } else
        {
            ActivarLayer(layerIdle);
            _animator.SetBool(derrotado, true);
        }
    }
}
