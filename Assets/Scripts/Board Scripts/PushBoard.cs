using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoard : Board
{
    public override string[,] getBoard()
    {
        return new string[8, 8] {
        {"E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        { "WR1", "WB1", "WB2", "WQ1", "WK1", "WB3", "WB4", "WR2" },
        { "WP1", "WP2", "WP3", "WP4", "WP5", "WP6", "WP7", "WP8"},
        { "BP1", "BP2", "BP3", "BP4", "BP5", "BP6", "BP7", "BP8"},
        { "BR1", "BB1", "BB2", "BQ1", "BK1", "BB3", "BB4", "BR2"},
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" }};
    }
}
