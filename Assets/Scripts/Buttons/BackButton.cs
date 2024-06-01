using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : PanelButton
{
    public void GoBack()
    {
        panelManager.GoToPreviousPanel();
    }
}
