using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    List<Panel> panelsList;

    public void OpenPanel(PanelName panelName)
    {
        foreach (Panel panel in panelsList)
        {
            panel.gameObject.SetActive((panelName == panel.PanelName) ? true : false);
        }
    }

    [ContextMenu("SetUpPanel")]
    private void SetUpPanels()
    {
        foreach (Panel panel in GetComponentsInChildren<Panel>(includeInactive: true))
        {
            panelsList.Add(panel);
        }
    }
}
