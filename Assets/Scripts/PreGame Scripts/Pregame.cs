using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pregame : MonoBehaviour
{
    public TheWorld world;
    public virtual bool doneWithPreGame()
    {
        return true;
    }

    public virtual void spotClicked(SpotBehavior spot)
    {
        return;
    }

    public virtual string getWin(char color)
    {
        return "";
    }
    public virtual void loadPregame()
    {
        return;
    }
}
