using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
        }
    }

    public void GoToAR()
    {
        LoadSceneWithSound("ARScene");
    }

    public void GoToInfo()
    {
        LoadSceneWithSound("InfoScene");
    }

    public void GoToQuiz()
    {
        LoadSceneWithSound("QuizScene");
    }

    // 🔥 MÉTODO CENTRALIZADO (la mejora real)
    void LoadSceneWithSound(string sceneName)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        }

        SceneManager.LoadScene(sceneName);
    }

}