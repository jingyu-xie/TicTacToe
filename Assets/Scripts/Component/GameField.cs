using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField]
    private int fieldSize = 3;

    [SerializeField]
    GameObject BlockPrefab;

    private Block[,] field;

/*    private void Start()
    {
        FieldInitialization();
    }

    private void FieldInitialization()
    {
        field = new Block[fieldSize, fieldSize];
        for (int row = 0; row < fieldSize; row++)
        {
            for (int col = 0; col < fieldSize; col++)
            {
                GameObject temp = Instantiate(BlockPrefab);
                temp.transform.parent = transform;
                field[row, col] = temp.GetComponent<Block>();
            }
        }
    }*/
}
