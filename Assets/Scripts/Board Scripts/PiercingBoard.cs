using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingBoard : Board
{
    public override string[,] getBoard()
    {
        return new string[8, 8] {
        { "WB1", "WR1", "WN1", "WN2", "WK1", "WN3", "WR2", "WB2"},
        { "WP1", "WP2", "WP3", "WP4", "WP5", "WP6", "WP7", "WP8"},
        {"E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        { "BP1", "BP2", "BP3", "BP4", "BP5", "BP6", "BP7", "BP8"},
        { "BB1", "BR1", "BN1", "BN2", "BK1", "BN3", "BR2", "BB2"}};
    }
}
