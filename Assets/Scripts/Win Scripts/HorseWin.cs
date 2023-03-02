using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseWin : Win
{
    public override string getDangerousSpots(char color, int _row, int _col, string[,] board)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        string[,] toReturn = new string[tRow, tCol];
        for (int i = 0; i < tRow; i++)
        {
            for (int j = 0; j < tCol; j++)
            {
                toReturn[i, j] = "";
            }
        }

        string rowCols = "";
        char otherColor = color == 'B' ? 'W' : 'B';
        int[,] knightMoves = world.moves.possibleMoves(otherColor, "TN1", _row, _col, board);
        int[,] rookMoves = world.moves.possibleMoves(otherColor, "TR1", _row, _col, board);
        int[,] bishopMoves = world.moves.possibleMoves(otherColor, "TB1", _row, _col, board);
        int[,] kingMoves = world.moves.possibleMoves(otherColor, "TK1", _row, _col, board);
        for (int row = 0; row < tRow; row++)
        {
            for (int col = 0; col < tCol; col++)
            {
                if (knightMoves[row, col] == 1)
                {
                    toReturn[row, col] += "N";
                }
                if (rookMoves[row, col] == 1)
                {
                    toReturn[row, col] += "RQ";
                }
                if (bishopMoves[row, col] == 1)
                {
                    if (_row - row == 1 && color == 'B')
                    {
                        toReturn[row, col] += "P";
                    }
                    else if (_row - row == -1 && color == 'W')
                    {
                        toReturn[row, col] += "P";
                    }
                    if (!toReturn[row, col].Contains("Q"))
                    {
                        toReturn[row, col] += "BQ";
                    }
                    else
                    {
                        toReturn[row, col] += "B";
                    }
                }
                if (kingMoves[row, col] == 1)
                {
                    toReturn[row, col] += "K";
                }
                if (board[row, col][0] == otherColor && toReturn[row, col].Contains(board[row, col][1].ToString()))
                {
                    rowCols += row.ToString() + col.ToString();
                }
            }
        }
        return rowCols;
    }
}
