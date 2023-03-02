using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyPregame : Pregame
{
    private bool amIDone = false;
    private char turn = 'W';
    private int[,] possibleSpots;
    public string whiteSpy = "";
    public string blackSpy = "";

    int nRow = -1;
    int nCol = -1;

    public override void loadPregame()
    {
        whiteSpy = "";
        blackSpy = "";
        turn = 'W';
        amIDone = false;
        nRow = -1;
        nCol = -1;
        string[,] tempBoard = world.boardLoader.getBoard();
        nRow = tempBoard.GetLength(0);
        nCol = tempBoard.Length / nRow;
        possibleSpots = world.moves.possibleMoves(turn, "start", nRow,nCol, tempBoard);
        world.possibleSpots = possibleSpots;
    }
    public override bool doneWithPreGame()
    {
        return amIDone;
    }

    public override void spotClicked(SpotBehavior spot)
    {
        if(possibleSpots[spot.row,spot.col] == 1) // meaning we already clicked something
        {
            if(turn == 'W')
            {
                whiteSpy = world.board[spot.row, spot.col];
                world.boardSide = 'B';
                turn = 'B';
                possibleSpots = world.moves.possibleMoves(turn, "start", nRow, nCol, world.board);
                world.possibleSpots = possibleSpots;
            } else
            {
                blackSpy = world.board[spot.row, spot.col];
                world.boardSide = 'W';
                world.possibleSpots = new int[nRow, nCol];
                amIDone = true;
            }
        }
    }

    public override string getWin(char color)
    {
        return color == 'W' ? whiteSpy : blackSpy;
    }
}
