using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TntRunMoves : Moves
{
    public override int[,] possibleMoves(char color, string pieceName, int row, int col, string[,] board)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        int[,] tempBoard = new int[tRow, tCol];
        for (int i = 0; i < 8; i++)
        {
            if (row + knightMoves[i, 0] >= 0 && row + knightMoves[i, 0] < tRow && col + knightMoves[i, 1] >= 0 && col + knightMoves[i, 1] < tCol && board[row + knightMoves[i, 0], col + knightMoves[i, 1]] == "E" && world.boardLoader.extraBoard[row + knightMoves[i, 0], col + knightMoves[i, 1]] != 1)
            {
                tempBoard[row + knightMoves[i, 0], col + knightMoves[i, 1]] = 1;
            }
        }

        
        return tempBoard;

    }
}
