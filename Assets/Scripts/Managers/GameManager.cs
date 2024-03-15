using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Board board;
    private GameMode mode;
    private bool goFirst;

    private Player currentPlayer = null;
    private List<Player> playersList = new List<Player>(2);

    [SerializeField]
    private GameObject AIPlayer;

    [HideInInspector]
    public UnityEvent<string> OnRoundChange;

    private void Start()
    {
        board.OnCheckFinished.AddListener(CheckResult);
        DontDestroyOnLoad(this);
    }

    public void StartGame(GameMode mode, bool goFirst)
    {
        board.InitializeBoard();
        // set up mode
        switch (mode)
        {
            case GameMode.PVP:
                SetUpPVP();
                break;

            case GameMode.PVE:
                SetUpPVE();
                break;
        }
        // set up first player turn
        if (goFirst)
        {
            currentPlayer = playersList[1];
            this.goFirst = goFirst;
        }      
        Debug.Log("Start Game: " + mode.ToString() + ", Go First: " + goFirst);
        NewRound();
    }

    #region Set Ups
    private void SetUpPVP()
    {
        Player player1 = new Player("Player1", MarkType.Circle);
        Player player2 = new Player("Player2", MarkType.Cross);
        playersList.Add(player1);
        playersList.Add(player2);
        UIManager.Instance.SetUpPlayerContainers(playersList);
    }

    private void SetUpPVE()
    {
        Player player1 = new Player("Player1", MarkType.Circle);
        Player player_ai = new Player("AI Player", MarkType.Cross);
        playersList.Add(player1);
        playersList.Add(player_ai);

        GameObject temp = Instantiate(AIPlayer);
        AIPlayer aiPlayer = temp.GetComponent<AIPlayer>();
        aiPlayer.InitializeAI(player_ai.PlayerID, player_ai.PlayerMark, player1.PlayerMark);

        UIManager.Instance.SetUpPlayerContainers(playersList);
    }
    #endregion

    #region New Round & Check Result
    private void CheckResult(BoardCondition condition)
    {
        switch (condition)
        {
            case BoardCondition.NoWinner:
                NewRound();
                break;
            case BoardCondition.HasWinner:
                Debug.Log(currentPlayer.PlayerID + " win the game");
                UIManager.Instance.PopUpWindow(currentPlayer.PlayerID + " win the game");
                Time.timeScale = 0;
                break;
            case BoardCondition.Tie:
                Debug.Log("Tie");
                UIManager.Instance.PopUpWindow("Tie");
                Time.timeScale = 0;
                break;
        }
    }

    private void NewRound()
    {
        if (currentPlayer == playersList[1])
            currentPlayer = playersList[0];
        else
            currentPlayer = playersList[1];

        Debug.Log("Current Player: " + currentPlayer.PlayerID);
        OnRoundChange?.Invoke(currentPlayer.PlayerID);
    }
    #endregion

    #region Getters
    public MarkType getCurrentPlayerMark()
    {
        return currentPlayer.PlayerMark;
    }

    public bool PlayerGoesFirst
    {
        get { return goFirst; }
    }
    #endregion

    public void quitGame()
    {
        Application.Quit();
    }
}
