using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    private Board board;
    private Block[,] currentBoardStatus;
    private int[] currentBestMove;

    private string playerID;
    private MarkType aiMark;
    private MarkType opponentMark;

    public void InitializeAI(string playerID, MarkType aiMark, MarkType opponentMark)
    {
        this.playerID = playerID;
        this.aiMark = aiMark;
        this.opponentMark = opponentMark;

        board = Board.Instance;
        currentBoardStatus = board.BoardStatus;
        currentBestMove = new int[2];
        GameManager.Instance.OnRoundChange.AddListener(CheckIsInAIRound);
    }

    private void CheckIsInAIRound(string inRoundPlayerID)
    {
        if (inRoundPlayerID != playerID) return;

        currentBestMove = FindBestMove();
        board.PlaceMark(currentBestMove[0], currentBestMove[1], aiMark);
    }

    private int[] FindBestMove()
    {
        currentBoardStatus = board.BoardStatus;
        int[] bestMove = new int[2] { -1, -1};
        int bestVal = int.MinValue;

        for (int row = 0; row < board.BoardSize; row++)
        {
            for (int col = 0; col < board.BoardSize; col++)
            {
                if (currentBoardStatus[row, col].CurrentMark == MarkType.Empty)
                {
                    // try movement
                    currentBoardStatus[row, col].CurrentMark = aiMark;
                    // evaluate movement score
                    int moveVal = Minimax(currentBoardStatus, 0, !GameManager.Instance.PlayerGoesFirst, int.MinValue, int.MaxValue);
                    // reset movement
                    currentBoardStatus[row, col].CurrentMark = MarkType.Empty;

                    if (moveVal > bestVal)
                    {
                        bestMove[0] = row;
                        bestMove[1] = col;
                        bestVal = moveVal;
                    }
                }
            }
        }
        return bestMove;
    }

    #region Minimax Algorithm

    private int Minimax(Block[,] currentBoardStatus, int depth, bool isMax, int alpha, int beta)
    {
        int score = EvaluateBoard();

        if (score != 0 || !board.CheckBoardFull())
            return score;

        if (isMax)
        {
            int best = int.MinValue;
            for (int row = 0; row < board.BoardSize; row++)
            {
                for (int col = 0; col < board.BoardSize; col++)
                {
                    if (currentBoardStatus[row, col].CurrentMark == MarkType.Empty)
                    {
                        currentBoardStatus[row, col].CurrentMark = aiMark;
                        best = Math.Max(best, Minimax(currentBoardStatus, depth + 1, !isMax, alpha, beta));
                        currentBoardStatus[row, col].CurrentMark = MarkType.Empty; 

                        alpha = Math.Max(alpha, best);
                        if (beta <= alpha)
                            break;
                    }
                }
                if (beta <= alpha)
                    break;
            }
            return best;
        }
        else
        {
            int best = int.MaxValue;
            for (int row = 0; row < board.BoardSize; row++)
            {
                for (int col = 0; col < board.BoardSize; col++)
                {
                    if (currentBoardStatus[row, col].CurrentMark == MarkType.Empty)
                    {
                        currentBoardStatus[row, col].CurrentMark = opponentMark;
                        best = Math.Min(best, Minimax(currentBoardStatus, depth + 1, !isMax, alpha, beta));
                        currentBoardStatus[row, col].CurrentMark = MarkType.Empty; 

                        beta = Math.Min(beta, best);
                        if (beta <= alpha)
                            break;
                    }
                }
                if (beta <= alpha)
                    break;
            }
            return best;
        }
    }
    #endregion

    #region Score Evaluation Functions
    private int EvaluateBoard()
    {
        int score = 0;

        // middle point score
        if (currentBoardStatus[1, 1].CurrentMark == aiMark) score += 3;
        else if (currentBoardStatus[1, 1].CurrentMark != MarkType.Empty) score -= 3;
        // corners score
        score += EvaluateCorners();
        // row, col, diagonal score
        score += EvaluateLines();

        return score;
    }

    private int EvaluateCorners()
    {
        int cornerScore = 0;
        MarkType[] corners = new MarkType[] {
            currentBoardStatus[0, 0].CurrentMark,
            currentBoardStatus[0, 2].CurrentMark,
            currentBoardStatus[2, 0].CurrentMark,
            currentBoardStatus[2, 2].CurrentMark
        };

        foreach (var corner in corners)
        {
            if (corner == aiMark) cornerScore += 2;
            else if (corner != MarkType.Empty) cornerScore -= 2;
        }

        return cornerScore;
    }

    private int EvaluateLines()
    {
        int lineScore = 0;

        // row and col score
        for (int i = 0; i < 3; i++)
        {
            lineScore += EvaluateRow(i);
            lineScore += EvaluateCol(i);
        }
        // diagonal score
        lineScore += EvaluateDiagonals();

        return lineScore;
    }

    private int EvaluateRow(int row)
    {
        int rowScore = 0;
        MarkType[] marksArr = new MarkType[] { 
            currentBoardStatus[row, 0].CurrentMark, 
            currentBoardStatus[row, 1].CurrentMark, 
            currentBoardStatus[row, 2].CurrentMark 
        };
        rowScore += EvaluateLine(marksArr);
        return rowScore;
    }

    private int EvaluateCol(int col)
    {
        int colScore = 0;
        MarkType[] marksArr = new MarkType[] { 
            currentBoardStatus[0, col].CurrentMark, 
            currentBoardStatus[1, col].CurrentMark, 
            currentBoardStatus[2, col].CurrentMark 
        };
        colScore += EvaluateLine(marksArr);
        return colScore;
    }

    private int EvaluateDiagonals()
    {
        int diagonalsScore = 0;

        diagonalsScore += EvaluateLine(new MarkType[] { 
            currentBoardStatus[0, 0].CurrentMark, 
            currentBoardStatus[1, 1].CurrentMark, 
            currentBoardStatus[2, 2].CurrentMark 
        });
        diagonalsScore += EvaluateLine(new MarkType[] { 
            currentBoardStatus[0, 2].CurrentMark, 
            currentBoardStatus[1, 1].CurrentMark, 
            currentBoardStatus[2, 0].CurrentMark 
        });

        return diagonalsScore;
    }

    private int EvaluateLine(MarkType[] line)
    {
        int score = 0;
        int aiMarksCnt = 0;
        int opponentMarksCnt = 0;

        foreach (var mark in line)
        {
            if (mark == aiMark) aiMarksCnt++;
            else if (mark == opponentMark) opponentMarksCnt++;
        }

        if (aiMarksCnt == 3) score += 100;  // AI wins
        else if (opponentMarksCnt == 3) score -= 100;  // Opponent wins
        else if (aiMarksCnt == 2 && opponentMarksCnt == 0) score += 50;  // AI close to win
        else if (opponentMarksCnt == 2 && aiMarksCnt == 0) score -= 60;  // Opponent close to win
        else if (aiMarksCnt == 1 && opponentMarksCnt == 0) score += 10;
        else if (opponentMarksCnt == 1 && aiMarksCnt == 0) score -= 10;

        return score;
    }
    #endregion
}
