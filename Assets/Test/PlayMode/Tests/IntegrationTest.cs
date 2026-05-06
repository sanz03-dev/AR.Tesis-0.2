using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;
using UnityEngine.UI;

public class IntegrationTest
{

    [UnityTest]
    public IEnumerator Info_LoadAndDisplayCards()
    {
        GameObject go = new GameObject();
        InfoContentManager manager = go.AddComponent<InfoContentManager>();

        GameObject container = new GameObject();
        manager.SendMessage("LoadJSON");

        yield return null;

        manager.SendMessage("LoadContent");

        yield return null;

        Assert.IsNotNull(manager);
    }

}