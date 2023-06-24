using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{
    [Header("Stas")]
    [SerializeField] private PersonajeStats stats;

    [Header("Config")]
    [SerializeField] private int nivelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;

    private float expActual;
    //private float expActualTemp;
    private float expRequeridaSiguienteNivel;

    private void Start()
    {
        stats.Nivel = 1;
        expRequeridaSiguienteNivel = expBase;
        stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
        ActualizarBarraExp();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AñadirExperiencia(2f);
        }
    }

    public void AñadirExperiencia(float expObtenida)
    {
        if (expObtenida <= 0) return;

        expActual += expObtenida;
        stats.ExpActual = expActual;

        if(expActual == expRequeridaSiguienteNivel)
        {
            ActualizarNivel();
        } else if(expActual > expRequeridaSiguienteNivel) 
        {
            float diferencia = expActual - expRequeridaSiguienteNivel;
            ActualizarNivel();
            AñadirExperiencia(diferencia);
        }

        stats.ExpTotal += expObtenida;
        ActualizarBarraExp();

        //if (expObtenida > 0f) // 10exp
        //{
        //    float expRestanteNuevoNivel = expRequeridaSiguienteNivel - expActualTemp; // 8 - 4 = 4
        //    if (expObtenida >= expRestanteNuevoNivel) // subir nivel
        //    {
        //        expObtenida -= expRestanteNuevoNivel; // 6exp
        //        expActual += expObtenida;
        //        ActualizarNivel();
        //        AñadirExperiencia(expObtenida);
        //    }
        //    else
        //    {
        //        expActual += expObtenida;
        //        expActualTemp += expObtenida;
        //        if (expActualTemp == expRequeridaSiguienteNivel)
        //        {
        //            ActualizarNivel();
        //        }
        //    }
        //}

        //stats.ExpActual = expActual;
        //ActualizarBarraExp();
    }

    private void ActualizarNivel()
    {
        if (stats.Nivel < nivelMax)
        {
            stats.Nivel++;
            stats.ExpActual = 0;
            expActual = 0;
            //expActualTemp = 0f;
            expRequeridaSiguienteNivel *= valorIncremental;
            stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
            stats.PuntosDisponibles += 3;
        }
    }

    private void ActualizarBarraExp()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActual, expRequeridaSiguienteNivel);
    }


    private void RespuestaEnemigoDerrotado(float exp)
    {
        AñadirExperiencia(exp);
    }

    private void OnEnable()
    {
        EnemigoVida.EventoEnemigoDerrotado += RespuestaEnemigoDerrotado;
    }

    private void OnDisable()
    {
        EnemigoVida.EventoEnemigoDerrotado -= RespuestaEnemigoDerrotado;
    }
}
