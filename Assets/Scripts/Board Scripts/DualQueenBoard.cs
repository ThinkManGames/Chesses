using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualQueenBoard : Board
{
    public override string[,] getBoard()
    {
        return new string[8, 8] {
        { "WR1", "WN1", "WB1", "WQ1", "WQ2", "WB2", "WN2", "WR2" },
        { "WP1", "WP2", "WP3", "WP4", "WP5", "WP6", "WP7", "WP8"},
        {"E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        { "BP1", "BP2", "BP3", "BP4", "BP5", "BP6", "BP7", "BP8"},
        { "BR1", "BN1", "BB1", "BQ1", "BQ2", "BB2", "BN2", "BR2"}};
    }
}
