using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : Panel
{
    [SerializeField]
    private Button startBtn, quitBtn;

    private void OnEnable()
    {
        startBtn.onClick.AddListener(() => UIManager.Instance.OpenPanel(PanelName.SettingPanel));
        quitBtn.onClick.AddListener(() => GameManager.Instance.quitGame());
    }

    private void OnDisable()
    {
        startBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.RemoveAllListeners();
    }
}
