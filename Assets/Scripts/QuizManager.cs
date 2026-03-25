using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;

  public Button[] answerButtons;

    public Slider progressBar;

    int currentQuestion = 0;
    int totalQuestions = 10;

    List<Question> filteredQuestions = new List<Question>();
    Question currentQuestionData;

    void Start()
    {
        LoadQuestions();
        ShowQuestion();
        UpdateProgress();
    }


    public void AnswerSelected(int answerIndex)
    {
        currentQuestion++;

        if (currentQuestion >= totalQuestions)
        {
            FinishQuiz();
        }
        else
        {
            ShowQuestion();
            UpdateProgress();
        }
    }

    void LoadQuestions()
{
    TextAsset jsonFile = Resources.Load<TextAsset>("quiz");

    QuestionList questionList = JsonUtility.FromJson<QuestionList>(jsonFile.text);

    foreach (Question q in questionList.questions)
    {
        if (q.figure == GameManager.Instance.selectedFigure &&
            q.difficulty == GameManager.Instance.selectedDifficulty)
        {
            filteredQuestions.Add(q);
        }
    }

    totalQuestions = filteredQuestions.Count;
}

    void ShowQuestion()
{
    currentQuestionData = filteredQuestions[currentQuestion];

    questionText.text = currentQuestionData.question;

    answerButtons[0].GetComponentInChildren<TMP_Text>().text = currentQuestionData.answers[0];
    answerButtons[1].GetComponentInChildren<TMP_Text>().text = currentQuestionData.answers[1];
    answerButtons[2].GetComponentInChildren<TMP_Text>().text = currentQuestionData.answers[2];
}

    /*public void Answer(int index)
{
    if (index == currentQuestionData.correct)
    {
        Debug.Log("Correcto");
    }
    else
    {
        Debug.Log("Incorrecto");
    }

    currentQuestion++;

    if (currentQuestion < totalQuestions)
    {
        ShowQuestion();
        UpdateProgress();
    }
    else
    {
        FinishQuiz();
    }
}*/

    void UpdateProgress()
    {
        progressBar.value = (float)(currentQuestion + 1) / totalQuestions;
    }

    void FinishQuiz()
    {
        Debug.Log("Quiz terminado");
    }

}