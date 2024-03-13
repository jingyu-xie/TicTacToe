using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField]
    private PanelName panelName;

    public PanelName PanelName
    {
        get { return panelName; }
    }
}
