using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public static QuizManager instance;

    [Header("Referencias")]
    public QuizLoader quizLoader;

    public string testType;
    public string selectedFigure;
    public string difficulty;

    public GameObject ProfileDetails; 
    public GameObject Feedback;
    public GameObject TestMain;

    // EVENTOS
    public event Action OnTestMain;
    public event Action OnFigureSelect;
    public event Action OnDifficultySelect;
    public event Action OnInstructions;
    public event Action OnTestItems;
    public event Action OnFeedback;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        ShowTestMain();
    }

    // =========================
    // MÉTODOS (NAVEGACIÓN)
    // =========================

    public void ShowTestMain()
    {

        // Apaga los paneles manualmente antes de avisar el evento
        if (ProfileDetails != null) ProfileDetails.SetActive(false);
        if (Feedback != null) Feedback.SetActive(false);
        if (TestMain != null) TestMain.SetActive(true);

        AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);

        OnTestMain?.Invoke();
        Debug.Log("TestMain activated");
    }

    public void ShowFigureSelect()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);

        OnFigureSelect?.Invoke();
        Debug.Log("FigureSelect activated");
    }

    public void ShowDifficultySelect()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);

        OnDifficultySelect?.Invoke();
        Debug.Log("DifficultySelect activated");
    }

    public void ShowInstructions()
    {
        
        if (quizLoader != null)
        {
            quizLoader.LoadQuestions();
        }
        else
        {
            Debug.LogWarning("QuizManager: No has asignado la referencia al QuizLoader.");
        }
        
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);

        OnInstructions?.Invoke();
        Debug.Log("Instructions activated");
    }

    public void ShowTestItems()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        AudioManager.instance.PlayMusic(AudioManager.instance.quizMusic);

        OnTestItems?.Invoke();

        QuizUI quizUI = FindObjectOfType<QuizUI>();
        if (quizUI != null)
        {
           quizUI.ResetQuiz();
        }
        
        Debug.Log("TestItems activated");
    }

   public void ShowFeedback()
{
    Debug.Log("[MANAGER] ▶️ ShowFeedback() llamado");
    
    if (Feedback == null)
    {
        Debug.LogError("[MANAGER] ❌ La referencia 'Feedback' está vacía en el Inspector");
        return;
    }

    // Busca en el objeto Y en todos sus hijos 
    QuizFeedbackUI feedbackScript = Feedback.GetComponentInChildren<QuizFeedbackUI>(true);
    
    if (feedbackScript != null)
    {
        feedbackScript.ShowFeedback();
    }
    else
    {
        Debug.LogError("[MANAGER] ❌ NO ENCONTRADO: Arrastra el GameObject que tiene 'QuizFeedbackUI' al campo 'Feedback' del Manager, o adjunta el script al objeto 'Feedback'.");
    }
    
    OnFeedback?.Invoke();
}

    public void SetTestType(string type)
    {
        testType = type;
        Debug.Log("TestType: " + type);
    }

    public void SetFigure(string figure)
    {
        selectedFigure = figure;
        Debug.Log("Figure: " + figure);
    }

    public void SetDifficulty(string diff)
    {
        difficulty = diff;
        Debug.Log("Difficulty: " + diff);
    }

    public void UnlockNextDifficulty()
    {
        if (difficulty == "facil")
        {
            PlayerPrefs.SetInt("mediumUnlocked", 1);
        }
        else if (difficulty == "media")
        {
            PlayerPrefs.SetInt("hardUnlocked", 1);
        }

        PlayerPrefs.Save();
    } 

    public void BackToFigureSelect()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        ShowFigureSelect();
    }

    public void BackToTestMain()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        ShowTestMain();
    }

    public void GoHome()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        SceneManager.LoadScene("MainMenu");
    }

}
