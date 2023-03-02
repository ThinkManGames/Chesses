using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TntRunWin : Win
{
    public override bool isGoodMove(char color, string[,] board)
    {
        return true;
    }
    public override bool isGameOver(char color, string[,] board)
    {
        int nRow = board.GetLength(0);
        int nCol = board.Length / nRow;
        int knightCol = -1;
        int knightRow = -1;
        for (int r = 0; r < nRow; r++)
        {
            for (int c = 0; c < nCol; c++)
            {
                if (board[r, c][0] == color)
                {
                    knightRow = r;
                    knightCol = c;
                    break;
                }
            }
            if (knightRow != -1)
            {
                break;
            }
        }
        if (board == world.board)
        {
            world.boardLoader.extraBoard[knightRow, knightCol] = 1;
        }
        int[,] temp = world.getPossibleMoves(color, "WN1", knightRow, knightCol);
        for (int r = 0; r < nRow; r++)
        {
            for (int c = 0; c < nCol; c++)
            {
                if (temp[r, c] == 1)
                {
                    return false;
                }
            }
        }
        return true;
    }

}
