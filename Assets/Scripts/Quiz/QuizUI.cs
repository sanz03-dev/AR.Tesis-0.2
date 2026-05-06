using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] answerTexts;
    public Button[] answerButtons;

    [Header("Progress UI")]
    public Slider progressBar;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI timerText;

    private Color colorBaseQuiz = new Color(0.388f, 0.584f, 0.568f, 0.341f);
    private int score = 0;

    private List<Question> questions;
    private int currentQuestionIndex = 0;

    private int totalScore = 0;

    private float timer = 0f;
    private bool isRunning = false;

    void OnEnable()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForEndOfFrame();
        LoadFirstQuestion();
    }

    void LoadFirstQuestion()
    {
        if (QuizManager.instance == null || QuizManager.instance.quizLoader == null)
        {
            Debug.LogError("QuizUI: No se pudo cargar porque falta el Manager o el Loader.");
            return; 
        }

        questions = QuizManager.instance.quizLoader.GetQuestions();

        if (questions == null || questions.Count == 0)
        {
            Debug.LogWarning("QuizUI: La lista de preguntas está vacía.");
            return;
        }

        timer = 0f;
        isRunning = true;

        currentQuestionIndex = 0;
        ShowQuestion(currentQuestionIndex);
    }

    void ShowQuestion(int index)
    {
       if (questions == null || index >= questions.Count) return;

       Question q = questions[index];

       questionText.text = q.question;

       if (QuizManager.instance.testType == "percepcion")
       {
           for (int i = 0; i < answerTexts.Length; i++)
           {
               if (i < q.answersWithValue.Length)
               {
                   answerTexts[i].text = q.answersWithValue[i].text;
                }
            }
        }        
        else
        {
            for (int i = 0; i < answerTexts.Length; i++)
            {
                if (i < q.answers.Length)
                {
                   answerTexts[i].text = q.answers[i];
                }
            }
        }

        float progress = (float)index  / questions.Count;

        progressBar.value = progress;

        if (progressBar.fillRect != null)
        {
           float alpha = (progressBar.value == 0) ? 0 : 1;
           progressBar.fillRect.GetComponent<CanvasRenderer>().SetAlpha(alpha);
        }
   
        int percentage = Mathf.RoundToInt(progress * 100);
        progressText.text = percentage + "%";
    }

    public void SelectAnswer(int index)
    {   
       Question q = questions[currentQuestionIndex]; 

       // Bloquear todos los botones para evitar doble clic
       foreach (Button btn in answerButtons) btn.interactable = false;

       // Obtener el componente Image del botón presionado
       Image selectedImage = answerButtons[index].GetComponent<Image>();

       // =========================
       // MODO PERCEPCIÓN (Test Personalidad/Psicológico)
       // =========================
       if (QuizManager.instance.testType == "percepcion")
       {
           // Usa la lista de valores 
           int value = q.answersWithValue[index].value;

           if (IsInverted(currentQuestionIndex))
           {
               value = 6 - value;
               Debug.Log("Ítem invertido aplicado");
            }

            totalScore += value;
            selectedImage.color = Color.cyan;
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        }
        // =========================
        // MODO FIGURAS (Test de Conocimiento / Tesis)
        // =========================
        else
        {
           // Aquí se compara el índice presionado con el 'correct' del JSON
           if (index == q.correct)
           {
               selectedImage.color = Color.green;

               AudioManager.instance.PlaySFX(AudioManager.instance.correct);

               Debug.Log("¡Correcto!");
               score++; 
            }
            else
            {
               selectedImage.color = Color.red;

               AudioManager.instance.PlaySFX(AudioManager.instance.wrong);

               Debug.Log("¡Incorrecto!");
            
               // Para Mostrar cuál era la correcta en verde
               // answerButtons[q.correct].GetComponent<Image>().color = Color.green;
            }
        }

        StartCoroutine(NextQuestionRoutine());
    }

    void ResetButtons()
    {
        foreach (Button btn in answerButtons)
        {
            btn.interactable = true;
            btn.GetComponent<Image>().color = colorBaseQuiz;
        }
    }

    IEnumerator NextQuestionRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Count)
        {
            ResetButtons();
            ShowQuestion(currentQuestionIndex);
        }
        else
        {
           isRunning = false;

           Debug.Log("QUIZ TERMINADO");

          
        if (QuizManager.instance.testType != "percepcion") // solo para modo figuras
        {
            if (score >= 6)
            {
                QuizManager.instance.UnlockNextDifficulty();
                Debug.Log("Dificultad desbloqueada");
            }
            else
            {
                Debug.Log("No se desbloquea dificultad (score insuficiente)");
            }
        }

        QuizManager.instance.ShowFeedback();
        }
    }

    public int GetScore()
    {  
        return score;
    }

    public int GetTotalQuestions()
    {
        return questions.Count;
    }

    public void ResetQuiz()
    {
        score = 0;
        totalScore = 0;
        currentQuestionIndex = 0;
        timer = 0f;

        if (questions != null && questions.Count > 0)
        {
            ResetButtons();
            ShowQuestion(currentQuestionIndex);
        }
    }

    bool IsInverted(int index)
    {
        return index == 1 || index == 5 || index == 7 || index == 10;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    void Update()
    {
       if (!isRunning) return;

       timer += Time.deltaTime;

       int minutes = Mathf.FloorToInt(timer / 60);
       int seconds = Mathf.FloorToInt(timer % 60);

       timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
