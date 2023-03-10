using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad = "Normal Chess";

    public Board myBoard = new Board();
    public Moves myMoves = new Moves();
    public Capture myCapture = new Capture();
    public Win myWin = new Win();
    public Pregame myPregame = new Pregame();
    public bool useAI = false;

    public string gamemode = "";

    [SerializeField] public bool isSinglePlayer;

    public void setBoard(Board _boardToSet) { myBoard = _boardToSet; }
    public void setMoves(Moves _movesToSet) { myMoves = _movesToSet; }
    public void setCapture(Capture _captureToSet) { myCapture = _captureToSet; }
    public void setWin(Win _winToSet) { myWin = _winToSet; }
    public void setPregame(Pregame _pregameToSet) { myPregame = _pregameToSet; }
    public void setAI(bool _aiToggle) { useAI = _aiToggle; }
    public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetSinglePlayer(bool isSinglePlayer)
    {
        this.isSinglePlayer = isSinglePlayer;
    }

    public void loadPreloadedScene()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(sceneToLoad);
    }
    public void ChoseAGamemode()
    {
        gamemode = GameObject.Find("GameSelectionPanelTitle").GetComponent<Text>().text;
        FindObjectOfType<NetworkManager>().HostChoseMode(gamemode);
    }

    public void setPreload(string toLoad)
    {
        sceneToLoad = toLoad;
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("Main Network Menu");
        //FindObjectOfType<NetworkManager>().nameText.text = keepMyName;
        //FindObjectOfType<NetworkManager>().ConnectToGame();
        Destroy(gameObject);
    }

    public void loadTitleScreen()
    {
        if(FindObjectOfType<NetworkManager>() != null)
        {
            Destroy(FindObjectOfType<NetworkManager>().gameObject);
        }
        SceneManager.LoadScene("Title Screen");
        Destroy(gameObject);
    }
}


