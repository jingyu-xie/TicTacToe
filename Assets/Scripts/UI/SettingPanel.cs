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
    private Toggle toggle_PVE;
    [SerializeField]
    private TMP_InputField InputField_boardSize;
    [SerializeField]
    private TMP_Text warningText;
    private string defaultWarning;

    private GameMode gameMode;
    private int boardSize;

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
        if (toggle_PVE.isOn)
            gameMode = GameMode.PVE;
        else
            gameMode = GameMode.PVP;

        if (int.TryParse(InputField_boardSize.text, out boardSize))
        {
            if (boardSize < 3 || boardSize > 6)
            {
                warningText.text = defaultWarning + InputField_boardSize.text;
                warningText.gameObject.SetActive(true);
                return;
            }
            UIManager.Instance.OpenPanel(PanelName.GamePanel);
            GameManager.Instance.StartGame(gameMode, boardSize);
        }
    }
}
