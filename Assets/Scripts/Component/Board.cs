using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Board : Singleton<Board>
{
    [SerializeField]
    private int boardSize = 3;

    [SerializeField]
    GameObject BlockPrefab;

    [SerializeField]
    private Block[,] board;

    public void Initialization()
    {
        board = new Block[boardSize, boardSize];
        DynamicallyChangeCells();

        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                GameObject temp = Instantiate(BlockPrefab);
                temp.GetComponent<Block>().SetUpBlock(row, col);
                board[row, col] = temp.GetComponent<Block>();

                temp.name = row + "_" + col;
                temp.transform.SetParent(transform, false);
                board[row, col].OnMarkPlaced.AddListener(CheckWinner);
            }
        }
    }

    public void PlaceMark(int row, int col, MarkType mark)
    {
        board[row, col].PlaceMark(mark);
    }

    private void CheckWinner()
    {
        if (CheckRowsAndCols() || CheckMainDiagonal(board) || CheckAntiDiagonal(board))
        {
            // Inform GameManager
            Debug.Log("sb wins");
            return;
        }
        Debug.Log("Nb win");
    }

    #region Check Board Status Functions
    private bool CheckRowsAndCols()
    {
        for (int idx = 0; idx < boardSize; idx++)
        {
            if (CheckRow(board, idx))
                return true;
            if (CheckCol(board, idx))
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
        return CheckAllElementEqual(marksArr);
    }

    private bool CheckCol(Block[,] board, int colNum)
    {
        MarkType[] marksArr = new MarkType[boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            marksArr[i] = board[i, colNum].CurrentMark;
        }
        return CheckAllElementEqual(marksArr);
    }

    private bool CheckMainDiagonal(Block[,] board)
    {
        MarkType[] marksArr = new MarkType[boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            marksArr[i] = board[i, i].CurrentMark;
        }
        return CheckAllElementEqual(marksArr);
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
        return CheckAllElementEqual(marksArr);
    }

    private bool CheckAllElementEqual(MarkType[] marksArr)
    {
        MarkType firstMark = marksArr[0];
        bool result = marksArr.Skip(1).All(mark => (mark != MarkType.Empty && mark == firstMark));
        return result;
    }

    #endregion

    #region Utilities
    private void DynamicallyChangeCells()
    {
        int cellSize = 300;
        switch (boardSize)
        {
            case <= 3 or > 10:
                break;
            case >= 4 and < 7:
                cellSize = 300 - 50 * (boardSize - 3);
                break;
            case >= 7 and <= 10:
                cellSize = 125;
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
                Debug.Log(row + "_" + col + ": " + board[row, col].CurrentMark.ToString());
            }
        }
    }
    #endregion

}
