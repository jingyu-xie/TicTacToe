using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private List<Panel> panelsList;

    [SerializeField]
    private List<PlayerContainer> playerContainersList = new List<PlayerContainer>(2);

    [SerializeField]
    private GameObject popupWindow;

    private void Start()
    {
        OpenPanel(PanelName.StartPanel);
    }

    public void OpenPanel(PanelName panelName)
    {
        foreach (Panel panel in panelsList)
        {
            panel.gameObject.SetActive((panelName == panel.PanelName) ? true : false);
        }
    }

    public void PopUpWindow(string message)
    {
        popupWindow.SetActive(true);
        popupWindow.GetComponent<PopupWindow>().SetUpMessage(message);
    }

    #region Set Ups
    public void SetUpPlayerContainers(List<Player> playersList)
    {
        for (int player_idx = 0; player_idx < playersList.Count; player_idx++)
        {
            playerContainersList[player_idx].PlayerIDText = playersList[player_idx].PlayerID;
            playerContainersList[player_idx].PlayerColor = (playersList[player_idx].PlayerMark == MarkType.Cross) ? Color.black : Color.red;
        }
    }

    [ContextMenu("SetUpUIManager")]
    private void SetUpUIManager()
    {
        panelsList.Clear();
        playerContainersList.Clear();

        foreach (Panel panel in GetComponentsInChildren<Panel>(includeInactive: true))
        {
            panelsList.Add(panel);
        }

        foreach (PlayerContainer pc in GetComponentsInChildren<PlayerContainer>(includeInactive: true))
        {
            playerContainersList.Add(pc);
        }
    }
    #endregion
}
