using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedrunConfirmationPopup : MonoBehaviour
{
    Canvas canvas = null;
    TheWorld world = null;
    char boardSide = 'B';
    bool isOk = true;
    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();
        canvas.enabled = false;
        world = FindObjectOfType<TheWorld>();
        if(world.isSinglePlayer != true)
        {
            isOk = FindObjectOfType<NetworkManager>().myColor[0] == 'W';
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if(world.isSinglePlayer != true)
        {
            if(world.boardSide != FindObjectOfType<NetworkManager>().myColor[0])
            {
                isOk = true;
            }
            if (world.boardSide == FindObjectOfType<NetworkManager>().myColor[0] && isOk == true)
            {
                isOk = false;
                Time.timeScale = 0;
                canvas.enabled = true;
            }
        } else
        {
            if (world.boardSide != this.boardSide)
            {
                this.boardSide = world.boardSide;
                Time.timeScale = 0;
                canvas.enabled = true;
            }
        }
    }
}
