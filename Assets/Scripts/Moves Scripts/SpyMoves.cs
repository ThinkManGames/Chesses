using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyMoves : Moves
{
    public override int[,] possibleMoves(char color, string pieceName, int row, int col, string[,] board)
    {
 
        if(pieceName == "start") // means we are doing some pregame thing
        {
            int[,] toReturn = new int[row, col];
            for (int r = 0; r < row; r++) // row is max rows
            {
                for(int c = 0; c < col; c++) // col is max cols
                {
                    if(board[r,c][0] == color)
                    {
                        toReturn[r, c] = 1;
                    }
                }
            }
            return toReturn;
        }
        else
        {
            return base.possibleMoves(color, pieceName, row, col, board);
        }
    }
}
