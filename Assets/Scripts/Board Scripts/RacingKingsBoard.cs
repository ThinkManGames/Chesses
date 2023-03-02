using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingKingsBoard : Board
{
    public override string[,] getBoard()
    {
        return new string[8, 8] {
        { "BQ1", "BR1", "BB1", "BN1", "WN1", "WB1", "WR1", "WQ1"},
        { "BK1", "BR2", "BB2", "BN2", "WN2", "WB2", "WR2", "WK1"},
        {"E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" }};
    }
}
