using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TntRunBoard : Board
{
    public override string[,] getBoard()
    {
        extraBoard = new int[8, 8];
        useExtraBoard = true;
        extraBoard[0, 3] = 1;
        extraBoard[7, 3] = 1;
        return new string[8, 8] {
        {"E", "E", "E", "WN1", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E"},
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "E", "E", "E", "E", "E" },
        {"E", "E", "E", "BN1", "E", "E", "E", "E" },};
    }
}
