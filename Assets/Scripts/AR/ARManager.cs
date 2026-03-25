using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ARManager : MonoBehaviour
{

    public event Action OnARMainMenu;
    public event Action OnItemsMenu;
    public event Action OnARPosition;
    public static ARManager instance;
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
        ARMainMenu();
    }
public void ARMainMenu() {
    OnARMainMenu?.Invoke();
    Debug.Log("AR Main Menu activated");
}

public void ItemsMenu() {
    OnItemsMenu?.Invoke();
    Debug.Log("Items Menu activated");
}

public void ARPosition() {
    OnARPosition?.Invoke();
    Debug.Log("AR Position activated");
}

public void CloseAPP()
    {
        Application.Quit();
    }



}
