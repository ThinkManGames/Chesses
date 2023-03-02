using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBoard : Board
{
    public override string[,] getBoard()
    {
        return new string[8, 8] {
        { "WP1", "WP2", "WP3", "WP4", "WK1","WP5", "WP6", "WP7"},
        { "WP8", "WP9", "WP10", "WP11", "WP12","WP13", "WP14", "WP15"},
        {"E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        { "BP8", "BP9", "BP10", "BP11", "BP12", "BP13", "BP14", "BP15"},
        { "BP1", "BP2", "BP3", "BP4", "BK1", "BP5", "BP6", "BP7"}};
    }
}
