using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaffeinatedCapture : Capture
{
    private int turnCounter = 0;
    private int currentTurnLength = 0;
    public override void movementLock(SpotBehavior spot, ref string[,] board, int selectedRow, int selectedCol)
    {
        if(world.turnNumber == 1)
        {
            turnCounter = 0;
            currentTurnLength = 0;
        }
        if (board[selectedRow, selectedCol][1] == 'K' && Mathf.Abs(spot.col - selectedCol) >= 2) // we are castling
        {
            if (spot.col - selectedCol > 0) // we are going right
            {
                int rookCol = selectedCol + 1;
                while (board[selectedRow, rookCol] == "E")
                {
                    rookCol++;
                }
                Transform rook = GameObject.Find(board[selectedRow, rookCol]).transform; // this is the rook we are switching with
                string spotName = char.ConvertFromUtf32(spot.col - 1 + 65) + " (" + (spot.row + 1).ToString() + ")";
                Vector3 newRookPos = GameObject.Find(spotName).transform.position;
                rook.position = newRookPos;
                board[spot.row, spot.col - 1] = board[selectedRow, rookCol];
                board[selectedRow, rookCol] = "E";
            }
            if (spot.col - selectedCol < 0) // we are going left
            {
                int rookCol = selectedCol - 1;
                while (board[selectedRow, rookCol] == "E")
                {
                    rookCol--;
                }
                Transform rook = GameObject.Find(board[selectedRow, rookCol]).transform; // this is the rook we are switching with
                string spotName = char.ConvertFromUtf32(spot.col + 1 + 65) + " (" + (spot.row + 1).ToString() + ")";
                Vector3 newRookPos = GameObject.Find(spotName).transform.position;
                rook.position = newRookPos;
                board[spot.row, spot.col + 1] = board[selectedRow, rookCol];
                board[selectedRow, rookCol] = "E";
            }
        }
        if (board[spot.row, spot.col] != "E" && board[spot.row, spot.col][0] != board[selectedRow, selectedCol][0])
        {
            GameObject destroyedPiece = GameObject.Find(board[spot.row, spot.col]);
            wAdder = GameObject.Find("DeadWhite").GetComponent<LostPieceAdder>();
            bAdder = GameObject.Find("DeadBlack").GetComponent<LostPieceAdder>();
            if (board[spot.row, spot.col][0] == 'W')
            {
                wAdder.lostAPiece(board[spot.row, spot.col][1]);
            }
            if (board[spot.row, spot.col][0] == 'B')
            {
                bAdder.lostAPiece(board[spot.row, spot.col][1]);
            }
            destroyedPiece.SetActive(false);
        }
        Transform GOPiece = GameObject.Find(board[selectedRow, selectedCol]).transform;
        Vector3 tempPos = spot.transform.position;
        tempPos.z -= 0.1f;
        GOPiece.position = tempPos;
        board[spot.row, spot.col] = board[selectedRow, selectedCol];
        board[selectedRow, selectedCol] = "E";
        int tRow = board.GetLength(0);
        if (spot.row == tRow - 1 && board[spot.row, spot.col][0] == 'W' && board[spot.row, spot.col][1] == 'P') // we just moved a white pawn to the back row
        {
            foreach (PopupButtonBehavior buttonBehavior in FindObjectsOfType<PopupButtonBehavior>())
            {
                buttonBehavior.setColor('W');
                buttonBehavior.whereToReplace(spot.row, spot.col);
            }
            GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;
            GameObject toDelete = GameObject.Find(board[spot.row, spot.col]);
            Destroy(toDelete);
        }
        else if (spot.row == 0 && board[spot.row, spot.col][0] == 'B' && board[spot.row, spot.col][1] == 'P') // we just moved a white pawn to the back row
        {
            foreach (PopupButtonBehavior buttonBehavior in FindObjectsOfType<PopupButtonBehavior>())
            {
                buttonBehavior.setColor('B');
                buttonBehavior.whereToReplace(spot.row, spot.col);
            }
            GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;
            GameObject toDelete = GameObject.Find(board[spot.row, spot.col]);
            Destroy(toDelete);
        }
        if(turnCounter == 14)
        {
            turnCounter++;
            int nRow = board.GetLength(0);
            int nCol = board.Length / nRow;
            for(int r = 0; r < nRow; r++)
            {
                for(int c = 0; c < nCol; c++)
                {
                    if(board[r,c] == "WQ1")
                    {
                        board[r, c] = "E";
                        GameObject wQueen = GameObject.Find("WQ1");
                        Destroy(wQueen);
                    }
                }
            }
            switchColor = true;
            switchBoard = true;
        }
        else if (turnCounter >= 15)
        {
            switchColor = true;
            switchBoard = true;
        }
        else
        {
            currentTurnLength++;
            if (world.turn == 'W')
            {
                if(currentTurnLength == 2)
                {
                    currentTurnLength = 0;
                    turnCounter += 2;
                    switchColor = true;
                    switchBoard = true;
                }
                else
                {
                    if (!world.win.isGoodMove('B', board)) // if our move put the other guy in check
                    {
                        turnCounter += 2;
                        currentTurnLength = 0;
                        switchColor = true;
                        switchBoard = true;
                    }
                }
            }
            else
            {
                turnCounter++;
                currentTurnLength = 0;
                switchColor = true;
                switchBoard = true;
            }
        }
        
    }
}