using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushWin : Win
{
    public override bool isGoodMove(char color, string[,] board)
    {
        return !isGameOver(color, board);
    }

    public override bool isGameOver(char color, string[,] board)
    {
        int nRow = board.GetLength(0);
        int nCol = board.Length / nRow;
        for (int r = 0; r < nRow; r++)
        {
            for (int c = 0; c < nCol; c++)
            {
                if (board[r, c][0] == color && board[r,c][1] == 'K')
                {
                    return false;
                }
            }
        }
        return true;
    }
}
