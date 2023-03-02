using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyWin : Win
{
    public override bool isGoodMove(char color, string[,] board)
    {
        return true;
    }

    public override bool isGameOver(char color, string[,] board)
    {
        int nRow = board.GetLength(0);
        int nCol = board.Length / nRow;
        for(int r = 0; r < nRow; r++)
        {
            for(int c = 0; c < nCol; c++)
            {
                if(board[r,c] == world.pregame.getWin(color))
                {
                    return false;
                }
            }
        }
        return true;
    }

}
