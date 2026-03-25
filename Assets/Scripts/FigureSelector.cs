using UnityEngine;

public class FigureSelector : MonoBehaviour
{
    public void SelectNeckerCube()
    {
        GameManager.Instance.SetFigure("NeckerCube");
    }

    public void SelectPenroseTriangle()
    {
        GameManager.Instance.SetFigure("PenroseTriangle");
    }

    public void SelectPenroseStairs()
    {
        GameManager.Instance.SetFigure("PenroseStairs");
    }
}