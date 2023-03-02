using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusterCluckBoard : Board
{
    public override string[,] getBoard()
    {
        string[,] toReturn = new string[6, 6] {
        { "WB1", "WR1", "WN1", "WN2", "WK1", "WQ1"},
        { "WP1", "WP2", "WP3", "WP4", "WP5", "WP6"},
        { "WR2", "WB2", "WP7", "WP8", "E", "E"},
        { "BR2", "BB2", "BP7", "BP8", "E", "E"},
        { "BP1", "BP2", "BP3", "BP4", "BP5", "BP6"},
        { "BB1", "BR1", "BN1", "BN2", "BK1", "BQ1"}};

        System.Random rand = new System.Random();
        for (int i = 0; i < 250; i++)
        {
            int r1 = rand.Next(0, 6); // spot 1 row
            int c1 = rand.Next(0, 6); // spot 1 col

            int r2 = rand.Next(0, 6); // spot 2 row
            int c2 = rand.Next(0, 6); // spot 2 col

            if(r1 == r2 && c1 == c2)
            {
                continue;
            }
            string hold = toReturn[r2, c2];
            toReturn[r2, c2] = toReturn[r1, c1];
            toReturn[r1, c1] = hold;
        }
        return toReturn;
    }
}
