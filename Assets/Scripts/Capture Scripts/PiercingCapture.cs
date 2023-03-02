using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingCapture : Capture
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
        if (board[selectedRow, selectedCol][1] != 'N' && board[selectedRow, selectedCol][1] != 'K' && board[selectedRow, selectedCol][1] != 'P') // as long as there might be pieces needing to be pierced
        {
            int rowChanger = spot.row - selectedRow; // other guys row - our row gives us the direction we need to go
            if (rowChanger < 0)
            {
                rowChanger = -1; // will give us how many cols we need to change every time we move along the board
            }
            else if (rowChanger > 0)
            {
                rowChanger = 1;
            }

            int colChanger = spot.col - selectedCol; // other guys col - our col gives us the direction we need to go
            if (colChanger < 0)
            {
                colChanger = -1; // will give us how many cols we need to change every time we move along the board
            } else if(colChanger > 0)
            {
                colChanger = 1;
            }
            int startingRow = selectedRow;
            int startingCol = selectedCol;
            startingRow += rowChanger;
            startingCol += colChanger;
            while (startingRow != spot.row || startingCol != spot.col)
            {
                if (temp[startingRow, startingCol] != "E") // meaning we went through something
                {
                    temp[startingRow, startingCol] = "E"; // kill the piece
                }
                startingRow += rowChanger;
                startingCol += colChanger;
            }
            
        }
        temp[spot.row, spot.col] = temp[selectedRow, selectedCol];
        temp[selectedRow, selectedCol] = "E";
        return temp;
    }

    public override void movementLock(SpotBehavior spot, ref string[,] board, int selectedRow, int selectedCol)
    {
        if (board[selectedRow, selectedCol][1] != 'N' && board[selectedRow, selectedCol][1] != 'K' && board[selectedRow, selectedCol][1] != 'P') // as long as there might be pieces needing to be pierced
        {
            int rowChanger = spot.row - selectedRow; // other guys row - our row gives us the direction we need to go
            if (rowChanger < 0)
            {
                rowChanger = -1; // will give us how many cols we need to change every time we move along the board
            }
            else if (rowChanger > 0)
            {
                rowChanger = 1;
            }

            int colChanger = spot.col - selectedCol; // other guys col - our col gives us the direction we need to go
            if (colChanger < 0)
            {
                colChanger = -1; // will give us how many cols we need to change every time we move along the board
            }
            else if (colChanger > 0)
            {
                colChanger = 1;
            }
            int startingRow = selectedRow;
            int startingCol = selectedCol;
            startingRow += rowChanger;
            startingCol += colChanger;
            while (startingRow != spot.row || startingCol != spot.col)
            {
                if (board[startingRow, startingCol] != "E") // meaning we went through something
                {
                    GameObject stabbedPiece = GameObject.Find(board[startingRow, startingCol]); // remove the piece
                    stabbedPiece.SetActive(false);
                    board[startingRow, startingCol] = "E"; // kill the piece
                }
                startingRow += rowChanger;
                startingCol += colChanger;
            }

        }
        if (board[spot.row, spot.col] != "E" && board[spot.row, spot.col][0] != board[selectedRow, selectedCol][0])
        {
            GameObject destroyedPiece = GameObject.Find(board[spot.row, spot.col]);
            destroyedPiece.SetActive(false);
        }
        Transform GOPiece = GameObject.Find(board[selectedRow, selectedCol]).transform;
        Vector3 tempPos = spot.transform.position;
        tempPos.z -= 0.1f;
        GOPiece.position = tempPos;
        board[spot.row, spot.col] = board[selectedRow, selectedCol];
        board[selectedRow, selectedCol] = "E";
        switchColor = true;
        switchBoard = true;
    }
}
