using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanelButton : PanelButton
{
    public string panelId;
    public void ChangePanel()
    {
        panelManager.ChangePanel(panelId);
    }
}
