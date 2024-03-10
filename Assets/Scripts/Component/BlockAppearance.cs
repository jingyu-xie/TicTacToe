using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockAppearance : MonoBehaviour
{
    [SerializeField]
    private Sprite crossMark, circleMark;
    private Image contentImage;
    private Animator ac;

    private void OnEnable()
    {
        contentImage = GetComponent<Image>();
        ac = GetComponent<Animator>();
        GetComponentInParent<Block>().OnMarkPlaced.AddListener(ChangeContentAppearance);
    }

    public void ChangeContentAppearance()
    {
        MarkType mark = GetComponentInParent<Block>().CurrentMark;

        switch (mark)
        {
            case MarkType.Empty:
                contentImage.sprite = null;
                break;
            case MarkType.Cross:
                contentImage.sprite = crossMark;
                break;
            case MarkType.Circle:
                contentImage.sprite = circleMark;
                break;
        }

        if (mark != MarkType.Empty)
            ac.enabled = true;
    }
}
