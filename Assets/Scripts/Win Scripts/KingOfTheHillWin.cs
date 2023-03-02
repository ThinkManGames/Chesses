using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KingOfTheHillWin : Win
{
    public int whiteScore = 0;
    public int blackScore = 0;
    private int[,] squaresToCheck = new int[4, 2]
    {
        {3,3 },
        {3,4 },
        {4,3 },
        {4,4 }
    };
    public override bool isGoodMove(char color, string[,] board)
    {
        return true;
    }

    public override bool isGameOver(char color, string[,] board)
    {
        if(world.turnNumber == 1)
        {
            whiteScore = 0;
            blackScore = 0;
        }
        for(int i = 0; i < 4; i++)
        {
            if(board[squaresToCheck[i,0], squaresToCheck[i,1]][0] == 'W' && color == 'B') 
            {
                whiteScore++;
                GameObject.Find("Points").GetComponent<Text>().text = "Your Score " + blackScore;
            }
            if (board[squaresToCheck[i, 0], squaresToCheck[i, 1]][0] == 'B' && color == 'W')
            {
                blackScore++;
                GameObject.Find("Points").GetComponent<Text>().text = "Your Score " + whiteScore;
            }
        }
        if(color == 'B' && whiteScore >= 30 && blackScore < 30)
        {
            return true;
        }
        if(color == 'W' && blackScore >= 30 && whiteScore < 30)
        {
            return true;
        }
        return false;
    }

    public override bool isDraw(char color, string[,] board)
    {
        return whiteScore >= 30 && blackScore >= 30;
    }
}
