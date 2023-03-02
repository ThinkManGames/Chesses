using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DandDCapture : Capture
{
    public int takeChance = 0;
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
        if(temp[spot.row, spot.col].Length > 1 && temp[selectedRow, selectedCol][1] != 'K')
        {
            System.Random rand = new System.Random();
            int attackChance = 0;
            int defendChance = 0;
            switch (temp[selectedRow, selectedCol][1])
            {
                case 'P':
                    attackChance = rand.Next(0, 20);
                    break;
                case 'N':
                    attackChance = rand.Next(0, 60);
                    break;
                case 'B':
                    attackChance = rand.Next(0, 60);
                    break;
                case 'R':
                    attackChance = rand.Next(0, 100);
                    break;
                case 'Q':
                    attackChance = rand.Next(0, 180);
                    break;
            }
            switch (temp[spot.row, spot.col][1])
            {
                case 'P':
                    defendChance = rand.Next(0, 10);
                    break;
                case 'N':
                    defendChance = rand.Next(0, 30);
                    break;
                case 'B':
                    defendChance = rand.Next(0, 30);
                    break;
                case 'R':
                    defendChance = rand.Next(0, 50);
                    break;
                case 'Q':
                    defendChance = rand.Next(0, 90);
                    break;
            }
            if (attackChance >= defendChance)
            {
                temp[spot.row, spot.col] = temp[selectedRow, selectedCol];
                temp[selectedRow, selectedCol] = "E";
                takeChance = 1;
            }
            else
            {
                temp[selectedRow, selectedCol] = "E";
                takeChance = 2;
            }
        }
        else
        {
            takeChance = 0;
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
        if (board[spot.row, spot.col] != "E" && board[spot.row, spot.col][0] != board[selectedRow, selectedCol][0])
        {
            if(takeChance == 2 && board[selectedRow, selectedCol][1] != 'K')
            {
                GameObject destroyedPiece = GameObject.Find(board[selectedRow, selectedCol]);
                wAdder = GameObject.Find("DeadWhite").GetComponent<LostPieceAdder>();
                bAdder = GameObject.Find("DeadBlack").GetComponent<LostPieceAdder>();
                if (board[selectedRow, selectedCol][0] == 'W')
                {
                    wAdder.lostAPiece(board[selectedRow, selectedCol][1]);
                }
                if (board[selectedRow, selectedCol][0] == 'B')
                {
                    bAdder.lostAPiece(board[selectedRow, selectedCol][1]);
                }
                Destroy(destroyedPiece);
                board[selectedRow, selectedCol] = "E";
            } else
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
                Destroy(destroyedPiece);

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
            }
        }
        else
        {
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
        }
        takeChance = 0;
        switchColor = true;
        switchBoard = true;
    }
}
