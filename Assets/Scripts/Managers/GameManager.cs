using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private GameMode mode;

    private Player currentPlayer;
    private List<Player> playerList = new List<Player>(2);

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void StartGame(GameMode mode, int boardSize)
    {
        Debug.Log("Start Game: " + mode.ToString() + ", with size: " + boardSize);
        Board.Instance.InitializeBoard(boardSize);

        switch (mode)
        {
            case GameMode.PVP:
                break;

            case GameMode.PVE:
                
                break;
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
