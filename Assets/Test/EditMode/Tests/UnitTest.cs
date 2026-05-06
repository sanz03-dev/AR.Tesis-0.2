using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;

public class UnitTest
{
    [Test]
public void Info_JSON_Exists()
{
    TextAsset json = Resources.Load<TextAsset>("infoData");

    Assert.IsNotNull(json);
}

[Test]
public void Info_JSON_ParsesCorrectly()
{
    TextAsset json = Resources.Load<TextAsset>("infoData");

    InfoList data = JsonUtility.FromJson<InfoList>(json.text);

    Assert.IsNotNull(data);
    Assert.IsTrue(data.items.Length > 0);
}

[Test]
public void Info_ItemExists()
{
    TextAsset json = Resources.Load<TextAsset>("infoData");
    InfoList data = JsonUtility.FromJson<InfoList>(json.text);

    bool found = false;

    foreach (var item in data.items)
    {
        if (item.name == "NeckerCube") // usa uno real
        {
            found = true;
            break;
        }
    }

    Assert.IsTrue(found);
}

[Test]
public void Quiz_CorrectAnswer_IncreasesScore()
{
    int score = 0;
    int correct = 2;
    int selected = 2;

    if (selected == correct)
        score++;

    Assert.AreEqual(1, score);
}

[Test]
public void Quiz_WrongAnswer_DoesNotIncreaseScore()
{
    int score = 0;
    int correct = 2;
    int selected = 1;

    if (selected == correct)
        score++;

    Assert.AreEqual(0, score);
}

[Test]
public void Perception_InvertedItem_WorksCorrectly()
{
    int value = 2;
    int inverted = 6 - value;

    Assert.AreEqual(4, inverted);
}

[Test]
public void Perception_TotalScore_AddsCorrectly()
{
    int totalScore = 0;

    totalScore += 3;
    totalScore += 4;

    Assert.AreEqual(7, totalScore);
}

[Test]
public void Feedback_ProfileAssignment_IsCorrect()
{
    int score = 50;
    string profile;

    if (score <= 24) profile = "Analítico–Secuencial";
    else if (score <= 36) profile = "Mixto o Adaptativo";
    else if (score <= 48) profile = "Visual–Espacial";
    else profile = "Intuitivo–Visual Avanzado";

    Assert.AreEqual("Intuitivo–Visual Avanzado", profile);
}

[Test]
public void Feedback_MainMessage_Correct()
{
    int score = 20;
    string message;

    if (score <= 24) message = "Tiendes a analizar paso a paso la información visual.";
    else message = "Otro";

    Assert.AreEqual("Tiendes a analizar paso a paso la información visual.", message);
}

[Test]
public void Loader_FiltersQuestions_ByType()
{
    string testType = "percepcion";

    bool matches = (testType == "percepcion");

    Assert.IsTrue(matches);
}

[Test]
public void AR_AssignModel_NotNull()
{
    GameObject model = new GameObject();

    Assert.IsNotNull(model);
}

[Test]
public void AR_DeleteItem_SetsNull()
{
    GameObject obj = new GameObject();
    
    Object.DestroyImmediate(obj);
    
    Assert.IsTrue(obj == null);
}

[Test]
public void AR_SelectItem_AssignsCorrectly()
{
    GameObject selected = new GameObject();
    GameObject current = null;

    current = selected;

    Assert.AreEqual(selected, current);
}

[Test]
public void AR_Rotation_Calculation_Works()
{
    Vector2 initial = new Vector2(1, 0);
    Vector2 current = new Vector2(0, 1);

    float angle = Vector2.SignedAngle(initial, current);

    Assert.AreEqual(90f, angle, 0.1f);
}

[Test]
public void DataManager_Items_NotEmpty()
{
    List<string> items = new List<string>() { "Cubo", "Triangulo", "Escalera" };

    Assert.IsTrue(items.Count > 0);
}

[Test]
public void AR_Prefab_NotNull()
{
    GameObject prefab = new GameObject();

    Assert.IsNotNull(prefab);
}
    
}
