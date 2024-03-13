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
    public UnityEvent OnMarkPlaced;

    private Button blockBtn;

    public MarkType CurrentMark
    {
        get { return currentMark; }
    }

    public void PlaceMark(MarkType mark)
    {
        currentMark = mark;
        blockBtn.enabled = false;

        StartCoroutine(PlaceMarkCoroutine(mark));
    }

    private IEnumerator PlaceMarkCoroutine(MarkType mark)
    {
        GetComponentInChildren<BlockAppearance>().ChangeContentAppearance(currentMark);
        yield return new WaitForSeconds(1f);
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
