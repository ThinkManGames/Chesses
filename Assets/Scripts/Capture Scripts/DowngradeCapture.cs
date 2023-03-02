using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DowngradeCapture : Capture
{
    public override string[,] movementCheck(SpotBehavior spot, string[,] board, int selectedRow, int selectedCol)
    {
        Dictionary<string, int> pieceNum = new Dictionary<string, int>();
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        string[,] temp = new string[tRow, tCol];
        for (int i = 0; i < tRow; i++)
        {
            for (int j = 0; j < tCol; j++)
            {
                temp[i, j] = board[i, j];
                if (board[i, j].Length > 1)
                {
                    string pieceName = board[i, j][0].ToString() + board[i, j][1].ToString();
                    if (!pieceNum.ContainsKey(pieceName))
                    {
                        if (board[i, j].Length == 4)
                        {
                            pieceNum.Add(pieceName, (board[i, j][2] - '0') * 10 + (board[i, j][3] - '0'));
                        }
                        else
                        {
                            pieceNum.Add(pieceName, board[i, j][2] - '0');
                        }
                    }
                    else
                    {
                        if (board[i, j].Length == 4)
                        {
                            if ((board[i, j][2] - '0') * 10 + (board[i, j][3] - '0') > pieceNum[pieceName])
                            {
                                pieceNum[pieceName] = (board[i, j][2] - '0') * 10 + (board[i, j][3] - '0');
                            }
                        }
                        else if (board[i, j][2] - '0' > pieceNum[pieceName])
                        {
                            pieceNum[pieceName] = board[i, j][2] - '0';
                        }
                    }
                }
            }
        }
        if (board[spot.row, spot.col] != "E" && board[spot.row, spot.col][0] != board[selectedRow, selectedCol][0]) // meaning we are taking a piece
        {
            temp[selectedRow, selectedCol] = getNewName(temp[selectedRow, selectedCol], pieceNum);
        }
        temp[spot.row, spot.col] = temp[selectedRow, selectedCol];
        temp[selectedRow, selectedCol] = "E";
        return temp;
    }

    private string getNewName(string currName, Dictionary<string, int> pieceNum)
    {
        string newName = "";
        if (currName[1] == 'Q')
        {
            if (!pieceNum.ContainsKey(currName[0].ToString() + "R"))
            {
                pieceNum.Add(currName[0].ToString() + "R", 1);
            }
            newName = currName[0].ToString() + "R" + (pieceNum[currName[0].ToString() + "R"] + 1).ToString();
        }
        else if (currName[1] == 'R')
        {
            if (!pieceNum.ContainsKey(currName[0].ToString() + "N"))
            {
                pieceNum.Add(currName[0].ToString() + "N", 1);
            }
            newName = currName[0].ToString() + "N" + (pieceNum[currName[0].ToString() + "N"] + 1).ToString();
        }
        else if (currName[1] == 'N')
        {
            if (!pieceNum.ContainsKey(currName[0].ToString() + "B"))
            {
                pieceNum.Add(currName[0].ToString() + "B", 1);
            }
            newName = currName[0].ToString() + "B" + (pieceNum[currName[0].ToString() + "B"] + 1).ToString();
        }
        else if (currName[1] == 'B')
        {
            if (!pieceNum.ContainsKey(currName[0].ToString() + "P"))
            {
                pieceNum.Add(currName[0].ToString() + "P", 1);
            }
            newName = currName[0].ToString() + "P" + (pieceNum[currName[0].ToString() + "P"] + 1).ToString();
        }
        else if (currName[1] == 'P' || currName[1] == 'K')
        {
            newName = currName;
        }
        return newName;
    }

    public override void movementLock(SpotBehavior spot, ref string[,] board, int selectedRow, int selectedCol)
    {

        if (board[spot.row, spot.col] != "E" && board[spot.row, spot.col][0] != board[selectedRow, selectedCol][0])
        {
            Dictionary<string, int> pieceNum = new Dictionary<string, int>();
            int tRow = board.GetLength(0);
            int tCol = board.Length / board.GetLength(0);
            string[,] temp = new string[tRow, tCol];
            for (int i = 0; i < tRow; i++)
            {
                for (int j = 0; j < tCol; j++)
                {
                    temp[i, j] = board[i, j];
                    if (board[i, j].Length > 1)
                    {
                        string pieceName = board[i, j][0].ToString() + board[i, j][1].ToString();
                        if (!pieceNum.ContainsKey(pieceName))
                        {
                            if (board[i, j].Length == 4)
                            {
                                pieceNum.Add(pieceName, (board[i, j][2] - '0') * 10 + (board[i, j][3] - '0'));
                            }
                            else
                            {
                                pieceNum.Add(pieceName, board[i, j][2] - '0');
                            }
                        }
                        else
                        {
                            if (board[i, j].Length == 4)
                            {
                                if ((board[i, j][2] - '0') * 10 + (board[i, j][3] - '0') > pieceNum[pieceName])
                                {
                                    pieceNum[pieceName] = (board[i, j][2] - '0') * 10 + (board[i, j][3] - '0');
                                }
                            }
                            else if (board[i, j][2] - '0' > pieceNum[pieceName])
                            {
                                pieceNum[pieceName] = board[i, j][2] - '0';
                            }
                        }
                    }
                }
            }
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

            string spotName = char.ConvertFromUtf32(spot.col + 65) + " (" + (spot.row + 1).ToString() + ")";
            Vector3 myTemp = GameObject.Find(spotName).transform.position;
            myTemp.z -= 0.1f;
            GameObject oldPiece = GameObject.Find(board[selectedRow, selectedCol]);
            oldPiece.SetActive(false);
            board[selectedRow, selectedCol] = getNewName(board[selectedRow, selectedCol], pieceNum);
            GameObject currPiece = null;
            if (board[selectedRow, selectedCol][0] == 'W')
            {
                switch (board[selectedRow, selectedCol][1])
                {
                    case 'R':
                        currPiece = Instantiate(world.wRook, Vector3.zero, Quaternion.identity);
                        break;
                    case 'B':
                        currPiece = Instantiate(world.wBishop, Vector3.zero, Quaternion.identity);
                        break;
                    case 'N':
                        currPiece = Instantiate(world.wKnight, Vector3.zero, Quaternion.identity);
                        break;
                    case 'K':
                        currPiece = Instantiate(world.wKing, Vector3.zero, Quaternion.identity);
                        break;
                    case 'Q':
                        currPiece = Instantiate(world.wQueen, Vector3.zero, Quaternion.identity);
                        break;
                    case 'P':
                        currPiece = Instantiate(world.wPawn, Vector3.zero, Quaternion.identity);
                        break;
                }
                currPiece.AddComponent<PieceBehavior>();
                currPiece.transform.position = myTemp;
                currPiece.name = board[selectedRow, selectedCol];
            }
            else if (board[selectedRow, selectedCol][0] == 'B')
            {
                switch (board[selectedRow, selectedCol][1])
                {
                    case 'R':
                        currPiece = Instantiate(world.bRook, Vector3.zero, Quaternion.identity);
                        break;
                    case 'B':
                        currPiece = Instantiate(world.bBishop, Vector3.zero, Quaternion.identity);
                        break;
                    case 'N':
                        currPiece = Instantiate(world.bKnight, Vector3.zero, Quaternion.identity);
                        break;
                    case 'K':
                        currPiece = Instantiate(world.bKing, Vector3.zero, Quaternion.identity);
                        break;
                    case 'Q':
                        currPiece = Instantiate(world.bQueen, Vector3.zero, Quaternion.identity);
                        break;
                    case 'P':
                        currPiece = Instantiate(world.bPawn, Vector3.zero, Quaternion.identity);
                        break;
                }
                currPiece.AddComponent<PieceBehavior>();
                currPiece.transform.position = myTemp;
                currPiece.name = board[selectedRow, selectedCol];
            }
        }
        else
        {
            Transform GOPiece = GameObject.Find(board[selectedRow, selectedCol]).transform;
            Vector3 tempPos = spot.transform.position;
            tempPos.z -= 0.1f;
            GOPiece.position = tempPos;
        }
        board[spot.row, spot.col] = board[selectedRow, selectedCol];
        board[selectedRow, selectedCol] = "E";
        switchColor = true;
        switchBoard = true;
    }
}
