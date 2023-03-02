using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotBehavior : MonoBehaviour
{
    public TheWorld theWorld = null;
    public int row;
    public int col;
    private Color myColor;
    private void Start()
    {
        theWorld = FindObjectOfType<TheWorld>();

        myColor = GetComponent<SpriteRenderer>().material.color;

    }
    private void Update()
    {

        if (theWorld.possibleSpots[row, col] == 1)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        
    }
}
