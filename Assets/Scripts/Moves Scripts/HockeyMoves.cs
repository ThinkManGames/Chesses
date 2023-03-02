using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HockeyMoves : Moves
{
    public override int[,] possibleMoves(char color, string pieceName, int row, int col, string[,] board)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        int[,] tempBoard = new int[tRow, tCol];
        char toSwitch = pieceName[1];
        if(toSwitch != 'P')
        {
            return base.possibleMoves(color, pieceName, row, col, board);
        } 
        else
        {
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
                        if (row == 3 && board[row + 2, col] == "E")
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
                        if (row == tRow - 4 && board[row - 2, col] == "E")
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
        }      
        return tempBoard;
    }
    public override void castleCheck(string[,] board)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        if (board[2, 0] == "E" || board[2, 0][1] != 'R') // if the left rook has moved, white cant castle left
        {
            wcl = false;
        }
        if (board[2, tCol - 1] == "E" || board[2, tCol - 1][1] != 'R') // if the right rook has moved, white cant castle right
        {
            wcr = false;
        }
        if (board[tRow - 3, 0][0] == 'E' || board[tRow - 3, 0][1] != 'R') // black cant castle left
        {
            bcl = false;
        }
        if (board[tRow - 3, tCol - 1][0] == 'E' || board[tRow - 3, tCol - 1][1] != 'R') // black cant castle right
        {
            bcr = false;
        }
        if (board[2, 4][0] == 'E' || board[2, 4][1] != 'K') // THIS ONLY WORKS FOR NORMAL BOARD SIZES, WILL MESS UP LARGER BOARDS
        {
            wcl = false;
            wcr = false;
        }
        if (board[tRow - 3, 4][0] == 'E' || board[tRow - 3, 4][1] != 'K') // THIS ONLY WORKS FOR NORMAL BOARD SIZES, WILL MESS UP LARGER BOARDS
        {
            bcl = false;
            bcr = false;
        }
    }
}
