using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PanelModel
{
    public string panelId;
    public GameObject panelObject;

    public override string ToString()
    {
        return panelId;
    }
}
