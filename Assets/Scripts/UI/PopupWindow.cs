using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupWindow : MonoBehaviour
{
    [SerializeField]
    private TMP_Text messageInfo;

    [SerializeField]
    private Button backToMenuBtn;

    public void SetUpMessage(string message)
    {
        messageInfo.text = message;
    }

    private void OnEnable()
    {
        backToMenuBtn.onClick.AddListener(BackToStart);
    }

    private void OnDestroy()
    {
        backToMenuBtn.onClick.RemoveAllListeners();
    }

    private void BackToStart()
    {
        GameManager.Instance.ResetGame();
        UIManager.Instance.OpenPanel(PanelName.StartPanel);
        gameObject.SetActive(false);
    }
}
