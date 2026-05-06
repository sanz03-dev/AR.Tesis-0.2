using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class QuizUIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject testMain;
    [SerializeField] private GameObject figureSelect;
    [SerializeField] private GameObject difficultySelect;
    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject testItems;
    [SerializeField] private GameObject feedback;

    [Header("Elements for Animation")]
    [SerializeField] private Transform[] testMainItems;
    [SerializeField] private Transform[] figureItems;
    [SerializeField] private Transform[] difficultyItems;
    [SerializeField] private Transform[] instructionsItems;
    [SerializeField] private Transform[] testItemsUI;
    [SerializeField] private Transform[] feedbackItems;

    void Start()
    {
        QuizManager.instance.OnTestMain += ActivateTestMain;
        QuizManager.instance.OnFigureSelect += ActivateFigureSelect;
        QuizManager.instance.OnDifficultySelect += ActivateDifficultySelect;
        QuizManager.instance.OnInstructions += ActivateInstructions;
        QuizManager.instance.OnTestItems += ActivateTestItems;
        QuizManager.instance.OnFeedback += ActivateFeedback;

        // Estado inicial limpio
        ShowOnly(testMain);
    }

    private void ActivateTestMain() => PrepareAndShow(testMain, testMainItems);
    private void ActivateFigureSelect() => PrepareAndShow(figureSelect, figureItems);
    private void ActivateDifficultySelect() => PrepareAndShow(difficultySelect, difficultyItems);
    private void ActivateInstructions() => PrepareAndShow(instructions, instructionsItems);
    private void ActivateTestItems() => PrepareAndShow(testItems, testItemsUI);
    private void ActivateFeedback() => PrepareAndShow(feedback, feedbackItems);

    // =========================
    // FUNCIÓN CLAVE 
    // =========================
    private void PrepareAndShow(GameObject panel, Transform[] items)
    {
        // 1. Apagar todo
        testMain.SetActive(false);
        figureSelect.SetActive(false);
        difficultySelect.SetActive(false);
        instructions.SetActive(false);
        testItems.SetActive(false);
        feedback.SetActive(false);

        // 2. Activar el panel correcto
        panel.SetActive(true);

        // 3. Animar elementos
        foreach (Transform item in items)
        {
            item.DOKill();
            item.localScale = Vector3.zero;
            item.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).SetUpdate(true);
        }
    }

    private void ShowOnly(GameObject panel)
    {
        testMain.SetActive(panel == testMain);
        figureSelect.SetActive(panel == figureSelect);
        difficultySelect.SetActive(panel == difficultySelect);
        instructions.SetActive(panel == instructions);
        testItems.SetActive(panel == testItems);
        feedback.SetActive(panel == feedback);
    }
}
