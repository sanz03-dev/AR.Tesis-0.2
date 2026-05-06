using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizLoader : MonoBehaviour
{
    public TextAsset jsonFile;

    private List<Question> filteredQuestions = new List<Question>();


    public void LoadQuestions()
    {
        if (jsonFile == null)
        {
            Debug.LogError("Error: No has asignado el JSON en el QuizLoader.");
            return;
        }

        // Convertimos el JSON a objetos de C#
        QuestionList data = JsonUtility.FromJson<QuestionList>(jsonFile.text);
        
        // Limpiamos la lista anterior para no acumular preguntas viejas
        filteredQuestions.Clear();

        // Filtramos según lo que el usuario seleccionó en los botones
        foreach (Question q in data.questions)
        {
            if (q.testType == QuizManager.instance.testType)
            {
                if (q.testType == "figuras")
                {
                    if (q.figure == QuizManager.instance.selectedFigure &&
                        q.difficulty == QuizManager.instance.difficulty)
                    {
                        filteredQuestions.Add(q);
                    }
                }
                else
                {
                    // Para otros tipos (cognición/percepción)
                    filteredQuestions.Add(q);
                }
            }
        }

        Debug.Log("Filtro finalizado. Preguntas encontradas: " + filteredQuestions.Count);
    }

    public List<Question> GetQuestions()
    {
        return filteredQuestions;
    }
}
