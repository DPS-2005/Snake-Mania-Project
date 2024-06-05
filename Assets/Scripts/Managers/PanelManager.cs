using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelManager : Singleton<PanelManager>
{
    public PanelModel mainPanel;
    public List<PanelModel> panels;
    private Stack<PanelModel> panelQueue;

    private void Start()
    {
        panelQueue = new Stack<PanelModel>();
        panelQueue.Push(mainPanel);
    }
    public void ChangePanel(string panelId)
    {
        PanelModel panelToShow = panels.FirstOrDefault(panel => {  return panel.panelId == panelId; });
        if(panelToShow != null)
        {
            PanelModel oldPanel = panelQueue.Peek();
            panelQueue.Push(panelToShow);
            panelToShow.panelObject.SetActive(true);
            oldPanel.panelObject.SetActive(false);
        }
        else
        {
            Debug.Log("no panel to show");
        }
    }

    public void DisplayOver(string panelId)
    {
        PanelModel panelToShow = panels.FirstOrDefault(panel => { return panel.panelId == panelId; });
        if (panelToShow != null)
        {
            panelQueue.Push(panelToShow);
            panelToShow.panelObject.SetActive(true);
        }
        else
        {
            Debug.Log("no panel to show");
        }
    }

    public void GoToPreviousPanel()
    {
        if (panelQueue.Count <= 1)
            return;
        PanelModel oldPanel = panelQueue.Peek();
        panelQueue.Pop();
        panelQueue.Peek().panelObject.SetActive(true);
        oldPanel.panelObject.SetActive(false);
    }

    public void ClosePanel()
    {
        PanelModel oldPanel = panelQueue.Peek();
        panelQueue.Pop();
        oldPanel.panelObject.SetActive(false);
    }
}
