using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizFeedbackUI : MonoBehaviour
{
    [Header("Textos (Obligatorio arrastrar)")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI scoreText;

    [Header("Sistema")]
    public QuizUI quizUI;
    public GameObject profileDetailsPanel;
    public ProfileDetailsUI profileDetailsUI;

    private CanvasGroup cg;
    private string currentProfile;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        if (cg == null) cg = gameObject.AddComponent<CanvasGroup>();
        
        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void ShowFeedback()
    {

        gameObject.SetActive(true);
        if (!gameObject.activeInHierarchy)
        {
            
            Transform t = transform;
            while (t != null) { t.gameObject.SetActive(true); t = t.parent; }
        }

        cg.alpha = 1f;
        cg.interactable = true;
        cg.blocksRaycasts = true;

        RectTransform rt = GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
            rt.sizeDelta = Vector2.zero;
            rt.anchoredPosition = Vector2.zero;
            LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
        }

        if (quizUI == null) quizUI = FindObjectOfType<QuizUI>();
        if (quizUI == null || QuizManager.instance == null)
        {
            Debug.LogError("[FEEDBACK] ❌ Faltan QuizUI o QuizManager en escena.");
            return;
        }
        if (titleText == null || resultText == null || messageText == null || scoreText == null)
        {
            Debug.LogError("[FEEDBACK] ❌ Arrastra los 4 TextMeshPro a los campos del Inspector.");
            return;
        }


        if (QuizManager.instance.testType == "percepcion")
        {
            int score = quizUI.GetTotalScore();
            currentProfile = GetProfile(score);
            titleText.text = "Resultado del perfil";
            resultText.text = $"Has obtenido {score} puntos";
            messageText.text = GetMainMessage(score);
            scoreText.text = "Escala: 12 - 60";
        }
        else
        {
            int score = quizUI.GetScore();
            int total = quizUI.GetTotalQuestions();
            if (total <= 0) return;

            titleText.text = score >= 6 ? "¡Enhorabuena!" : "¡Sigue intentándolo!";
            resultText.text = $"Has respondido {score} de {total} preguntas correctas";
            messageText.text = score >= 6 ? "Sigue así, eres un experto en ambigüedad perceptiva" : "No te desanimes, casi lo tienes";
            scoreText.text = $"Puntuación: {Mathf.RoundToInt((float)score / total * 100)}%";
        }


        titleText.ForceMeshUpdate(true);
        resultText.ForceMeshUpdate(true);
        messageText.ForceMeshUpdate(true);
        scoreText.ForceMeshUpdate(true);
        Canvas.ForceUpdateCanvases();

        Debug.Log("✅ [FEEDBACK] UI lista. Renderizado síncrono completado.");
    }

    public void HideFeedback()
    {
        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    string GetProfile(int score)
    {
        if (score <= 24) return "Analítico–Secuencial";
        if (score <= 36) return "Mixto o Adaptativo";
        if (score <= 48) return "Visual–Espacial";
        return "Intuitivo–Visual Avanzado";
    }

    string GetMainMessage(int score)
    {
        if (score <= 24) return "Tiendes a analizar paso a paso la información visual.";
        if (score <= 36) return "Tienes un equilibrio entre análisis e intuición.";
        if (score <= 48) return "Posees buena capacidad visual–espacial.";
        return "Tu intuición visual está altamente desarrollada.";
    }

    public void OnClickContinue()
    {
        if (AudioManager.instance != null) AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        HideFeedback();

        if (QuizManager.instance != null && QuizManager.instance.testType == "percepcion")
        {
            currentProfile = GetProfile(quizUI.GetTotalScore());
            if (profileDetailsUI != null) profileDetailsUI.ShowDetails(currentProfile);
            if (profileDetailsPanel != null) profileDetailsPanel.SetActive(true);
        }
        else if (QuizManager.instance != null && QuizManager.instance.testType == "cognicion")
        {
            QuizManager.instance.ShowTestMain();
        }
        else
        {
            if (QuizManager.instance != null) QuizManager.instance.ShowDifficultySelect();
        }
    }
}