using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader1 : MonoBehaviour
{
    public string sceneToLoad = "Normal Chess";

    public Board myBoard = null;
    public Moves myMoves = null;
    public Capture myCapture = null;
    public Win myWin = null;
    public Pregame myPregame = null;
    public bool useAI = false;

    public string gamemode = "";

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

    public void loadMainMenu() // should be chose a gamemode idk why it wont work
    {
        gamemode = GameObject.Find("GameSelectionPanelTitle").GetComponent<Text>().text;
        FindObjectOfType<NetworkManager>().HostChoseMode(gamemode);
    }

    public void StartGamemode(string mode)
    {

    }
}



