using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject ARMainMenu;
    [SerializeField] private GameObject ItemsMenu;
    [SerializeField] private GameObject ARPosition;
    // Start is called before the first frame update
    void Start()
    {
        ARManager.instance.OnARMainMenu += ActivateARMainMenu;
        ARManager.instance.OnItemsMenu += ActivateItemsMenu;    
        ARManager.instance.OnARPosition += ActivateARPosition;

    }

    private void ActivateARMainMenu()
    {
        ARMainMenu.transform.GetChild(0).transform.DOScale(new Vector3(1,1,1), 0.3f);
        ARMainMenu.transform.GetChild(1).transform.DOScale(new Vector3(1,1,1), 0.3f);

        ItemsMenu.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0.5f);
        ItemsMenu.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), 0.3f);
        ItemsMenu.transform.GetChild(1).transform.DOMoveY(180, 0.3f);

        ARPosition.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0.3f);
        ARPosition.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), 0.3f);
         
    }

    private void ActivateItemsMenu()
    {
        ARMainMenu.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0.3f);
        ARMainMenu.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), 0.3f);

        ItemsMenu.transform.GetChild(0).transform.DOScale(new Vector3(1,1,1), 0.5f);
        ItemsMenu.transform.GetChild(1).transform.DOScale(new Vector3(1,1,1), 0.3f);
        ItemsMenu.transform.GetChild(1).transform.DOMoveY(300, 0.3f);
    }

    private void ActivateARPosition()
    {
        ARMainMenu.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0.3f);
        ARMainMenu.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), 0.3f);

        ItemsMenu.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0.5f);
        ItemsMenu.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), 0.3f);
        ItemsMenu.transform.GetChild(1).transform.DOMoveY(180, 0.3f);

        ARPosition.transform.GetChild(0).transform.DOScale(new Vector3(1,1,1), 0.3f);
        ARPosition.transform.GetChild(1).transform.DOScale(new Vector3(1,1,1), 0.3f);
    }
}
