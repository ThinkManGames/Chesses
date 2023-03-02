using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DowngradeBoard : Board
{
    public override string[,] getBoard()
    {
        return new string[8, 8] {
        { "WQ1", "WQ2", "WQ3", "WQ4", "WK1","WQ5", "WQ6", "WQ7"},
        { "WQ8", "WQ9", "WQ10", "WQ11", "WQ12","WQ13", "WQ14", "WQ15"},
        {"E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        { "BQ8", "BQ9", "BQ10", "BQ11", "BQ12", "BQ13", "BQ14", "BQ15"},
        { "BQ1", "BQ2", "BQ3", "BQ4", "BK1", "BQ5", "BQ6", "BQ7"}};
    }
}
