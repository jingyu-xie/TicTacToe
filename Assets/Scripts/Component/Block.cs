using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField]
    MarkType currentMark;
    private int row, col;

    [HideInInspector]
    public UnityEvent OnPlacingMark, OnMarkPlaced;

    private Button blockBtn;

    public MarkType CurrentMark
    {
        get { return currentMark; }
        set { currentMark = value; }
    }

    #region Place Mark Functions
    public void PlaceMark(MarkType mark)
    {
        if (Board.Instance.IsPlacingMark) return;

        currentMark = mark;
        blockBtn.enabled = false;

        StartCoroutine(PlaceMarkCoroutine(mark));
    }

    private IEnumerator PlaceMarkCoroutine(MarkType mark)
    {
        OnPlacingMark?.Invoke();
        GetComponentInChildren<BlockAppearance>().ChangeContentAppearance(currentMark);
        yield return new WaitForSeconds(1f);
        OnMarkPlaced?.Invoke();
    }
    #endregion

    public void SetUpBlock(int row, int col)
    {
        this.row = row;
        this.col = col;

        blockBtn = GetComponent<Button>();
        blockBtn.onClick.AddListener(() => PlaceMark(GameManager.Instance.getCurrentPlayerMark()));
    }

    public void ResetBlock()
    {
        currentMark = MarkType.Empty;
    }
}
