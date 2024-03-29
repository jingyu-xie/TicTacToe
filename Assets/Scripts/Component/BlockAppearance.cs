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
    }

    public void ChangeContentAppearance(MarkType mark)
    {
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

    public void ResetBlockAppearance()
    {
        ac.enabled = false;
        contentImage.sprite = null;
        contentImage.fillAmount = 0;

        // reset alpha
        Color tempColor = contentImage.color;
        tempColor.a = 0;
        contentImage.color = tempColor;
    }
}
