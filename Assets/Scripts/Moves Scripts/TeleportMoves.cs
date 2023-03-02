using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMoves : Moves
{
    public override int[,] possibleMoves(char color, string pieceName, int row, int col, string[,] board)
    {

        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        if(board[row,col][1] == 'K')
        {
            return base.possibleMoves(color, pieceName, row, col, board);
        }
        int[,] toReturn = base.possibleMoves(color, pieceName, row, col, board);
        for(int r = 0; r < tRow; r++)
        {
            for(int c = 0; c < tCol; c++)
            {
                // cant switch with the other color, with ourselves, or with our king
                if(board[r,c][0] == color && board[r,c] != board[row,col] && board[r,c][1] != 'K')
                {
                    toReturn[r, c] = 1;
                }
            }
        }
        return toReturn;
    }

}
