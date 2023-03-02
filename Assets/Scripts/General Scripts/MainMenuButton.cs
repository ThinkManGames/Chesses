using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public void buttonClicked()
    {
        FindObjectOfType<NetworkManager>().ReturnToMenu();
        //SceneLoader loader = FindObjectOfType<SceneLoader>();
        //Time.timeScale = 1;
        //loader.loadMainMenu();
    }
}
