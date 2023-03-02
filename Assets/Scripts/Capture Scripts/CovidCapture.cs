using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovidCapture : Capture
{
    public override void movementLock(SpotBehavior spot, ref string[,] board, int selectedRow, int selectedCol)
    {
        base.movementLock(spot, ref board, selectedRow, selectedCol);

        if (world.turnNumber % 5 == 0)
        {
            int tRow = board.GetLength(0);
            int tCol = board.Length / board.GetLength(0);
            string[,] temp = new string[tRow, tCol];
            for (int i = 0; i < tRow; i++)
            {
                for (int j = 0; j < tCol; j++)
                {
                    temp[i, j] = board[i, j];
                }
            }
            System.Random rand = new System.Random();
            int randRow = rand.Next(0, tRow);
            int randCol = rand.Next(0, tCol);
            bool stillLooking = true;
            while (stillLooking)
            {
                if (board[randRow, randCol].Length == 1 || board[randRow,randCol][1] == 'K')
                {
                    randRow = rand.Next(0, tRow);
                    randCol = rand.Next(0, tCol);
                }
                else
                {
                    temp[randRow, randCol] = "E";
                    if (world.win.isGoodMove('B', board) && world.win.isGoodMove('W', board))
                    {
                        Debug.Log(board[randRow, randCol]);
                        GameObject destroyedPiece = GameObject.Find(board[randRow, randCol]);
                        destroyedPiece.SetActive(false);
                        board[randRow, randCol] = "E";
                        stillLooking = false;
                    }
                    else
                    {
                        randRow = rand.Next(0, tRow);
                        randCol = rand.Next(0, tCol);
                    }
                }
            }
        }
    }
}
