using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainButton : MonoBehaviour
{
    public void onClick()
    {
        SceneLoader loader = FindObjectOfType<SceneLoader>();
        string sceneName = SceneManager.GetActiveScene().name;
        Time.timeScale = 1;
        loader.loadScene(sceneName);
    }
}
