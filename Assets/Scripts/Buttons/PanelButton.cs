using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PanelButton : MonoBehaviour
{
    protected PanelManager panelManager;
    public void Start()
    {
        panelManager = PanelManager.Instance;
    }

    

}
