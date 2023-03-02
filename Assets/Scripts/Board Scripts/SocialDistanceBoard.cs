using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialDistanceBoard : Board
{
    public override string[,] getBoard()
    {
        return new string[16, 16] {
        { "WR1","E", "WN1","E", "WB1","E", "WQ1","E", "WK1","E", "WB2","E", "WN2","E", "WR2", "E" },
        { "E","WP1", "E","WP2", "E","WP3", "E","WP4","E", "WP5", "E","WP6","E", "WP7", "E","WP8"},
        {"E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E"},
        { "BP1","E", "BP2","E", "BP3", "E","BP4", "E","BP5", "E","BP6", "E","BP7","E", "BP8","E"},
        { "E","BR1", "E","BN1", "E","BB1", "E","BQ1", "E","BK1", "E","BB2","E", "BN2","E", "BR2"}};
    }
}
