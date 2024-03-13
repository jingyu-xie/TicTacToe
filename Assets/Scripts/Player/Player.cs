using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string playerID;
    private MarkType playerMark;

    public Player(string playerID, MarkType playerMark)
    {
        this.playerID = playerID;
        this.playerMark = playerMark;
    }

    public MarkType PlayerMark
    {
        get { return playerMark; }
    }
}
