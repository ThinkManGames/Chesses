using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class DodgeballCapture : Capture
{
    public bool placingGuy = false;
    public char guyToPlace = ' ';
    public bool guySelected = false;
    public char guyTaken = ' ';

    private Dictionary<string, int> countOfPieces = new Dictionary<string, int>();
    private bool doneCountingPieces = false;

    private void Awake()
    {
        placingGuy = false;
        guyToPlace = ' ';
        guySelected = false;
        guyTaken = ' ';
        doneCountingPieces = false;
        countOfPieces = new Dictionary<string, int>();
    }
    private void Start()
    {
        placingGuy = false;
        guyToPlace = ' ';
        guySelected = false;
        guyTaken = ' ';
        doneCountingPieces = false;
        countOfPieces = new Dictionary<string, int>();
    }

    private bool makeYourStuffNull = true;

    private void Update()
    {
        if(makeYourStuffNull == true)
        {
            placingGuy = false;
            guyToPlace = ' ';
            guySelected = false;
            guyTaken = ' ';
            doneCountingPieces = false;
            countOfPieces = new Dictionary<string, int>();
            makeYourStuffNull = false;
        }
    }
    public override string[,] movementCheck(SpotBehavior spot, string[,] board, int selectedRow, int selectedCol)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        string[,] temp = new string[tRow, tCol];
        for (int i = 0; i < tRow; i++)
        {
            for (int j = 0; j < tCol; j++)
            {
                temp[i,j] = board[i,j];
                if(!doneCountingPieces)
                {
                    if (board[i,j] == "E")
                    {
                        continue;
                    }
                    if (countOfPieces.ContainsKey(board[i, j].Substring(0, 2)))
                    {
                        countOfPieces[board[i, j].Substring(0, 2)]++;
                    }
                    else
                    {
                        countOfPieces.Add(board[i, j].Substring(0, 2), 1);
                    }
                }
            }
        }
        if (placingGuy)
        {
            if (guyToPlace != ' ')
            {
                Debug.Log(world.turn.ToString() + guyToPlace.ToString() + (countOfPieces[world.turn.ToString() + guyToPlace.ToString()] + 1).ToString());
                board[spot.row, spot.col] = world.turn.ToString() + guyToPlace.ToString() + (countOfPieces[world.turn.ToString() + guyToPlace.ToString()] + 1).ToString();
            }
            return temp;
        }
        temp[spot.row, spot.col] = temp[selectedRow, selectedCol];
        temp[selectedRow, selectedCol] = "E";
        return temp;
    }

    public void selectGuy(char guy)
    {
        guyToPlace = guy;
        if(guySelected)
        {
            return;
        }
        guySelected = true;
        int rowCount = world.board.GetLength(0);
        int colCount = world.board.Length / world.board.GetLength(0);
        world.possibleSpots = new int[rowCount, colCount];
        Debug.Log(rowCount + " " + colCount);
        if (world.turn == 'W')
        {
            for(int tRow = 0; tRow < 2; tRow++)
            {
                for (int tCol = 1; tCol < colCount; tCol++)
                {
                    if (world.board[tRow, tCol] == "E")
                    {
                        Debug.Log("found an empty space at: " + tRow + " " + tCol);
                        world.possibleSpots[tRow,tCol] = 1;
                    }
                }
            }
        }
        else
        {
            for (int tRow = rowCount-2; tRow < rowCount; tRow++)
            {
                for (int tCol = 1; tCol < colCount; tCol++)
                {
                    if (world.board[tRow, tCol] == "E")
                    {
                        world.possibleSpots[tRow, tCol] = 1;
                    }
                }
            }
        }

    }

    public override void movementLock(SpotBehavior spot, ref string[,] board, int selectedRow, int selectedCol)
    {
        if(placingGuy)
        {
            if (guyToPlace != ' ')
            {
                selectedRow = spot.row; 
                selectedCol = spot.col;
                board[selectedRow, selectedCol] = world.turn.ToString() + guyToPlace.ToString() + (countOfPieces[world.turn.ToString() + guyToPlace.ToString()] + 1).ToString();
                string spotName = char.ConvertFromUtf32(selectedCol + 65) + " (" + (selectedRow + 1).ToString() + ")";
                Vector3 newPiecePosition = GameObject.Find(spotName).transform.position;
                GameObject currPiece = null;
                DodgeballLostPieceAdder wAdder = GameObject.Find("DeadWhite").GetComponent<DodgeballLostPieceAdder>();
                DodgeballLostPieceAdder bAdder = GameObject.Find("DeadBlack").GetComponent<DodgeballLostPieceAdder>();
                if (board[selectedRow, selectedCol][0] == 'W')
                {
                    (wAdder).gotAPiece(guyToPlace);
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
                    currPiece.transform.position = newPiecePosition;
                    currPiece.GetComponent<SpriteRenderer>().sortingLayerName = "PieceMask";
                    currPiece.name = board[selectedRow, selectedCol];
                }
                else if (board[selectedRow, selectedCol][0] == 'B')
                {
                    (bAdder).gotAPiece(guyToPlace);
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
                    currPiece.transform.position = newPiecePosition;
                    currPiece.GetComponent<SpriteRenderer>().sortingLayerName = "PieceMask";
                    currPiece.name = board[selectedRow, selectedCol];
                }
                switchColor = true;
                switchBoard = true;
                guySelected = false;
                placingGuy = false;
                guyToPlace = ' ';
                return;
            }
        }
        if(board[selectedRow, selectedCol][1] == 'K' && Mathf.Abs(spot.col - selectedCol) >= 2) // we are castling
        {
            if (spot.col - selectedCol > 0) // we are going right
            {
                int rookCol = selectedCol + 1;
                while(board[selectedRow, rookCol] == "E")
                {
                    rookCol++;
                }
                Transform rook = GameObject.Find(board[selectedRow, rookCol]).transform; // this is the rook we are switching with
                string spotName = char.ConvertFromUtf32(spot.col -1 + 65) + " (" + (spot.row + 1).ToString() + ")";
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
        if(board[spot.row, spot.col] != "E" && board[spot.row, spot.col][0] != board[selectedRow,selectedCol][0])
        {
            GameObject destroyedPiece = GameObject.Find(board[spot.row, spot.col]);
            DodgeballLostPieceAdder wAdder = GameObject.Find("DeadWhite").GetComponent<DodgeballLostPieceAdder>();
            DodgeballLostPieceAdder bAdder = GameObject.Find("DeadBlack").GetComponent<DodgeballLostPieceAdder>();
            Debug.Log(selectedRow.ToString() + " " + selectedCol.ToString() + " " + board[selectedRow, selectedCol] + " " + wAdder.GetAllLostPieces() + " " + bAdder.GetAllLostPieces());

            // look through the list of my lost pieces and see if I have a piece that is worse than or equal to the one I just took. If so, allow me to put it on the board.
            string lostGuys = board[selectedRow, selectedCol][0] == 'W' ? wAdder.GetAllLostPieces() : bAdder.GetAllLostPieces();
            string worseThanJustTaken = "KQRBNP";
            int indexInWorseThanJustTaken = worseThanJustTaken.IndexOf(board[spot.row, spot.col][1]);
            if(worseThanJustTaken[indexInWorseThanJustTaken] == 'N')
            {
                // if we just took a knight, let us bring back bishops as well
                indexInWorseThanJustTaken--;
            }
            for(int i = indexInWorseThanJustTaken; i < worseThanJustTaken.Length; i++)
            {
                if(lostGuys.Contains(worseThanJustTaken[i].ToString()))
                {
                    int rowCount = world.board.GetLength(0);
                    int colCount = world.board.Length / world.board.GetLength(0);
                    for (int row = (board[selectedRow, selectedCol][0] == 'W' ? 0 : rowCount - 2); row < (board[selectedRow, selectedCol][0] == 'W' ? 2 : rowCount); row++)
                    {
                        for(int col = 0; col < colCount; col++)
                        {
                            if(board[row,col] == "E")
                            {
                                guyTaken = board[spot.row, spot.col][1];
                                placingGuy = true;
                                break;
                            }
                        }
                        if(placingGuy == true)
                        {
                            break;
                        }
                    }
                }
            }
            

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
        if (spot.row == tRow - 1 && board[spot.row,spot.col][0] == 'W' && board[spot.row,spot.col][1] == 'P') // we just moved a white pawn to the back row
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
        if(!placingGuy)
        {
            switchColor = true;
            switchBoard = true;
        }
    }
}
