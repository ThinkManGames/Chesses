using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldWarBoard : Board
{
    public override string[,] getBoard()
    {   
         return new string[8,26] {
        { "WR1","WR2","WR3","WR4", "WN1", "WN2", "WN3", "WN4", "WB1", "WB2", "WB3", "WB4", "WQ1", "WK1", "WB5", "WB6", "WB7", "WB8", "WN5", "WN6", "WN7", "WN8", "WR5", "WR6", "WR7", "WR8"},
        { "WP1", "WP2", "WP3", "WP4", "WP5", "WP6", "WP7", "WP8", "WP9", "WP10", "WP11", "WP12", "WP13", "WP14", "WP15", "WP16", "WP17", "WP18", "WP19", "WP20", "WP21", "WP22", "WP23", "WP24", "WP25", "WP26"},
        {"E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E","E", "E", "E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E","E", "E", "E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E","E", "E", "E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E","E", "E", "E", "E", "E", "E", "E", "E", "E", "E"},
        { "BP1", "BP2", "BP3", "BP4", "BP5", "BP6", "BP7", "BP8", "BP9", "BP10", "BP11", "BP12", "BP13", "BP14", "BP15", "BP16", "BP17", "BP18", "BP19", "BP20", "BP21", "BP22", "BP23", "BP24", "BP25", "BP26"},
        { "BR1","BR2","BR3","BR4", "BN1", "BN2", "BN3", "BN4", "BB1", "BB2", "BB3", "BB4", "BQ1", "BK1", "BB5", "BB6", "BB7", "BB8", "BN5", "BN6", "BN7", "BN8", "BR5", "BR6", "BR7", "BR8"}};
    }
}
