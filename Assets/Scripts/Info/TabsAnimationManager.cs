using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TabsAnimationManager : MonoBehaviour
{
    public CanvasGroup pageDescripcion;
    public CanvasGroup pageHistoria;
    public CanvasGroup pageCuriosidades;

    float duration = 0.3f;

    public void ShowDescripcion()
    {
        Animate(pageDescripcion);
    }

    public void ShowHistoria()
    {
        Animate(pageHistoria);
    }

    public void ShowCuriosidades()
    {
        Animate(pageCuriosidades);
    }

    void Animate(CanvasGroup target)
    {
        CanvasGroup[] all = { pageDescripcion, pageHistoria, pageCuriosidades };

        foreach (CanvasGroup current in all)
        {

        // 1. DETENER animaciones previas para evitar conflictos
        current.DOKill(); 
        current.transform.DOKill();

        if (current == target)
        {

        // MOSTRAR
        current.gameObject.SetActive(true);
        current.interactable = true;
        current.blocksRaycasts = true;

        // Animación de entrada limpia
        current.DOFade(1, duration).SetEase(Ease.OutCubic);
        current.transform.DOScale(Vector3.one, duration).SetEase(Ease.OutBack);
        }

        else
        {

            // OCULTAR
            current.interactable = false;
            current.blocksRaycasts = false;

            // Animación de salida 
            current.DOFade(0, duration).SetEase(Ease.InCubic).OnComplete(() => {
               
               if(current.alpha < 0.1f) current.gameObject.SetActive(false);
             });
             current.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), duration);
             
            }
        }
    }
}
