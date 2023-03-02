using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWin : Win
{
    public override bool isGameOver(char color, string[,] board)
    {
        return base.isDraw(color, board);
    }

    public override bool isDraw(char color, string[,] board)
    {
        Debug.Log("checking if Checkmate");
        Vector2 rowCol = findKingSpot(color, board);
        int row = (int)rowCol.x;
        int col = (int)rowCol.y;
        if (getDangerousSpots(color, row, col, board) != "")
        {
            Debug.Log("king in check");
            if (kingCantMove(color, row, col, board))
            {
                Debug.Log("king cant move");
                if (attackingCantDie(color, row, col, board))
                {
                    Debug.Log("attacking cant die");
                    if (noneCanBlock(color, row, col, board))
                    {
                        Debug.Log("is Checkmate");
                        return true;
                    }
                }
            }
        }
        Debug.Log("is not checkmate");
        return false;
    }
}
