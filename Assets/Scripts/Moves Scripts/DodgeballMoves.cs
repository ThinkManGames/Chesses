using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeballMoves : Moves
{
    public override int[,] possibleMoves(char color, string pieceName, int row, int col, string[,] board)
    {
        if(((DodgeballCapture)world.capture).placingGuy)
        {
            return world.possibleSpots;
        }
        else
        {
            return base.possibleMoves(color, pieceName, row, col, board);
        }
    }
}
