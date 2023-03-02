using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBoard : Board
{
    public override string[,] getBoard()
    {
        return new string[8, 8] {
        { "WR1", "WN1", "WB1", "WQ1", "WK1", "WB2", "WN2", "WR2" },
        { "WQ2", "WQ3", "WQ4", "WQ5", "WQ6", "WQ7", "WQ8", "WQ9"},
        {"E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        { "BQ2", "BQ3", "BQ4", "BQ5", "BQ6", "BQ7", "BQ8", "BQ9"},
        { "BR1", "BN1", "BB1", "BQ1", "BK1", "BB2", "BN2", "BR2"}};
    }
}
