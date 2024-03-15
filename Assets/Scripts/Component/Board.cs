using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Board : Singleton<Board>
{
    bool isInitialized;

    [SerializeField]
    private int boardSize = 3;

    [SerializeField]
    GameObject BlockPrefab;

    [SerializeField]
    private Block[,] boardStatus;

    [HideInInspector]
    public UnityEvent<BoardCondition> OnCheckFinished;
    private bool isPlacingMark;

    public void InitializeBoard()
    {
        if (isInitialized) return;

        boardStatus = new Block[boardSize, boardSize];
        DynamicallyChangeCells();

        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                GameObject temp = Instantiate(BlockPrefab);
                temp.GetComponent<Block>().SetUpBlock(row, col);
                boardStatus[row, col] = temp.GetComponent<Block>();

                temp.name = row + "_" + col;
                temp.transform.SetParent(transform, false);
                boardStatus[row, col].OnPlacingMark.AddListener(() => isPlacingMark = true) ;
                boardStatus[row, col].OnMarkPlaced.AddListener(() => CheckWinner());
            }
        }
        isInitialized = true;
    }

    public void PlaceMark(int row, int col, MarkType mark)
    {
        boardStatus[row, col].PlaceMark(mark);
    }

    private void CheckWinner()
    {
        isPlacingMark = false;

        if (CheckRowsAndCols() || CheckMainDiagonal(boardStatus) || CheckAntiDiagonal(boardStatus))
        {
            OnCheckFinished?.Invoke(BoardCondition.HasWinner);
            return;
        }

        if (CheckBoardFull())
        {
            OnCheckFinished?.Invoke(BoardCondition.Tie);
            return;
        }
        OnCheckFinished?.Invoke(BoardCondition.NoWinner);
    }

    public void ResetBoard()
    {
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                boardStatus[row, col].ResetBlock();
            }
        }
    }

    #region Getters

    public bool IsPlacingMark { get {  return isPlacingMark; } }

    public Block[,] BoardStatus
    {
        get { return boardStatus; }
    }

    public int BoardSize { get { return boardSize; } }
    #endregion

    #region Check Board Status Functions
    private bool CheckRowsAndCols()
    {
        for (int idx = 0; idx < boardSize; idx++)
        {
            if (CheckRow(boardStatus, idx))
                return true;
            if (CheckCol(boardStatus, idx))
                return true;
        }
        return false;
    }

    private bool CheckRow(Block[,] board, int rowNum)
    {
        MarkType[] marksArr = new MarkType[boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            marksArr[i] = board[rowNum, i].CurrentMark;
        }
        return CheckLine(marksArr);
    }

    private bool CheckCol(Block[,] board, int colNum)
    {
        MarkType[] marksArr = new MarkType[boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            marksArr[i] = board[i, colNum].CurrentMark;
        }
        return CheckLine(marksArr);
    }

    private bool CheckMainDiagonal(Block[,] board)
    {
        MarkType[] marksArr = new MarkType[boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            marksArr[i] = board[i, i].CurrentMark;
        }
        return CheckLine(marksArr);
    }

    private bool CheckAntiDiagonal(Block[,] board)
    {
        int row_cnt = 0;
        int col_cnt = boardSize - 1;
        MarkType[] marksArr = new MarkType[boardSize];

        for (int i = 0; i < boardSize; i++)
        {
            marksArr[i] = board[row_cnt++, col_cnt--].CurrentMark;
        }
        return CheckLine(marksArr);
    }

    private bool CheckLine(MarkType[] marksArr)
    {
        MarkType firstMark = marksArr[0];
        bool result = marksArr.Skip(1).All(mark => (mark != MarkType.Empty && mark == firstMark));
        return result;
    }

    public bool CheckBoardFull()
    {
        var flattenedArr = boardStatus.Cast<Block>().ToArray();
        bool result = flattenedArr.All(block => (block.CurrentMark != MarkType.Empty));
        return result;
    }

    #endregion

    #region Utilities
    private void DynamicallyChangeCells()
    {
        int cellSize = 300;
        switch (boardSize)
        {
            case < 3:
                break;
            case 4 or  5:
                cellSize = 300 - 50 * (boardSize - 3);
                break;
        }
        GetComponent<GridLayoutGroup>().constraintCount = boardSize;
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(cellSize, cellSize);
    }

    [ContextMenu("Print Board")]
    private void PrintBoard()
    {
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                Debug.Log(row + "_" + col + ": " + boardStatus[row, col].CurrentMark.ToString());
            }
        }
    }
    #endregion

}
