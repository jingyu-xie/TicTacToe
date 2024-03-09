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

    [HideInInspector]
    public UnityEvent<MarkType> OnCurrentContentChange;

    private void OnEnable()
    {
        blockBtn = GetComponent<Button>();
        blockBtn.onClick.AddListener(OnBlockClicked);
    }

    private void OnBlockClicked()
    {
        CurrentContent = MarkType.Cross;
    }

    private void PlaceMark(MarkType type)
    {
        CurrentContent = type;
    }

    public MarkType CurrentContent
    {
        get { return currentMark; }
        set { 
            currentMark = value;
            OnCurrentContentChange?.Invoke(currentMark);
        }
    }

    private void OnValidate()
    {
        OnCurrentContentChange?.Invoke(currentMark);
    }
}
