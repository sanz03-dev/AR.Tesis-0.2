using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class InfoCard
{
    public string title;
    public string text;
    public string image;
}

[Serializable]
public class InfoItem
{
    public string name;
    public List<InfoCard> description;
    public List<InfoCard> history;
    public List<InfoCard> curiosities;
}

[Serializable]
public class InfoList
{
    public InfoItem[] items;
}