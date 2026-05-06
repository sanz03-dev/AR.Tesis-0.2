using UnityEngine;

public class ExitConfirmationUI : MonoBehaviour
{
    public GameObject panel;

    public void ShowPanel()
    {
        panel.SetActive(true);
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
    }

    public void HidePanel()
    {
        panel.SetActive(false);
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
    }

    public void ConfirmExit()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);

        panel.SetActive(false);

        // olver al menú principal del test
        QuizManager.instance.ShowTestMain();
    }
}
