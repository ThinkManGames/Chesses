using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    Client myClient;
    public InputField nameText;
    public InputField serverNameText;
    public bool onTheNetwork = false;
    public GameObject GameListContent = null;
    public int currentGameListHeight = 0;
    private List<GameObject> lobbyList = new List<GameObject>();
    public GameObject gameItemPrefab;
    public GameItemBehavior selectedGame = null;
    [SerializeField] GameObject LoadingText;
    [SerializeField] GameObject InvalidGamePopup;
    [SerializeField] GameObject joinButton;
    [SerializeField] GameObject GameSelectionMenu;
    [SerializeField] GameObject ConnectMenu;
    [SerializeField] GameObject JoinOrHostMenu;
    [SerializeField] GameObject ServerErrorPopup;
    [SerializeField] GameObject UsernameText;
    public string selectedMode = "";
    public string myColor = "";
    public string whitePlayerName;
    public string blackPlayerName;

    public TheWorld theWorld;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void ConnectToGame()
    {
        myClient = new Client(nameText.text, serverNameText.text == "" ? "20.253.237.151" : serverNameText.text);
        myClient.manager = this;
        if(!myClient.couldConnect) // client couldnt connect to the server
        {
            myClient = null;
            ServerErrorPopup.SetActive(true);
        } else
        {
            UsernameText.GetComponent<Text>().text = "Username: " + nameText.text;
            UsernameText.SetActive(true);
            ConnectMenu.SetActive(false);
            JoinOrHostMenu.SetActive(true);
            onTheNetwork = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (onTheNetwork)
        {
            //Debug.Log("reading data");
            myClient.ReadData();
        }
    }

    public void JoinButtonClicked()
    {
        currentGameListHeight = 0;
        joinButton.GetComponent<Button>().interactable = false;
        LoadingText.SetActive(true);
        foreach (GameObject gamePanel in lobbyList)
        {
            Destroy(gamePanel);
        }
        myClient.GetGameList();
    }

    public void HostButtonClicked()
    {
        //myClient.SendHost("normal");
    }
    public void HostChoseMode(string mode)
    {
        GameSelectionMenu.SetActive(false);
        myClient.SendHost(mode);
    }
    public void TryingToJoinAGame()
    {
        LoadingText.SetActive(true);
        myClient.SendJoin(selectedGame.gameID);
    }

    public void StopHosting()
    {
        myClient.StopHosting();
    }

    public void MyTurnOver()
    {
        Debug.Log("My turn is over");
        myClient.MyTurnEnded();
        //myClient = null;
    }

    public void GameOver(string condition)
    {
        myClient.SendGameOver(condition);
        myClient.Disconnect();
        myClient = null;
    }

    public void SendBoard(string[,] board)
    {
        myClient.SendBoard(board);
    }

    public void AddGameOption(string _gameID, string _hostName, string _gameType)
    {
        if(LoadingText.activeSelf == true)
        {
            LoadingText.SetActive(false);
        }
        GameObject newItem = Instantiate(gameItemPrefab, GameListContent.transform);
        Vector3 oldPos = newItem.transform.localPosition;
        oldPos.y -= currentGameListHeight * 100;
        newItem.transform.localPosition = oldPos;
        currentGameListHeight++;
        lobbyList.Add(newItem);
        newItem.GetComponent<GameItemBehavior>().gameID = _gameID;
        newItem.GetComponent<GameItemBehavior>().hostName = _hostName;
        newItem.GetComponent<GameItemBehavior>().gameType = _gameType;
        Debug.Log("gameID: " + _gameID + " hostName: " + _hostName + " gameType: " + _gameType);
    }

    public void InvalidGame()
    {
        InvalidGamePopup.SetActive(true);
        LoadingText.SetActive(false);
        Debug.Log("Invalid Game");
    }

    public void GotNewBoard(string[,] board)
    {
        string message = "";
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        for (int i = 0; i < tRow; i++)
        {
            for (int j = 0; j < tCol; j++)
            {
                message += board[i, j] + " ";
            }
            message += "\n";
        }
        theWorld.UpdateBoard(board);
        Debug.Log(message);
    }

    public void StartTurn()
    {
        Debug.Log("starting turn");
        theWorld.MyTurn();
    }

    public void StartGame(string color, string mode, string whiteName, string blackName)
    {
        GameSelectionMenu.SetActive(true);
        GameObject.Find(mode).GetComponent<Button>().onClick.Invoke();
        myColor = color;
        FindObjectOfType<SceneLoader>().loadPreloadedScene();
        Debug.Log("starting game as: " + color);
        whitePlayerName = whiteName;
        blackPlayerName = blackName;
    }

    public void EndGame(string condition)
    {
        theWorld.StopMyGame(condition);
        Debug.Log("ending game due to: " + condition);
    }

    public void GameSelected(GameItemBehavior gameItem)
    {
        selectedGame = gameItem;
        joinButton.GetComponent<Button>().interactable = true;
    }

    public void ReturnToMenu()
    {
        if(myClient != null)
        {
            myClient.Disconnect();
        }
        FindObjectOfType<SceneLoader>().loadMainMenu();
        Destroy(gameObject);
    }

    public void NoRematch()
    {

    }

    public void CancelGame()
    {
        theWorld.CancelGame();
    }
    
    public void UpdateOpponentsSquares(string[] oldSquares, string dangerousSpot)
    {
        if (dangerousSpot != null)
        {
            Debug.Log("this is the dangerous spot: " + dangerousSpot);
        }
        else
        {
            Debug.Log("no dangerous spot");
        }
        string[] thingToSend = new string[3] { "NONE", "NONE", "NONE" };
        thingToSend[0] = oldSquares[0];
        thingToSend[1] = oldSquares[1];
        if (oldSquares[0] == "")
        {
            thingToSend[0] = "NONE";
        }
        if (oldSquares[1] == "")
        {
            thingToSend[1] = "NONE";
        }

        if(dangerousSpot != null && dangerousSpot != "")
        {
            thingToSend[2] = dangerousSpot;
        } else
        {
            thingToSend[2] = "NONE";
        }
        myClient.UpdateOpponentsSquares(thingToSend);
    }

    public void UpdateMySquares(string[] squares)
    {
        theWorld.showOldMove[0] = squares[1] == "NONE" ? "" : squares[1];
        theWorld.showOldMove[1] = squares[2] == "NONE" ? "" : squares[2];
        theWorld.showDanger = squares[3] == "NONE" ? null : squares[3];
        Debug.Log(squares[3]);
    }

    public void SendLostPieces()
    {
        string lostBPieces = "B" + GameObject.Find("DeadBlack").GetComponent<LostPieceAdder>().GetAllLostPieces().ToString();
        myClient.SendLostPieces(lostBPieces);
        string lostWPieces = "W" + GameObject.Find("DeadWhite").GetComponent<LostPieceAdder>().GetAllLostPieces().ToString();
        myClient.SendLostPieces(lostWPieces);
    }

    public void UpdateMyDeaths(string[] lines)
    {
        if(lines[1] == "B")
        {
            GameObject.Find("DeadBlack").GetComponent<LostPieceAdder>().PossibleDeaths(lines);
        }
        if (lines[1] == "W")
        {
            GameObject.Find("DeadWhite").GetComponent<LostPieceAdder>().PossibleDeaths(lines);
        }
    }
}
