using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeCapture : Capture
{
    protected int[,] QueenKingMoves = new int[8, 2]
    {
        {0, -1 },
        {0, 1 },
        {-1, 0 },
        {1, 0 },
        {1, -1 },
        {1, 1 },
        {-1, 1 },
        {-1, -1 }
    };
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
        if (temp[spot.row, spot.col] != "E") //means we are killin someone
        {
            for (int i = 0; i < 8; i++)
            {
                int rowAdder = QueenKingMoves[i, 0];
                int colAdder = QueenKingMoves[i, 1];
                int currRow = spot.row + rowAdder;
                int currCol = spot.col + colAdder;
                if (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol] != "0")
                {
                    temp[currRow, currCol] = "E";
                }
            }
            temp[spot.row, spot.col] = "E";
            temp[selectedRow, selectedCol] = "E";
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
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        if (board[spot.row, spot.col] != "E")
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
            destroyedPiece = GameObject.Find(board[selectedRow, selectedCol]);
            if (board[selectedRow, selectedCol][0] == 'W')
            {
                wAdder.lostAPiece(board[selectedRow, selectedCol][1]);
            }
            if (board[selectedRow, selectedCol][0] == 'B')
            {
                bAdder.lostAPiece(board[selectedRow, selectedCol][1]);
            }
            destroyedPiece.SetActive(false);
            board[spot.row, spot.col] = "E";
            board[selectedRow, selectedCol] = "E";
            for (int i = 0; i < 8; i++)
            {
                int rowAdder = QueenKingMoves[i, 0];
                int colAdder = QueenKingMoves[i, 1];
                int currRow = spot.row + rowAdder;
                int currCol = spot.col + colAdder;
                if (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol] != "0" && board[currRow,currCol] != "E")
                {
                    destroyedPiece = GameObject.Find(board[currRow, currCol]);
                    wAdder = GameObject.Find("DeadWhite").GetComponent<LostPieceAdder>();
                    bAdder = GameObject.Find("DeadBlack").GetComponent<LostPieceAdder>();
                    if (board[currRow, currCol][0] == 'W')
                    {
                        wAdder.lostAPiece(board[currRow, currCol][1]);
                    }
                    if (board[currRow, currCol][0] == 'B')
                    {
                        bAdder.lostAPiece(board[currRow, currCol][1]);
                    }
                    destroyedPiece.SetActive(false);
                    board[currRow, currCol] = "E";
                }
            }

        }
        else
        {
            Transform GOPiece = GameObject.Find(board[selectedRow, selectedCol]).transform;
            Vector3 tempPos = spot.transform.position;
            tempPos.z -= 0.1f;
            GOPiece.position = tempPos;
            board[spot.row, spot.col] = board[selectedRow, selectedCol];
            board[selectedRow, selectedCol] = "E";
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
        }
        switchColor = true;
        switchBoard = true;
    }
}
