using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnRebellionBoard : Board
{
    public override string[,] getBoard()
    {
        return new string[8, 8] {
        { "WR1", "WN1", "WB1", "WQ1", "WK1", "WB2", "WN2", "WR2" },
        { "WP1", "WP2", "WP3", "WP4", "WP5", "WP6", "WP7", "WP8"},
        {"E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        { "BP25", "BP26", "BP27", "BP28", "BP29", "BP30", "BP31", "BP32"},
        { "BP17", "BP18", "BP19", "BP20", "BP21", "BP22", "BP23", "BP24"},
        { "BP9", "BP10", "BP11", "BP12", "BP13", "BP14", "BP15", "BP16"},
        { "BP1", "BP2", "BP3", "BP4", "BP5", "BP6", "BP7", "BP8"}};
    }
}
