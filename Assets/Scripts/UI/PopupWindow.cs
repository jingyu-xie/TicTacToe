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
        backToMenuBtn.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    private void OnDisable()
    {
        backToMenuBtn.onClick.RemoveAllListeners();
    }
}
