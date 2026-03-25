using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelector : MonoBehaviour
{
    public void SelectEasy()
    {
        GameManager.Instance.SetDifficulty("Easy");
        SceneManager.LoadScene("TestItems");
    }

    public void SelectMedium()
    {
        GameManager.Instance.SetDifficulty("Medium");
        SceneManager.LoadScene("TestItems");
    }

    public void SelectHard()
    {
        GameManager.Instance.SetDifficulty("Hard");
        SceneManager.LoadScene("TestItems");
    }
}