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

    private Button blockBtn;
    private int row, col;

    [HideInInspector]
    public UnityEvent OnMarkPlaced;

    public MarkType CurrentMark
    {
        get { return currentMark; }
    }

    public void PlaceMark(MarkType mark)
    {
        currentMark = mark;
        blockBtn.enabled = false;
        OnMarkPlaced?.Invoke();
    }

    public void SetUpBlock(int row, int col)
    {
        this.row = row;
        this.col = col;

        blockBtn = GetComponent<Button>();
        blockBtn.onClick.AddListener(() => PlaceMark(MarkType.Circle));
    }
}
