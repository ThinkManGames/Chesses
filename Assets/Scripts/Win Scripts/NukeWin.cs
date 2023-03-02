using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeWin : Win
{
    public override string getDangerousSpots(char color, int _row, int _col, string[,] board)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        string[,] toReturn = new string[tRow, tCol];
        List<string> badGuys = new List<string>();
        for (int i = 0; i < tRow; i++)
        {
            for (int j = 0; j < tCol; j++)
            {
                toReturn[i, j] = "";
            }
        }
        string rowCols = "";
        char otherColor = color == 'B' ? 'W' : 'B';
        int[,] knightMoves = world.moves.possibleMoves(color, "TN1", _row, _col, board);
        int[,] rookMoves = world.moves.possibleMoves(color, "TR1", _row, _col, board);
        int[,] bishopMoves = world.moves.possibleMoves(color, "TB1", _row, _col, board);
        int[,] kingMoves = world.moves.possibleMoves(color, "TK1", _row, _col, board);
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
                if (board[row, col][0] == otherColor && toReturn[row, col].Contains(board[row, col][1].ToString()) && !badGuys.Contains(row.ToString() + col.ToString()))
                {
                    Debug.Log("someone is attacking me");
                    rowCols += row.ToString() + col.ToString();
                    badGuys.Add(row.ToString() + col.ToString());
                }
            }
        }
        for (int i = 0; i < 8; i++)
        {
            int rowAdder = QueenKingMoves[i, 0];
            int colAdder = QueenKingMoves[i, 1];
            int currRow = _row + rowAdder;
            int currCol = _col + colAdder;
            if (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol][0] == color)
            {
                otherColor = color == 'B' ? 'W' : 'B';
                knightMoves = world.moves.possibleMoves(color, "WN1", currRow, currCol, board);
                rookMoves = world.moves.possibleMoves(color, "WR1", currRow, currCol, board);
                bishopMoves = world.moves.possibleMoves(color, "WB1", currRow, currCol, board);
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
                            if (_row - row == 1 && color == 'W')
                            {
                                toReturn[row, col] += "P";
                            }
                            else if (_row - row == -1 && color == 'B')
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
                        if (board[row, col][0] == otherColor && toReturn[row, col].Contains(board[row, col][1].ToString()) && !badGuys.Contains(row.ToString() + col.ToString()))
                        {
                            Debug.Log("someone is attacking me");
                            rowCols += row.ToString() + col.ToString();
                            badGuys.Add(row.ToString() + col.ToString());
                        }
                    }
                }
            }
        }

        return rowCols;
    }
}
