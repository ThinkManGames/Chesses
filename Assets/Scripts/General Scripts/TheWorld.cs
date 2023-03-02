using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheWorld : MonoBehaviour
{
    public int turnNumber = 1;
    public char turn = 'W';
    public char boardSide = 'W';
    public bool rotatingBoard = true;

    public GameObject wPawn = null;
    public GameObject wRook = null;
    public GameObject wKnight = null;
    public GameObject wBishop = null;
    public GameObject wQueen = null;
    public GameObject wKing = null;

    public GameObject bPawn = null;
    public GameObject bRook = null;
    public GameObject bKnight = null;
    public GameObject bBishop = null;
    public GameObject bQueen = null;
    public GameObject bKing = null;

    public int[,] possibleSpots;
    private int selectedRow;
    private int selectedCol;

    // code to load board
    public string[,] board = null;
    public Board boardLoader = null;
    private int nRow;
    private int nCol;

    public Capture capture = null;

    public SimpleAI ai = null;

    public Win win = null;

    public Pregame pregame = null;

    public Moves moves = null;
    [SerializeField] bool dontRotate;
    private float maxTime = 0.4f;
    private float timer = 0f;
    private bool startTimer = false;

    public string[] showOldMove = new string[2] { "", "" };
    public string showSelected = null;
    public string showDanger = null;

    public bool useAI = false;

    public string[,] bPrevBoard = null;
    public string[,] wPrevBoard = null;

    public int bRepCount = 0;
    public int wRepCount = 0;

    public int wLastTurn = 5;
    public int bLastTurn = 6;

    private NetworkManager netManager;


    // Start is called before the first frame update
    private void Start()
    {
        ai = FindObjectOfType<SimpleAI>();
        netManager = FindObjectOfType<NetworkManager>();
        netManager.theWorld = this;
        Debug.Log(netManager.myColor[0]);
        boardSide = 'W';
        turnNumber = 1;
        turn = 'W';
        SceneLoader theLoader = FindObjectOfType<SceneLoader>();
        boardLoader = theLoader.myBoard;
        capture = theLoader.myCapture;
        win = theLoader.myWin;
        pregame = theLoader.myPregame;
        moves = theLoader.myMoves;
        useAI = theLoader.useAI;
        board = boardLoader.getBoard();
        nRow = board.GetLength(0);
        nCol =  board.Length / nRow;
        wPrevBoard = new string[nRow, nCol];
        bPrevBoard = new string[nRow, nCol];
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        for (int i = 0; i < tRow; i++)
        {
            for (int j = 0; j < tCol; j++)
            {
                wPrevBoard[i, j] = board[i, j];
                bPrevBoard[i, j] = board[i, j];
            }
        }
        possibleSpots = new int[nRow, nCol];
        loadPieces();
        win.world = this;
        capture.world = this;
        pregame.world = this;
        moves.world = this;
        moves.bcl = true;
        moves.bcr = true;
        moves.wcl = true;
        moves.wcr = true;
        if(netManager.myColor[0] == 'W')
        {
            Debug.Log("I am the captain now");
            netManager.SendBoard(board);
        }
        GameObject.Find("WhitePlayerName").GetComponent<Text>().text = netManager.whitePlayerName;
        GameObject.Find("BlackPlayerName").GetComponent<Text>().text = netManager.blackPlayerName;
        pregame.loadPregame();
    }
    private void Update()
    {
        if(startTimer)
        {
            timer += Time.deltaTime;
            if(timer > maxTime)
            {
                timer -= maxTime;
                startTimer = false;
                runBoardFlip();
            }
        }
    }

    public void runBoardFlip()
    {
        if (win.isGameOver(turn, board))
        {
            if (turn == 'B')
            {
                GameObject.Find("Win Text").GetComponent<Text>().text = "White Wins!";
                GameObject.Find("WinPopup").GetComponent<Canvas>().enabled = true;
                netManager.GameOver("white");
                Time.timeScale = 0;
            } else if (turn == 'W')
            {
                GameObject.Find("Win Text").GetComponent<Text>().text = "Black Wins!";
                GameObject.Find("WinPopup").GetComponent<Canvas>().enabled = true;
                netManager.GameOver("black");
                Time.timeScale = 0;
            }
        }
        else if (win.isDraw(turn, board))
        {
            if (turn == 'B')
            {
                GameObject.Find("Win Text").GetComponent<Text>().text = "Draw!";
                GameObject.Find("WinPopup").GetComponent<Canvas>().enabled = true;
                netManager.GameOver("draw");
                Time.timeScale = 0;
            }
            else if (turn == 'W')
            {
                GameObject.Find("Win Text").GetComponent<Text>().text = "Draw!";
                GameObject.Find("WinPopup").GetComponent<Canvas>().enabled = true;
                netManager.GameOver("draw");
                Time.timeScale = 0;
            }
        }
        else if (useAI && turn == 'B')
        {
            if (rotatingBoard)
            {
                boardSide = boardSide == 'B' ? 'W' : 'B';
            }
            int tRow = board.GetLength(0);
            int tCol = board.Length / board.GetLength(0);
            int count = 0;
            for (int i = 0; i < tRow; i++)
            {
                for (int j = 0; j < tCol; j++)
                {
                    if(board[i,j][0] == 'W')
                    {
                        count++;
                    }
                }
            }
            int[] AIMove = null;
            if (count < 6)
            {
                AIMove = ai.getBestMove(board, 'B', 5);
            }
            else
            {
                AIMove = ai.getBestMove(board, 'B', 4);
            }
            //SpotBehavior endSpot = null;
            //int[] AIMove = new int[4];
            //do
            //{
            //    AIMove = ai.getBestMove(board, 'B', 0);
            //    endSpot = GameObject.Find(char.ConvertFromUtf32(AIMove[3] + 65) + " (" + (AIMove[2] + 1).ToString() + ")").GetComponent<SpotBehavior>();
            //} while (!win.isGoodMove('B', capture.movementCheck(endSpot, board, AIMove[0], AIMove[1])));
            string spotName = char.ConvertFromUtf32(AIMove[1] + 65) + " (" + (AIMove[0] + 1).ToString() + ")";
            selectSpot(GameObject.Find(spotName).GetComponent<SpotBehavior>());
            spotName = char.ConvertFromUtf32(AIMove[3] + 65) + " (" + (AIMove[2] + 1).ToString() + ")";
            selectSpot(GameObject.Find(spotName).GetComponent<SpotBehavior>());
        }
        else if (rotatingBoard)
        {
            netManager.MyTurnOver();
            boardSide = boardSide == 'B' ? 'W' : 'B';
        }


    }
    private void loadPieces()
    {
        for (int row = 0; row < nRow; row++)
        {
            for (int col = 0; col < nCol; col++)
            {
                if(board[row, col] == "0")
                {
                    continue;
                }
                string spotName = char.ConvertFromUtf32(col + 65) + " (" + (row + 1).ToString() + ")";
                Vector3 tempPos = GameObject.Find(spotName).transform.position;
                GameObject currPiece = null;
                if (board[row, col][0] == 'W')
                {
                    switch (board[row, col][1])
                    {
                        case 'R':
                            currPiece = Instantiate(wRook, Vector3.zero, Quaternion.identity);
                            break;
                        case 'B':
                            currPiece = Instantiate(wBishop, Vector3.zero, Quaternion.identity);
                            break;
                        case 'N':
                            currPiece = Instantiate(wKnight, Vector3.zero, Quaternion.identity);
                            break;
                        case 'K':
                            currPiece = Instantiate(wKing, Vector3.zero, Quaternion.identity);
                            break;
                        case 'Q':
                            currPiece = Instantiate(wQueen, Vector3.zero, Quaternion.identity);
                            break;
                        case 'P':
                            currPiece = Instantiate(wPawn, Vector3.zero, Quaternion.identity);
                            break;
                    }
                    currPiece.AddComponent<PieceBehavior>();
                    currPiece.transform.position = tempPos;
                    currPiece.GetComponent<SpriteRenderer>().sortingLayerName = "PieceMask";
                    currPiece.name = board[row, col];
                }
                else if (board[row, col][0] == 'B')
                {
                    switch (board[row, col][1])
                    {
                        case 'R':
                            currPiece = Instantiate(bRook, Vector3.zero, Quaternion.identity);
                            break;
                        case 'B':
                            currPiece = Instantiate(bBishop, Vector3.zero, Quaternion.identity);
                            break;
                        case 'N':
                            currPiece = Instantiate(bKnight, Vector3.zero, Quaternion.identity);
                            break;
                        case 'K':
                            currPiece = Instantiate(bKing, Vector3.zero, Quaternion.identity);
                            break;
                        case 'Q':
                            currPiece = Instantiate(bQueen, Vector3.zero, Quaternion.identity);
                            break;
                        case 'P':
                            currPiece = Instantiate(bPawn, Vector3.zero, Quaternion.identity);
                            break;
                    }
                    currPiece.AddComponent<PieceBehavior>();
                    currPiece.transform.position = tempPos;
                    currPiece.GetComponent<SpriteRenderer>().sortingLayerName = "PieceMask";
                    currPiece.name = board[row, col];
                }
            }
        }
    }

    public void selectSpot(SpotBehavior spot)
    {
        if (!pregame.doneWithPreGame())
        {
            pregame.spotClicked(spot);

        }
        else
        {
            if (startTimer == true)
            {
                return;
            }
            if (boardSide != netManager.myColor[0])
            {
                return;
            }
            if (possibleSpots[spot.row, spot.col] == 1)
            {
                string[,] tempBoard = capture.movementCheck(spot, board, selectedRow, selectedCol);
                if (win.isGoodMove(turn, tempBoard) && (turn == boardSide || win.isGoodMove(boardSide, tempBoard))) // ensures that a player cannot put themselves in check by moving someone elses piece
                {
                    showOldMove[0] = char.ConvertFromUtf32(selectedCol + 65) + " (" + (selectedRow + 1).ToString() + ")" + "S";
                    showOldMove[1] = char.ConvertFromUtf32(spot.col + 65) + " (" + (spot.row + 1).ToString() + ")" + "S";
                    capture.movementLock(spot, ref board, selectedRow, selectedCol);
                    print("post capture");
                    showSelected = null;
                    selectedCol = -1;
                    selectedRow = -1;
                    possibleSpots = new int[nRow, nCol];
                    turnNumber++;
                    netManager.SendBoard(board);
                    netManager.UpdateOpponentsSquares(showOldMove, showDanger);
                    moves.castleCheck(board);
                    netManager.SendLostPieces();
                    if (capture.changeTurn()) // if the color turn has changed
                    {
                        if (turnNumber == 2)
                        {
                            int tRow = board.GetLength(0);
                            int tCol = board.Length / board.GetLength(0);
                            for (int i = 0; i < tRow; i++)
                            {
                                for (int j = 0; j < tCol; j++)
                                {
                                    bPrevBoard[i, j] = board[i, j];
                                }
                            }
                        }
                        turn = turn == 'B' ? 'W' : 'B';
                        if(turn == 'W' && wLastTurn == turnNumber)
                        {
                            bool boardEqual = true;
                            int tRow = board.GetLength(0);
                            int tCol = board.Length / board.GetLength(0);
                            for (int i = 0; i < tRow && boardEqual; i++)
                            {
                                for (int j = 0; j < tCol && boardEqual; j++)
                                {
                                    if(wPrevBoard[i,j] != board[i,j])
                                    {
                                        boardEqual = false;
                                        break;
                                    }
                                }
                            }
                            wLastTurn += 4;
                            if (boardEqual)
                            {
                                wRepCount++;
                            } else
                            {
                                tRow = board.GetLength(0);
                                tCol = board.Length / board.GetLength(0);
                                for (int i = 0; i < tRow; i++)
                                {
                                    for (int j = 0; j < tCol; j++)
                                    {
                                        wPrevBoard[i, j] = board[i, j];
                                    }
                                }
                                wRepCount = 0;
                            }
                            if(wRepCount == 3)
                            {
                                GameObject.Find("Win Text").GetComponent<Text>().text = "Draw!";
                                GameObject.Find("WinPopup").GetComponent<Canvas>().enabled = true;
                                netManager.GameOver("draw");
                                Time.timeScale = 0;
                            }
                        }
                        if (turn == 'B' && bLastTurn == turnNumber)
                        {
                            bool boardEqual = true;
                            int tRow = board.GetLength(0);
                            int tCol = board.Length / board.GetLength(0);
                            for (int i = 0; i < tRow && boardEqual; i++)
                            {
                                for (int j = 0; j < tCol && boardEqual; j++)
                                {
                                    if (bPrevBoard[i, j] != board[i, j])
                                    {
                                        boardEqual = false;
                                        break;
                                    }
                                }
                            }
                            bLastTurn += 4;
                            if (boardEqual)
                            {
                                bRepCount++;
                            }
                            else
                            {
                                tRow = board.GetLength(0);
                                tCol = board.Length / board.GetLength(0);
                                for (int i = 0; i < tRow; i++)
                                {
                                    for (int j = 0; j < tCol; j++)
                                    {
                                        bPrevBoard[i, j] = board[i, j];
                                    }
                                }
                                bRepCount = 0;
                            }
                            if (bRepCount == 3)
                            {
                                GameObject.Find("Win Text").GetComponent<Text>().text = "Draw!";
                                GameObject.Find("WinPopup").GetComponent<Canvas>().enabled = true;
                                netManager.GameOver("draw");
                                Time.timeScale = 0;
                            }
                        }
                    }
                    if (capture.changeBoard()) // if the irl players turn has changed
                    {
                        runBoardFlip();
                        if (dontRotate)
                        {
                            runBoardFlip();
                        }
                        else
                        {
                            //startTimer = true;
                        }

                    }
                    
                }
            }
            else
            {
                string pieceName = board[spot.row, spot.col];
                if (pieceName != "E" && pieceName[0] == turn && (spot.row != selectedRow || spot.col != selectedCol))
                {
                    showSelected = char.ConvertFromUtf32(spot.col + 65) + " (" + (spot.row + 1).ToString() + ")" + "S";
                    selectedRow = spot.row;
                    selectedCol = spot.col;
                    possibleSpots = getPossibleMoves(pieceName[0], pieceName, spot.row, spot.col);
                    int tRow = board.GetLength(0);
                    int tCol = board.Length / board.GetLength(0);
                    for (int i = 0; i < tRow; i++)
                    {
                        for (int j = 0; j < tCol; j++)
                        {
                            if(possibleSpots[i,j] == 1)
                            {
                                string spotName = char.ConvertFromUtf32(j + 65) + " (" + (i + 1).ToString() + ")";
                                SpotBehavior checkSpot = GameObject.Find(spotName).GetComponent<SpotBehavior>();
                                if (!win.isGoodMove(turn, capture.movementCheck(checkSpot,board,selectedRow,selectedCol)))
                                {
                                    possibleSpots[i, j] = 0;
                                }
                            }
                        }
                    }

                }
                else
                {
                    showSelected = null;
                    selectedCol = -1;
                    selectedRow = -1;
                    possibleSpots = new int[nRow, nCol];
                }
            }
        }
    }

    public int[,] getPossibleMoves(char color, string pieceName, int row, int col)
    {
        return moves.possibleMoves(color, pieceName, row, col, board);
    }

    public void UpdateBoard(string[,] _board)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        for (int i = 0; i < tRow; i++)
        {
            for (int j = 0; j < tCol; j++)
            {
                if(board[i,j].Length > 1)
                {
                    Destroy(GameObject.Find(board[i, j]));
                }
                board[i, j] = _board[i, j];
            }
        }
        loadPieces();
    }

    public void MyTurn()
    {
        turnNumber++;
        turn = netManager.myColor[0];
        boardSide = netManager.myColor[0];
    }

    public void StopMyGame(string condition)
    {
        if(condition == "draw")
        {
            GameObject.Find("Win Text").GetComponent<Text>().text = "Draw!";
            GameObject.Find("WinPopup").GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;
        }
        if(condition == "white")
        {
            GameObject.Find("Win Text").GetComponent<Text>().text = "White Wins!";
            GameObject.Find("WinPopup").GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;
        }
        if(condition == "black")
        {
            GameObject.Find("Win Text").GetComponent<Text>().text = "Black Wins!";
            GameObject.Find("WinPopup").GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;
        }
    }

    public void CancelGame()
    {
        GameObject.Find("Win Text").GetComponent<Text>().text = "Your opponent has left the match";
        GameObject.Find("Win Text").GetComponent<Text>().fontSize = 45;
        GameObject.Find("WinPopup").GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0;
    }
}

