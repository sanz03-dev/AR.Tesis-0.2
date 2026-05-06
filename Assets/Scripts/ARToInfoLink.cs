using UnityEngine;
using UnityEngine.SceneManagement;

public class ARToInfoLink : MonoBehaviour
{
    [Header("Configuración de Información")]
    public string itemNameInJson;
    public string category = "figuras";

    private float lastTapTime;
    private const float doubleTapDelay = 0.3f;

    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Lanzamos un rayo desde la cámara hacia donde tocamos
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Si el rayo golpea este objeto
                    if (hit.transform == transform)
                    {
                        HandleDoubleTap();
                    }
                }
            }
        }
    }

    private void HandleDoubleTap()
    {
        if (Time.time - lastTapTime < doubleTapDelay)
        {
            TriggerInfoTransition();
        }
        lastTapTime = Time.time;
    }

    public void TriggerInfoTransition()
    {
        PlayerPrefs.SetString("DirectAccessItem", itemNameInJson);
        PlayerPrefs.SetString("DirectAccessMenu", category);
        PlayerPrefs.SetInt("ShouldSkipMenus", 1);
        SceneManager.LoadScene("InfoScene"); 
    }
}