using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoManager : MonoBehaviour
{
    public static InfoManager instance;
    public string selectedItem;
    public string lastMenu;

    [Header("Referencias de UI")]
    [SerializeField] private InfoContentManager contentManager;
    [SerializeField] private GameObject panelPreguntaRA; 

    private bool vieneDeRA = false; // Flag para saber el origen

    // EVENTOS
    public event Action OnInfoMenu;
    public event Action OnSubMenuFiguras;
    public event Action OnSubMenuCognition;
    public event Action OnInfoTabs;

    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(gameObject); }
        else { instance = this; }
    }

    void Start()
    {
        StartCoroutine(InitSequence());
    }

    IEnumerator InitSequence()
    {
        yield return null;

        if (panelPreguntaRA != null) panelPreguntaRA.SetActive(false);

        if (PlayerPrefs.GetInt("ShouldSkipMenus", 0) == 1)
        {
            vieneDeRA = true; 
            Debug.Log("[INFO] 🚀 Acceso directo de RA detectado.");
            ExecuteDirectAccess();
        }
        else
        {
            vieneDeRA = false;
            Debug.Log("[INFO] 🏠 Inicio normal.");
            OnInfoMenu?.Invoke();
        }
    }

    private void ExecuteDirectAccess()
    {
        string item = PlayerPrefs.GetString("DirectAccessItem", "");
        
     
        PlayerPrefs.SetInt("ShouldSkipMenus", 0);
        PlayerPrefs.Save();

        if (contentManager != null && !string.IsNullOrEmpty(item))
        {
            contentManager.InicializarCargaDirecta(item);
        }

        ShowInfoTabs();
    }

    // --- LÓGICA DE NAVEGACIÓN ---

    public void BackFromTabs()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);

        if (vieneDeRA)
        {
           
            if (panelPreguntaRA != null)
            {
                panelPreguntaRA.SetActive(true);
            }
            else
            {
                
                VolverARealidadAumentada();
            }
        }
        else
        {
            // Flujo normal: volver a los submenús
            RegresarAlMenuAnterior();
        }
    }

    // Función para el botón "SÍ" del panel de pregunta
    public void VolverARealidadAumentada()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        SceneManager.LoadScene("ARScene"); // Cambia por el nombre exacto de tu escena RA
    }

    // Función para el botón "NO" del panel de pregunta
    public void RegresarAlMenuAnterior()
    {
        if (panelPreguntaRA != null) panelPreguntaRA.SetActive(false);
        
        AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);

        if (lastMenu == "figuras")
        {
            ShowSubMenuFiguras();
        }
        else if (lastMenu == "cognition")
        {
            ShowSubMenuCognition();
        }
        else
        {
            
            ShowInfoMenu();
        }
    }

    // --- MÉTODOS DE ACTIVACIÓN (Invokes) ---

    public void ShowInfoMenu()
    {
        OnInfoMenu?.Invoke();
    }

    public void ShowSubMenuFiguras()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        lastMenu = "figuras";
        OnSubMenuFiguras?.Invoke();
    }

    public void ShowSubMenuCognition()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        lastMenu = "cognition";
        OnSubMenuCognition?.Invoke();
    }

    public void ShowInfoTabs()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        AudioManager.instance.PlayMusic(AudioManager.instance.infoMusic);
        OnInfoTabs?.Invoke();
    }

    public void SetSelectedItem(string item) { selectedItem = item; }

    public void GoHome()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        SceneManager.LoadScene("MainMenu");
    }
}
