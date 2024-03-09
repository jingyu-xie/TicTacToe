using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private MarkType playerMark;

    public MarkType PlayerMark
    {
        get { return playerMark; }
        set { playerMark = value; }
    }
}
