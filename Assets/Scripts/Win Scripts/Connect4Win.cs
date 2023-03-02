using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect4Win : Win
{
    public override bool isGoodMove(char color, string[,] board)
    {
        return true;
    }

    public override bool isDraw(char color, string[,] board)
    {
        int count = 0;
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        for (int r = 0; r < tRow; r++)
        {
            for (int c = 0; c < tCol; c++)
            {
                if (board[r, c][0] == color) // found a piece
                {
                    count++;
                    if(count >= 4)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    public override bool isGameOver(char color, string[,] board)
    {
        int count = 0;
        char otherColor = color == 'B' ? 'W' : 'B';
        //for(int r = 2; r <= 5; r++)
        //{
        //    count = 0;
        //    for(int c = 2; c <= 5; c++)
        //    {
        //        if(board[r,c][0] == otherColor)
        //        {
        //            count++;
        //        }
        //    }
        //    if(count == 4)
        //    {
        //        return true;
        //    }
        //}
        for (int c = 2; c <= 5; c++)
        {
            count = 0;
            for (int r = 2; r <= 5; r++)
            {
                if (board[r, c][0] == otherColor)
                {
                    count++;
                }
            }
            if (count == 4)
            {
                return true;
            }
        }
        count = 0;
        for(int rc =2; rc <= 5; rc++)
        {
            if(board[rc,rc][0] == otherColor)
            {
                count++;
            }
        }
        if(count == 4)
        {
            return true;
        }
        count = 0;
        for (int rc = 5; rc >= 2; rc--)
        {
            if (board[rc, rc][0] == otherColor)
            {
                count++;
            }
        }
        if (count == 4)
        {
            return true;
        }
        return false;
    }


}
