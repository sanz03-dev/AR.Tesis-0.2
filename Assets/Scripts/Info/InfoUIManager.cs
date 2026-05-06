using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class InfoUIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject infoMenu;
    [SerializeField] private GameObject subMenuFiguras;
    [SerializeField] private GameObject subMenuCognition;
    [SerializeField] private GameObject infoTabs;

    [Header("Elements for Animation")]
    [SerializeField] private Transform[] infoMenuItems;
    [SerializeField] private Transform[] figurasItems;
    [SerializeField] private Transform[] cognitionItems;
    [SerializeField] private Transform[] tabsItems; 

    void Start()
    {
        InfoManager.instance.OnInfoMenu += ActivateInfoMenu;
        InfoManager.instance.OnSubMenuFiguras += ActivateSubMenuFiguras;
        InfoManager.instance.OnSubMenuCognition += ActivateSubMenuCognition;
        InfoManager.instance.OnInfoTabs += ActivateInfoTabs;
        
        
        ShowOnly(infoMenu);
    }

    private void ActivateInfoMenu() => PrepareAndShow(infoMenu, infoMenuItems);
    private void ActivateSubMenuFiguras() => PrepareAndShow(subMenuFiguras, figurasItems);
    private void ActivateSubMenuCognition() => PrepareAndShow(subMenuCognition, cognitionItems);
    private void ActivateInfoTabs() => PrepareAndShow(infoTabs, tabsItems);

    
    private void PrepareAndShow(GameObject panel, Transform[] items)
    {
        
        infoMenu.SetActive(false);
        subMenuFiguras.SetActive(false);
        subMenuCognition.SetActive(false);
        infoTabs.SetActive(false);

    
        panel.SetActive(true);

        
        foreach (Transform item in items)
        {
            item.DOKill(); 
            item.localScale = Vector3.zero; 
            item.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).SetUpdate(true); 
            
        }
    }

    private void ShowOnly(GameObject panel)
    {
        infoMenu.SetActive(panel == infoMenu);
        subMenuFiguras.SetActive(panel == subMenuFiguras);
        subMenuCognition.SetActive(panel == subMenuCognition);
        infoTabs.SetActive(panel == infoTabs);
    }
}