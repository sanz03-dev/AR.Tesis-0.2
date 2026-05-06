using System;

[Serializable]
public class Question
{
    public string testType;
    public string figure;
    public string difficulty;
    public string question;
    public string[] answers;
    public int correct;

    // SOLO PERCEPCIÓN
    public Answer[] answersWithValue;

}

[Serializable]
public class Answer
{
    public string text;
    public int value;
}