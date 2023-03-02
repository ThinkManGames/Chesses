using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBoard : Board
{
    public override string[,] getBoard()
    {

        string[,] notRandom = base.getBoard();
        System.Random rand = new System.Random();
        for (int i = 0; i < 32; i++)
        {
            int firstRow = rand.Next(0, 2);
            int firstCol = rand.Next(0, 8);
            int secondRow = rand.Next(0, 2);
            int secondCol = rand.Next(0, 8);
            string p1 = notRandom[firstRow, firstCol];
            string p2 = notRandom[secondRow, secondCol];
            if(p1[1] != 'K' && p2[1] != 'K')
            {
                notRandom[secondRow, secondCol] = p1;
                notRandom[firstRow, firstCol] = p2;
            }

            firstRow += 6;
            secondRow += 6;
            p1 = notRandom[firstRow, firstCol];
            p2 = notRandom[secondRow, secondCol];
            if (p1[1] != 'K' && p2[1] != 'K')
            {
                notRandom[secondRow, secondCol] = p1;
                notRandom[firstRow, firstCol] = p2;
            }
        }
        return notRandom;
    }
}
