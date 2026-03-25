using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Datos del test seleccionado
    public string selectedFigure;
    public string selectedDifficulty;
    public string selectedTest;

    // Progreso de desbloqueo
    public bool mediumUnlocked = false;
    public bool hardUnlocked = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Guardar figura seleccionada
    public void SetFigure(string figure)
    {
        selectedFigure = figure;
    }

    // Guardar dificultad seleccionada
    public void SetDifficulty(string difficulty)
    {
        selectedDifficulty = difficulty;
    }

    // Guardar tipo de test
    public void SetTest(string test)
    {
        selectedTest = test;
    }

    // Desbloquear dificultad media
    public void UnlockAdvanced()
    {
        mediumUnlocked = true;
    }

    // Desbloquear dificultad difícil
    public void UnlockHard()
    {
        hardUnlocked = true;
    }
}