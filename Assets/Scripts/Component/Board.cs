using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Board : Singleton<Board>
{
    [SerializeField]
    private int fieldSize = 3;

    [SerializeField]
    GameObject BlockPrefab;

    [SerializeField]
    private Block[,] board;

    private void Start()
    {
        BoardInitialization();
    }

    private void BoardInitialization()
    {
        board = new Block[fieldSize, fieldSize];
        for (int row = 0; row < fieldSize; row++)
        {
            for (int col = 0; col < fieldSize; col++)
            {
                GameObject temp = Instantiate(BlockPrefab);
                temp.GetComponent<Block>().SetUpBlock(row, col);
                board[row, col] = temp.GetComponent<Block>();

                temp.name = row + "_" + col;
                temp.transform.SetParent(transform, false);
                //board[row, col].OnCurrentMarkChange.AddListener(() => UpdateBoard());
            }
        }
    }

    public void PlaceMark(int row, int col, MarkType mark)
    {
        board[row, col].PlaceMark(mark);
    }
}
