using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ARManager : MonoBehaviour
{

    public event Action OnARMainMenu;
    public event Action OnItemsMenu;
    public event Action OnARPosition;
    public static ARManager instance;
    private bool initial = true;
    private void Awake()
    {
        if (instance!=null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMusic(AudioManager.instance.arMusic);
        ARMainMenu();
        initial = false;
    }
    public void ARMainMenu() 
    {
        if (!initial)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        }
        OnARMainMenu?.Invoke();
        Debug.Log("AR Main Menu activated");
    }

    public void ItemsMenu() 
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        OnItemsMenu?.Invoke();
        Debug.Log("Items Menu activated");
    }

    public void ARPosition()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.placeObject);
        OnARPosition?.Invoke();
        Debug.Log("AR Position activated");
    } 

    public void GoHome()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        SceneManager.LoadScene("MainMenu");
    }

}
