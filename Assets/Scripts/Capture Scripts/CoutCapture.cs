using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoutCapture : Capture
{
    int takeChance = 0;
    public override string[,] movementCheck(SpotBehavior spot, string[,] board, int selectedRow, int selectedCol)
    {
        int points = 30;
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        string[,] temp = new string[tRow, tCol];
        for (int i = 0; i < tRow; i++)
        {
            for (int j = 0; j < tCol; j++)
            {
                temp[i, j] = board[i, j];
                if(board[i,j][0] == board[selectedRow,selectedCol][0])
                {
                    if(board[i,j][1] == 'Q')
                    {
                        points += 9;
                    }
                    if (board[i, j][1] == 'R')
                    {
                        points += 5;
                    }
                    if (board[i, j][1] == 'B')
                    {
                        points += 3;
                    }
                    if (board[i, j][1] == 'N')
                    {
                        points += 3;
                    }
                    if (board[i, j][1] == 'P')
                    {
                        points += 1;
                    }
                }
            }
        }
        System.Random rand = new System.Random();
        int checkpoint = rand.Next(0, 100);
        if(checkpoint >= points && board[selectedRow, selectedCol][1] != 'K')
        {
            int[,] moves = world.getPossibleMoves(board[selectedRow, selectedCol][0], board[selectedRow, selectedCol], selectedRow, selectedCol);
            int options = 0;
            for(int r = 0; r < tRow; r++)
            {
                for(int c = 0; c < tCol; c++)
                {
                    if(moves[r,c] == 1)
                    {
                        options++;
                    }
                }
            }
            if(options > 1)
            {
                takeChance = rand.Next(1, options);
                int toSubtract = takeChance;
                bool exitLoop = false;
                for (int r = 0; r < tRow && !exitLoop; r++)
                {
                    for (int c = 0; c < tCol && !exitLoop; c++)
                    {
                        if (moves[r, c] == 1)
                        {
                            if (toSubtract != 0)
                            {
                                toSubtract--;
                            }
                            else
                            {
                                temp[r, c] = temp[selectedRow, selectedCol];
                                temp[selectedRow, selectedCol] = "E";
                                exitLoop = true;
                            }
                        }
                    }
                }
            } else
            {
                takeChance = 0;
                temp[spot.row, spot.col] = temp[selectedRow, selectedCol];
                temp[selectedRow, selectedCol] = "E";
            }

        } else
        {
            takeChance = 0;
            temp[spot.row, spot.col] = temp[selectedRow, selectedCol];
            temp[selectedRow, selectedCol] = "E";
        }
        return temp;
    }

    public override void movementLock(SpotBehavior spot, ref string[,] board, int selectedRow, int selectedCol)
    {
        if (takeChance != 0)
        {
            int[,] moves = world.getPossibleMoves(board[selectedRow, selectedCol][0], board[selectedRow, selectedCol], selectedRow, selectedCol);
            int nRow = board.GetLength(0);
            int nCol = board.Length / board.GetLength(0);
            int toSubtract = takeChance;
            bool exitLoop = false;
            for (int r = 0; r < nRow && !exitLoop; r++)
            {
                for (int c = 0; c < nCol && !exitLoop; c++)
                {
                    if (moves[r, c] == 1)
                    {
                        if (toSubtract != 0)
                        {
                            toSubtract--;
                        }
                        else
                        {
                            string spotName = char.ConvertFromUtf32(c + 65) + " (" + (r + 1).ToString() + ")";
                            spot = GameObject.Find(spotName).GetComponent<SpotBehavior>();
                            world.showOldMove[1] = spotName + "S";
                            exitLoop = true;

                        }
                    }
                }
            }
            takeChance = 0;
        }

        base.movementLock(spot, ref board, selectedRow, selectedCol);
    }
    
}
