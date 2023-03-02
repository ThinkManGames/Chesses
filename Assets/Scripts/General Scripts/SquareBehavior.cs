using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBehavior : MonoBehaviour
{
    Color green = new Color(0, 0.47f, 0, 0.6f);
    Color red = new Color(0.47f, 0, 0, 0.6f);
    Color yellow = new Color(0.47f, 0.47f, 0, 0.6f);
    SpriteRenderer myRenderer = null;
    TheWorld world = null;
    string nickname = null;
    int row = -1;
    int col = -1;

    private void Start()
    {
        name += "S";
        world = FindObjectOfType<TheWorld>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        col = name[0] - 65;
        row = name[3] - '0' - 1;
    }
    private void Update()
    {
        if (world.boardLoader.useExtraBoard && world.boardLoader.extraBoard[row, col] == 1)
        {
            myRenderer.color = red;
            myRenderer.enabled = true;
        }
        else
        {
            if (world.showOldMove[0] == name || world.showOldMove[1] == name)
            {
                myRenderer.color = yellow;
                myRenderer.enabled = true;
            }
            else if (world.showSelected == name)
            {
                myRenderer.color = green;
                myRenderer.enabled = true;
            }
            else if (world.showDanger == name)
            {
                myRenderer.color = red;
                myRenderer.enabled = true;
            }
            else
            {
                myRenderer.enabled = false;
            }
        }
    }
}
