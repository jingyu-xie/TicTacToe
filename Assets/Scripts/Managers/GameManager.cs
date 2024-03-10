using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    PVP, PVE
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    Board board;
    GameMode mode;

    Player currentPlayer;
    List<Player> players;

    private void Start()
    {
        board.Initialization();
    }

    private void Update()
    {
        
    }
}
