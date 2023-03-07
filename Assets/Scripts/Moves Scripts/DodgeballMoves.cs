using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeballMoves : Moves
{
    public override int[,] possibleMoves(char color, string pieceName, int row, int col, string[,] board)
    {
        DodgeballCapture capture = (DodgeballCapture)world.capture;
        if(((DodgeballCapture)world.capture).placingGuy == true)
        {
            Debug.Log("Pick a real spot loser");
            return world.possibleSpots;
        }
        else
        {
            return base.possibleMoves(color, pieceName, row, col, board);
        }
    }
}
