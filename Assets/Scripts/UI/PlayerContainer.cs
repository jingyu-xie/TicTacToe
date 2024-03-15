using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContainer : MonoBehaviour
{
    [SerializeField]
    private string playerIDText;
    [SerializeField]
    private Color playerColor;

    [SerializeField]
    private TMP_Text textHolder;

    [SerializeField]
    private Image backgroundImage;

    private void OnEnable()
    {
        GameManager.Instance.OnRoundChange.AddListener(UpdateBackgroundColor);
    }

    #region Setters
    public string PlayerIDText
    {
        set
        {
            playerIDText = value;
            textHolder.text = value;
        }
    }

    public Color PlayerColor
    {
        set
        {
            playerColor = value;
            textHolder.color = value;
        }
    }
    #endregion

    private void UpdateBackgroundColor(string playerID)
    {
        if (playerID == playerIDText)
            backgroundImage.color = Color.yellow;
        else 
            backgroundImage.color = Color.white;
    }
}
