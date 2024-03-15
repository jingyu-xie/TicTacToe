using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : Panel
{
    [SerializeField]
    private Button backBtn, settingStartBtn;

    [SerializeField]
    private Toggle toggle_PVE, toggle_GoFirst;

    [SerializeField]
    private TMP_Text warningText;
    private string defaultWarning;

    private void Start()
    {
        defaultWarning = warningText.text;
    }

    private void OnEnable()
    {
        backBtn.onClick.AddListener(() => UIManager.Instance.OpenPanel(PanelName.StartPanel));
        settingStartBtn.onClick.AddListener(SettingStartBtnOnclickPerform);
    }

    private void OnDisable()
    {
        backBtn.onClick.RemoveAllListeners();
        settingStartBtn.onClick.RemoveAllListeners();
    }

    private void SettingStartBtnOnclickPerform()
    {
        GameMode gameMode = GameMode.PVE;
        bool goFirst = false;

        if (toggle_PVE.isOn)
            gameMode = GameMode.PVE;
        else
            gameMode = GameMode.PVP;

        if (toggle_GoFirst.isOn)
            goFirst = true;

        UIManager.Instance.OpenPanel(PanelName.GamePanel);
        GameManager.Instance.StartGame(gameMode, goFirst);
    }
}
