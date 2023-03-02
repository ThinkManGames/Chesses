using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingKingsWin : Win
{
    public override bool isGoodMove(char color, string[,] board)
    {
        if (color == 'W')
        {
            Vector2 rowCol = findKingSpot('B', board);
            int row = (int)rowCol.x;
            if (row == -1)
            {
                return false;
            } else
            {
                return true;
            }
        }
        else if (color == 'B')
        {
            Vector2 rowCol = findKingSpot('W', board);
            int row = (int)rowCol.x;
            if (row == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false; // should never happen
    }

    public override bool isGameOver(char color, string[,] board)
    {
        world.rotatingBoard = false;
        if (color == 'W')
        {
            Vector2 rowCol = findKingSpot('B', board);
            int row = (int)rowCol.x;
            print(row);
            if (row == 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (color == 'B')
        {
            Vector2 rowCol = findKingSpot('W', board);
            int row = (int)rowCol.x;
            if (row == 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        print("this should never happen");
        return false; // should never happen
    }
}
