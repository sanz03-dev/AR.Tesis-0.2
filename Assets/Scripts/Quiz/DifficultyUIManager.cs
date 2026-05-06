using UnityEngine;
using UnityEngine.UI;

public class DifficultyUIManager : MonoBehaviour
{
    [Header("Botones")]
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    [Header("Bloqueos visuales")]
    public GameObject mediumLock;
    public GameObject hardLock;

    public GameObject mediumOverlay;
    public GameObject hardOverlay;

    public GameObject mediumTextLock;
    public GameObject hardTextLock;

    void OnEnable()
    {
        UpdateDifficultyUI();
    }

    void UpdateDifficultyUI()
    {
        // Fácil SIEMPRE desbloqueado
        easyButton.interactable = true;

        // MEDIA
        bool mediumUnlocked = PlayerPrefs.GetInt("mediumUnlocked", 0) == 1;

        mediumButton.interactable = mediumUnlocked;
        mediumLock.SetActive(!mediumUnlocked);
        mediumOverlay.SetActive(!mediumUnlocked);
        mediumTextLock.SetActive(!mediumUnlocked);

        // DIFÍCIL
        bool hardUnlocked = PlayerPrefs.GetInt("hardUnlocked", 0) == 1;

        hardButton.interactable = hardUnlocked;
        hardLock.SetActive(!hardUnlocked);
        hardOverlay.SetActive(!hardUnlocked);
        hardTextLock.SetActive(!hardUnlocked); 
    }
}