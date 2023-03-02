using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushMoves : Moves
{
    public override int[,] possibleMoves(char color, string pieceName, int row, int col, string[,] board)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        int[,] tempBoard = new int[tRow, tCol];
        char toSwitch = pieceName[1];
        switch (toSwitch)
        {
            case 'P':
                if (color == 'W')
                {
                    if (row < tRow - 1)
                    {
                        // if the pawn is not on the left edge and the left spot either is empty or has a black piece
                        if (col > 0 && board[row + 1, col - 1].Length > 1)
                        {
                            tempBoard[row + 1, col - 1] = 1;
                        }
                        // if the pawn is not on the right edge and the right spot is either empty or has a black piece
                        if (col < tCol - 1 && board[row + 1, col + 1].Length > 1)
                        {
                            tempBoard[row + 1, col + 1] = 1;
                        }
                        if (board[row + 1, col] != "0")
                        {
                            tempBoard[row + 1, col] = 1;
                        }
                    }
                    else
                    {
                        // this should never happen if we implement pawn upgrades
                    }
                }
                else if (color == 'B')
                {
                    if (row > 0)
                    {
                        // if the pawn is not on the left edge and the left spot either is empty or has a black piece
                        if (col > 0 && board[row - 1, col - 1].Length > 1)
                        {
                            tempBoard[row - 1, col - 1] = 1;
                        }
                        // if the pawn is not on the right edge and the right spot is either empty or has a black piece
                        if (col < tCol - 1 && board[row - 1, col + 1].Length > 1)
                        {
                            tempBoard[row - 1, col + 1] = 1;
                        }
                        if (board[row - 1, col] != "0")
                        {

                            tempBoard[row - 1, col] = 1;
                        }
                    }
                    else
                    {
                        // this should never happen if we implement pawn upgrades
                    }
                }
                break;
            case 'B':
                for (int i = 0; i < 4; i++)
                {
                    int rowAdder = BishopMoves[i, 0];
                    int colAdder = BishopMoves[i, 1];
                    int currRow = row + rowAdder;
                    int currCol = col + colAdder;

                    while (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol] != "0")
                    {
                        tempBoard[currRow, currCol] = 1;
                        if (board[currRow, currCol][0] != 'E')
                        {
                            break;
                        }
                        currRow += rowAdder;
                        currCol += colAdder;
                    }
                }
                break;
            case 'R':
                for (int i = 0; i < 4; i++)
                {
                    int rowAdder = RookMoves[i, 0];
                    int colAdder = RookMoves[i, 1];
                    int currRow = row + rowAdder;
                    int currCol = col + colAdder;
                    while (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol] != "0")
                    {
                        tempBoard[currRow, currCol] = 1;
                        if (board[currRow, currCol][0] != 'E')
                        {
                            break;
                        }
                        currRow += rowAdder;
                        currCol += colAdder;
                    }
                }
                break;
            case 'Q':
                for (int i = 0; i < 8; i++)
                {
                    int rowAdder = QueenKingMoves[i, 0];
                    int colAdder = QueenKingMoves[i, 1];
                    int currRow = row + rowAdder;
                    int currCol = col + colAdder;
                    while (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol] != "0")
                    {
                        tempBoard[currRow, currCol] = 1;
                        if (board[currRow, currCol][0] != 'E')
                        {
                            break;
                        }
                        currRow += rowAdder;
                        currCol += colAdder;
                    }
                }
                break;
        }
        return tempBoard;

    }
}
