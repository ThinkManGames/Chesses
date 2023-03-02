using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    public void onClick()
    {
        GameObject.Find("PausePanel").SetActive(false);
        Time.timeScale = 1;
    }
}
