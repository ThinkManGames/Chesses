using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostPieceBehavior : MonoBehaviour
{
    Color green = new Color(0, 0.47f, 0, 0.3f);
    public char me = ' ';
    public void Clicked()
    {
        DodgeballCapture capture = (DodgeballCapture)FindObjectOfType<TheWorld>().capture;
        if(capture.placingGuy)
        {
            string worseThanMe = "KQRBNP";
            switch(me)
            {
                case 'K':
                    break;
                case 'Q':
                    worseThanMe = worseThanMe.Substring(1);
                    break;
                case 'R':
                    worseThanMe = worseThanMe.Substring(2);
                    break;
                case 'B':
                case 'N':
                    worseThanMe = worseThanMe.Substring(3);
                    break;
                case 'P':
                    worseThanMe = worseThanMe.Substring(5);
                    break;
            }
            // if the piece we just took is worse than or equal to the one the player selected to place back on the board, let it happen
            if(worseThanMe.Contains(capture.guyTaken.ToString()))
            {
                gameObject.GetComponent<SpriteRenderer>().color = green;
                capture.selectGuy(me);
            }
        }
    }
}
