using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Quests")]
    [SerializeField] private Quest[] questDisponibles;


    [Header("Inspector Quests")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContenedor;

    [Header("Personaje Quests")]
    [SerializeField] private PersonajeQuestDescripcion personajeQuestPrefab;
    [SerializeField] private Transform personajeQuestContenedor;


    private void Start()
    {
        CargarQuestEnInspector();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            AñadirProgreso("Mata10", 1);
            AñadirProgreso("Mata25", 1);
            AñadirProgreso("Mata50", 1);
        }
    }

    public void AñadirQuest(Quest questPorCompletar)
    {
        AñadirQuestPorCompletar(questPorCompletar);
    }

    public void AñadirProgreso(string questID, int cantidad)
    {
        Quest questPorActualizar = QuestExiste(questID);
        questPorActualizar.AñadirProgreso(cantidad);
    }

    private void CargarQuestEnInspector()
    {
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            InspectorQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrefab, inspectorQuestContenedor);
            nuevoQuest.ConfigurarQuestUI(questDisponibles[i]);
        }
    }

    private void AñadirQuestPorCompletar(Quest questPorCompletar)
    {
        PersonajeQuestDescripcion nuevoQuest = Instantiate(personajeQuestPrefab, personajeQuestContenedor);
        nuevoQuest.ConfigurarQuestUI(questPorCompletar);
    }

    private Quest QuestExiste(string questID)
    {
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            if (questDisponibles[i].ID == questID)
            {
                return questDisponibles[i];
            }
        }

        return null;
    }

}
