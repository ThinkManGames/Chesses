using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCapture : Capture
{
    public override string[,] movementCheck(SpotBehavior spot, string[,] board, int selectedRow, int selectedCol)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        string[,] temp = new string[tRow, tCol];
        for (int i = 0; i < tRow; i++)
        {
            for (int j = 0; j < tCol; j++)
            {
                temp[i, j] = board[i, j];
            }
        }

        if (temp[spot.row, spot.col].Length > 1) // means we are pushing something
        {
            int dRow = 0;
            int dCol = 0;
            if(selectedRow > spot.row) // we are going down
            {
                dRow = -1;
            }
            else if(selectedRow < spot.row) // we are going up
            {
                dRow = 1;
            }

            if(selectedCol > spot.col) // we are going left
            {
                dCol = -1;
            }
            else if(selectedCol < spot.col) // we are going right
            {
                dCol = 1;
            }

            string pushingPiece = temp[selectedRow, selectedCol];
            int pushingRow = selectedRow;
            int pushingCol = selectedCol;
            string gettingPushed = temp[spot.row, spot.col];
            int gettingPushedRow = spot.row;
            int gettingPushedCol = spot.col;
            temp[pushingRow, pushingCol] = "E";
            while (pushingPiece != "E")
            {
                temp[gettingPushedRow, gettingPushedCol] = pushingPiece;
                gettingPushedRow += dRow;
                gettingPushedCol += dCol;
                pushingPiece = gettingPushed;
                if(gettingPushedCol < 0 || gettingPushedRow < 0 || gettingPushedCol == tCol || gettingPushedRow == tRow)
                {
                    break;
                }
                gettingPushed = temp[gettingPushedRow, gettingPushedCol];
            }
        }
        else
        {
            temp[spot.row, spot.col] = temp[selectedRow, selectedCol];
            temp[selectedRow, selectedCol] = "E";
        }
        return temp;
    }

    public override void movementLock(SpotBehavior spot, ref string[,] board, int selectedRow, int selectedCol)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        if (board[spot.row, spot.col].Length > 1)
        {
            int dRow = 0;
            int dCol = 0;
            if (selectedRow > spot.row) // we are going down
            {
                dRow = -1;
            }
            else if (selectedRow < spot.row) // we are going up
            {
                dRow = 1;
            }

            if (selectedCol > spot.col) // we are going left
            {
                dCol = -1;
            }
            else if (selectedCol < spot.col) // we are going right
            {
                dCol = 1;
            }

            string pushingPiece = board[selectedRow, selectedCol];
            int pushingRow = selectedRow;
            int pushingCol = selectedCol;
            string gettingPushed = board[spot.row, spot.col];
            int gettingPushedRow = spot.row;
            int gettingPushedCol = spot.col;
            board[pushingRow, pushingCol] = "E";
            while (pushingPiece.Length > 1)
            {
                board[gettingPushedRow, gettingPushedCol] = pushingPiece;
                Transform pushingTrasnform = GameObject.Find(pushingPiece).transform;
                string spotName = char.ConvertFromUtf32(gettingPushedCol + 65) + " (" + (gettingPushedRow + 1).ToString() + ")";
                pushingTrasnform.position = GameObject.Find(spotName).transform.position;
                gettingPushedRow += dRow;
                gettingPushedCol += dCol;
                pushingPiece = gettingPushed;
                if (gettingPushedCol < 0 || gettingPushedRow < 0 || gettingPushedCol == tCol || gettingPushedRow == tRow || board[gettingPushedRow,gettingPushedCol] == "0")
                {
                    wAdder = GameObject.Find("DeadWhite").GetComponent<LostPieceAdder>();
                    bAdder = GameObject.Find("DeadBlack").GetComponent<LostPieceAdder>();
                    if (pushingPiece[0] == 'W')
                    {
                        wAdder.lostAPiece(pushingPiece[1]);
                    }
                    if (pushingPiece[0] == 'B')
                    {
                        bAdder.lostAPiece(pushingPiece[1]);
                    }
                    Destroy(GameObject.Find(pushingPiece));
                    break;
                }
                gettingPushed = board[gettingPushedRow, gettingPushedCol];
            }
        }
        else
        {
            Transform GOPiece = GameObject.Find(board[selectedRow, selectedCol]).transform;
            Vector3 tempPos = spot.transform.position;
            GOPiece.position = tempPos;
            board[spot.row, spot.col] = board[selectedRow, selectedCol];
            board[selectedRow, selectedCol] = "E";
        }
        switchColor = true;
        switchBoard = true;
    }
}
