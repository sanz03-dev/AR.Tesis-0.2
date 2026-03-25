using UnityEngine;
using UnityEngine.UI;

public class DifficultyUIManager : MonoBehaviour
{
    public Button mediumButton;
    public Button hardButton;

    public GameObject mediumLock;
    public GameObject hardLock;

    void Start()
    {
        mediumButton.interactable = GameManager.Instance.mediumUnlocked;
        hardButton.interactable = GameManager.Instance.hardUnlocked;

        mediumLock.SetActive(!GameManager.Instance.mediumUnlocked);
        hardLock.SetActive(!GameManager.Instance.hardUnlocked);
    }
}