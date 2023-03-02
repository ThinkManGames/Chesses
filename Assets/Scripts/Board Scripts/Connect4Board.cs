using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect4Board : Board
{ 
    public override string[,] getBoard()
    {
        return new string[8, 8] {
        { "WR1", "WN1", "WB1", "WQ1", "WK1", "WB2", "WN2", "WR2" },
        { "WP1", "WP2", "WP3", "E", "E", "WP6", "WP7", "WP8"},
        {"E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        { "BP1", "BP2", "BP3", "E", "E", "BP6", "BP7", "BP8"},
        { "BR1", "BN1", "BB1", "BQ1", "BK1", "BB2", "BN2", "BR2"}};
    }
}
