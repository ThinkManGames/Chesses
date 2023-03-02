using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineBoard : Board
{
    [SerializeField] int landmineNumber = 6; 

    public override string[,] getBoard()
    {
        System.Random rand = new System.Random();

        for (int i = 0; i < landmineNumber; i++)
        {
            extraBoard[rand.Next(0, 7), rand.Next(0, 7)] = 1;
        }

        return base.getBoard();

    }
}
