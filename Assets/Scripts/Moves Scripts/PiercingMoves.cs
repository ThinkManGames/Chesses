using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingMoves : Moves
{
    public override int[,] possibleMoves(char color, string pieceName, int row, int col, string[,] board)
    {
        bool dangerousCheck = pieceName[0] == 'D';
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
                        if (col > 0 && board[row + 1, col - 1][0] == 'B')
                        {
                            tempBoard[row + 1, col - 1] = 1;
                        }
                        // if the pawn is not on the right edge and the right spot is either empty or has a black piece
                        if (col < tCol - 1 && board[row + 1, col + 1][0] == 'B')
                        {
                            tempBoard[row + 1, col + 1] = 1;
                        }
                        if (board[row + 1, col] == "E")
                        {
                            if (row == 1 && board[row + 2, col] == "E")
                            {
                                tempBoard[row + 2, col] = 1;
                            }
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
                        if (col > 0 && board[row - 1, col - 1][0] == 'W')
                        {
                            tempBoard[row - 1, col - 1] = 1;
                        }
                        // if the pawn is not on the right edge and the right spot is either empty or has a black piece
                        if (col < tCol - 1 && board[row - 1, col + 1][0] == 'W')
                        {
                            tempBoard[row - 1, col + 1] = 1;
                        }
                        if (board[row - 1, col] == "E")
                        {
                            if (row == tRow - 2 && board[row - 2, col] == "E")
                            {
                                tempBoard[row - 2, col] = 1;
                            }
                            tempBoard[row - 1, col] = 1;
                        }
                    }
                    else
                    {
                        // this should never happen if we implement pawn upgrades
                    }
                }
                break;
            case 'N':

                for (int i = 0; i < 8; i++)
                {
                    if (row + knightMoves[i, 0] >= 0 && row + knightMoves[i, 0] < tRow && col + knightMoves[i, 1] >= 0 && col + knightMoves[i, 1] < tCol && board[row + knightMoves[i, 0], col + knightMoves[i, 1]][0] != color && board[row + knightMoves[i, 0], col + knightMoves[i, 1]] != "0")
                    {
                        tempBoard[row + knightMoves[i, 0], col + knightMoves[i, 1]] = 1;
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

                    while (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol][0] != color && board[currRow, currCol] != "0")
                    {
                        tempBoard[currRow, currCol] = 1;
                        currRow += rowAdder;
                        currCol += colAdder;
                    }
                    if (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && dangerousCheck && board[currRow, currCol][0] == color)
                    {
                        tempBoard[currRow, currCol] = 1;
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
                    while (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol][0] != color && board[currRow, currCol] != "0")
                    {
                        tempBoard[currRow, currCol] = 1;
                        currRow += rowAdder;
                        currCol += colAdder;
                    }
                    if (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && dangerousCheck && board[currRow, currCol][0] == color)
                    {
                        tempBoard[currRow, currCol] = 1;
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
                    while (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol][0] != color && board[currRow, currCol] != "0")
                    {
                        tempBoard[currRow, currCol] = 1;
                        currRow += rowAdder;
                        currCol += colAdder;
                    }
                    if (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && dangerousCheck && board[currRow, currCol][0] == color)
                    {
                        tempBoard[currRow, currCol] = 1;
                    }
                }
                break;
            case 'K':
                for (int i = 0; i < 8; i++)
                {
                    int rowAdder = QueenKingMoves[i, 0];
                    int colAdder = QueenKingMoves[i, 1];
                    int currRow = row + rowAdder;
                    int currCol = col + colAdder;
                    if (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol][0] != color && board[currRow, currCol] != "0")
                    {
                        tempBoard[currRow, currCol] = 1;
                    }
                }

                break;
        }
        return tempBoard;

    }
}
