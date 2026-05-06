using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoContentManager : MonoBehaviour
{
    private string currentItemName;
    [Header("Contenedores de tarjetas")]
    [SerializeField] private Transform descriptionContainer;
    [SerializeField] private Transform historyContainer;
    [SerializeField] private Transform curiositiesContainer;

    [SerializeField] private GameObject cardPrefab;

    [Header("Configuración de Scrolls")]
    [SerializeField] private ScrollRect scrollDesc;
    [SerializeField] private ScrollRect scrollHist;
    [SerializeField] private ScrollRect scrollCurio;

    private InfoList data;

    // FLUJO NORMAL: Se activa al abrir el panel desde los menús
    void OnEnable()
    {
        if (data == null) LoadJSON();
        
        LoadContent();
    }

    // FLUJO RA: Llamado manualmente por el InfoManager durante el salto
    public void InicializarCargaDirecta(string nombreItem)
    {
        Debug.Log("[DEBUG] InfoContentManager: Iniciando carga directa para " + nombreItem);
        
        if (data == null) LoadJSON();

        LoadContent(nombreItem);
    }

    void LoadJSON()
    {
        TextAsset json = Resources.Load<TextAsset>("infoData");
        if (json != null)
        {
            data = JsonUtility.FromJson<InfoList>(json.text);
        }
    }

    void LoadContent(string forcedItemName = "")
    {
        string selected = !string.IsNullOrEmpty(forcedItemName) ? forcedItemName : 
                          (InfoManager.instance != null ? InfoManager.instance.selectedItem : "");

        if (string.IsNullOrEmpty(selected)) 
        {
            
            return;
        }
        
        if (data == null || data.items == null) return;

        foreach (InfoItem item in data.items)
        {
            if (item.name == selected)
            {
                currentItemName = item.name;
                CreateCards(item.description, descriptionContainer);
                CreateCards(item.history, historyContainer);
                CreateCards(item.curiosities, curiositiesContainer);

                // Reset de posición de los scrolls
                ResetScroll(scrollDesc);
                ResetScroll(scrollHist);
                ResetScroll(scrollCurio);
                
                return;
            }
        }

        Debug.LogError("No se encontró información en el JSON para: " + selected);
    }

    private void ResetScroll(ScrollRect sr)
    {
        if (sr != null)
        {
            Canvas.ForceUpdateCanvases();
            sr.verticalNormalizedPosition = 1f;
            
            if (sr.content != null)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(sr.content);
            }
        }
    }

    void CreateCards(List<InfoCard> cards, Transform container)
    {
        // Limpieza de tarjetas anteriores
        foreach (Transform child in container)
        {
           Destroy(child.gameObject);
        }

        if (cards == null || cards.Count == 0) return; 

        foreach (InfoCard card in cards)
        {
           GameObject newCard = Instantiate(cardPrefab, container);

           TextMeshProUGUI title = newCard.transform.Find("Title").GetComponent<TextMeshProUGUI>();
           TextMeshProUGUI text = newCard.transform.Find("Text").GetComponent<TextMeshProUGUI>();
           Image img = newCard.transform.Find("Image").GetComponent<Image>();

           if (title != null) title.text = card.title;
           if (text != null) text.text = card.text;

           if (img != null && !string.IsNullOrEmpty(card.image))
           {
               Sprite sprite = Resources.Load<Sprite>("Images/" + card.image);
               if (sprite != null) img.sprite = sprite;
            }
        }
    }
}
