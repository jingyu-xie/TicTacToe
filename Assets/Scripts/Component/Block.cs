using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum MarkType
{
    Empty, Cross, Circle
}

public class Block : MonoBehaviour
{
    [SerializeField]
    MarkType currentMark;

    BlockAppearance appearance;
    private Button blockBtn;
    private int row, col;

    private void OnBlockClicked()
    {
        PlaceMark(MarkType.Circle);
    }

    public void PlaceMark(MarkType type)
    {
        currentMark = type;
        blockBtn.enabled = false;
        appearance.ChangeContentAppearance(currentMark);
    }

    public void SetUpBlock(int row, int col)
    {
        this.row = row;
        this.col = col;

        appearance = GetComponentInChildren<BlockAppearance>();
        blockBtn = GetComponent<Button>();
        blockBtn.onClick.AddListener(OnBlockClicked);
    }
}
