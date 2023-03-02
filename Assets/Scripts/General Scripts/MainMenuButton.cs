using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public void buttonClicked()
    {
        if (FindObjectOfType<NetworkManager>() != null)
        {
            FindObjectOfType<NetworkManager>().ReturnToMenu();
        }
        else
        {
            SceneLoader loader = FindObjectOfType<SceneLoader>();
            Time.timeScale = 1;
            loader.loadTitleScreen();
        }
    }
}
