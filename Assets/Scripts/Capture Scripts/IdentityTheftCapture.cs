using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentityTheftCapture : Capture
{
    public override void movementLock(SpotBehavior spot, ref string[,] board, int selectedRow, int selectedCol)
    {
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
            if (board[selectedRow, selectedCol][1] != 'K' && board[selectedRow, selectedCol][1] != board[spot.row, spot.col][1]) {
                string victimName = destroyedPiece.name;
                TheWorld world = GameObject.Find("TheWorld").GetComponent<TheWorld>();
                int tRow1 = world.board.GetLength(0);
                int tCol1 = world.board.Length / world.board.GetLength(0);
                int myNewNum = 0;
                for (int r = 0; r < tRow1; r++)
                {
                    for (int c = 0; c < tCol1; c++)
                    {
                        if (board[r, c][0] == board[selectedRow, selectedCol][0] && board[r, c][1] == victimName[1])
                        {
                            if(board[r,c].Length == 4)
                            {
                                if((board[r,c][2] - '0') * 10 + (board[r,c][3] - '0') > myNewNum)
                                {
                                    myNewNum = (board[r, c][2] - '0') * 10 + (board[r, c][3] - '0');
                                }
                            }
                            else if(board[r,c][2] - '0' > myNewNum)
                            {
                                myNewNum = board[r, c][2] - '0';
                            }
                        }
                    }
                }
                myNewNum++;
                string newName = board[selectedRow, selectedCol][0].ToString() + victimName[1].ToString() + myNewNum.ToString();
                GameObject toRetexture = GameObject.Find(board[selectedRow, selectedCol]);
                toRetexture.name = newName;
                board[selectedRow, selectedCol] = newName;
                switch (victimName[1])
                {
                    case 'P':
                        if(board[selectedRow,selectedCol][0] == 'W')
                        {
                            toRetexture.GetComponent<SpriteRenderer>().sprite = world.wPawn.GetComponent<SpriteRenderer>().sprite;
                        }
                        else
                        {
                            toRetexture.GetComponent<SpriteRenderer>().sprite = world.bPawn.GetComponent<SpriteRenderer>().sprite;
                        }
                        break;
                    case 'N':
                        if (board[selectedRow, selectedCol][0] == 'W')
                        {
                            toRetexture.GetComponent<SpriteRenderer>().sprite = world.wKnight.GetComponent<SpriteRenderer>().sprite;
                        }
                        else
                        {
                            toRetexture.GetComponent<SpriteRenderer>().sprite = world.bKnight.GetComponent<SpriteRenderer>().sprite;
                        }
                        break;
                    case 'B':
                        if (board[selectedRow, selectedCol][0] == 'W')
                        {
                            toRetexture.GetComponent<SpriteRenderer>().sprite = world.wBishop.GetComponent<SpriteRenderer>().sprite;
                        }
                        else
                        {
                            toRetexture.GetComponent<SpriteRenderer>().sprite = world.bBishop.GetComponent<SpriteRenderer>().sprite;
                        }
                        break;
                    case 'R':
                        if (board[selectedRow, selectedCol][0] == 'W')
                        {
                            toRetexture.GetComponent<SpriteRenderer>().sprite = world.wRook.GetComponent<SpriteRenderer>().sprite;
                        }
                        else
                        {
                            toRetexture.GetComponent<SpriteRenderer>().sprite = world.bRook.GetComponent<SpriteRenderer>().sprite;
                        }
                        break;
                    case 'Q':
                        if (board[selectedRow, selectedCol][0] == 'W')
                        {
                            toRetexture.GetComponent<SpriteRenderer>().sprite = world.wQueen.GetComponent<SpriteRenderer>().sprite;
                        }
                        else
                        {
                            toRetexture.GetComponent<SpriteRenderer>().sprite = world.bQueen.GetComponent<SpriteRenderer>().sprite;
                        }
                        break;
                }
            }
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
            Destroy(destroyedPiece);
        }
        Transform GOPiece = GameObject.Find(board[selectedRow, selectedCol]).transform;
        Vector3 tempPos = spot.transform.position;
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
        switchColor = true;
        switchBoard = true;
    }
}
