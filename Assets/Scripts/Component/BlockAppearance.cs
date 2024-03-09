using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockAppearance : MonoBehaviour
{
    private Block block;

    [SerializeField]
    private Sprite crossMark, circleMark;
    private Image contentImage;
    private Animator ac;

    private void OnEnable()
    {
        block = GetComponentInParent<Block>();
        contentImage = GetComponent<Image>();
        ac = GetComponent<Animator>();

        if (block != null)
            block.OnCurrentContentChange.AddListener(ChangeContentAppearance);
    }

    private void OnDestroy()
    {
        block.OnCurrentContentChange.RemoveListener(ChangeContentAppearance);
    }

    private void ChangeContentAppearance(MarkType type)
    {
        switch (type)
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

        if (type != MarkType.Empty)
            ac.enabled = true;
    }
}
