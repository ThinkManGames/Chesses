using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyBoard : Board
{
    public override string[,] getBoard()
    {
        return new string[5, 5] {
        { "WR1", "WB1", "WK1", "WB2", "WR2" },
        { "WP1", "WN1", "WP2","WN2", "WP3"},
        {"E", "E", "E", "E", "E"},
        { "BP1", "BN1", "BP2", "BN2", "BP3"},
        { "BR1","BB1","BK1", "BB2","BR2"}};
    }
}
//{ "WR1", "WB1", "WK1", "WB2", "WR2" },
//        { "WP1", "WN1", "WP2","WN2", "WP3"},
//        { "E", "E", "E", "E", "E"},
//        { "BP1", "BN1", "BP2", "BN2", "BP3"},
//        { "BR1","BB1","BK1", "BB2","BR2"}};