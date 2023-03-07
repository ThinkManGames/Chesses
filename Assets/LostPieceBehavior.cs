using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LostPieceBehavior : MonoBehaviour
{
    Color green = new Color(0, 0.47f, 0, 0.3f);
    public char me = ' ';
    public void Clicked()
    {
        DodgeballCapture capture = (DodgeballCapture)FindObjectOfType<TheWorld>().capture;
        if(capture.placingGuy)
        {
            string worseThanJustTaken = "KQRBNP";
            switch(capture.guyTaken)
            {
                case 'K':
                    break;
                case 'Q':
                    worseThanJustTaken = worseThanJustTaken.Substring(1);
                    break;
                case 'R':
                    worseThanJustTaken = worseThanJustTaken.Substring(2);
                    break;
                case 'B':
                case 'N':
                    worseThanJustTaken = worseThanJustTaken.Substring(3);
                    break;
                case 'P':
                    worseThanJustTaken = worseThanJustTaken.Substring(5);
                    break;
            }
            // if the piece we just took is worse than or equal to the one the player selected to place back on the board, let it happen
            if (worseThanJustTaken.Contains(me.ToString()))
            {
                gameObject.GetComponent<Image>().color = green;
                capture.selectGuy(me);
            }
        }
    }
}
