using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBoard : Board
{
    public override string[,] getBoard()
    {
        return new string[8, 8] {
        { "WR1", "WN1", "WB1", "WQ1", "WK1", "WB2", "WN2", "WR2" },
        { "WP1", "WP2", "WP3", "WP4", "WP5", "WP6", "WP7", "WP8"},
        {"E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "BK1", "BK2", "BK3", "BK4", "E", "E" }};
    }
}
