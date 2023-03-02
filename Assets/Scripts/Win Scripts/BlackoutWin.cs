using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackoutWin : Win
{
    public override bool isGoodMove(char color, string[,] board)
    {
        return true;
    }

    public override bool isGameOver(char color, string[,] board)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        for(int row = 0; row < tRow; row++)
        {
            for(int col = 0; col < tCol; col++)
            {
                if(board[row,col][0] == color)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
