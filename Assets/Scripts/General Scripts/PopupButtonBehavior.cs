using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupButtonBehavior : MonoBehaviour
{
    [SerializeField] Sprite blackSprite = null;
    [SerializeField] Sprite whiteSprite = null;
    private GameObject pawn = null;
    private int row = -1;
    private int col = -1;
    private char color;
    public void setColor(char _color)
    {
        if (_color == 'W')
        {
            GetComponent<Image>().sprite = whiteSprite;
        }
        else if (_color == 'B')
        {
            GetComponent<Image>().sprite = blackSprite;
        }
        color = _color;
    }

    public void whereToReplace(int _row, int _col)
    {
        row = _row;
        col = _col;
    }
    public void buttonPressed()
    {
        TheWorld world = GameObject.Find("TheWorld").GetComponent<TheWorld>();
        int tRow = world.board.GetLength(0);
        int tCol = world.board.Length / world.board.GetLength(0);
        int myNewNum = 0;
        for (int r = 0; r < tRow; r++)
        {
            for (int c = 0; c < tCol; c++)
            {
                if (world.board[r, c][0] == color && world.board[r, c][1] == name[0])
                {
                    if (world.board[r, c].Length == 4)
                    {
                        if ((world.board[r, c][2] - '0') * 10 + (world.board[r, c][3] - '0') > myNewNum)
                        {
                            myNewNum = (world.board[r, c][2] - '0') * 10 + (world.board[r, c][3] - '0');
                        }
                    }
                    else if (world.board[r, c][2] - '0' > myNewNum)
                    {
                        myNewNum = world.board[r, c][2] - '0';
                    }
                }
            }
        }
        myNewNum++;
        string newName = color.ToString() + name[0].ToString() + myNewNum.ToString();
        world.board[row, col] = newName;

        string spotName = char.ConvertFromUtf32(col + 65) + " (" + (row + 1).ToString() + ")";
        Vector3 myTemp = GameObject.Find(spotName).transform.position;
        myTemp.z -= 0.1f;
        GameObject currPiece = null;
        if (color == 'W')
        {
            switch (name[0])
            {
                case 'R':
                    currPiece = Instantiate(world.wRook, Vector3.zero, Quaternion.identity);
                    break;
                case 'B':
                    currPiece = Instantiate(world.wBishop, Vector3.zero, Quaternion.identity);
                    break;
                case 'K':
                    currPiece = Instantiate(world.wKnight, Vector3.zero, Quaternion.identity);
                    break;
                case 'Q':
                    currPiece = Instantiate(world.wQueen, Vector3.zero, Quaternion.identity);
                    break;
            }
            currPiece.AddComponent<PieceBehavior>();
            currPiece.transform.position = myTemp;
            currPiece.GetComponent<SpriteRenderer>().sortingLayerName = "PieceMask";
            currPiece.name = newName;

        }
        if (color == 'B')
        {
            switch (name[0])
            {
                case 'R':
                    currPiece = Instantiate(world.bRook, Vector3.zero, Quaternion.identity);
                    break;
                case 'B':
                    currPiece = Instantiate(world.bBishop, Vector3.zero, Quaternion.identity);
                    break;
                case 'K':
                    currPiece = Instantiate(world.bKnight, Vector3.zero, Quaternion.identity);
                    break;
                case 'Q':
                    currPiece = Instantiate(world.bQueen, Vector3.zero, Quaternion.identity);
                    break;
            }
            currPiece.AddComponent<PieceBehavior>();
            currPiece.transform.position = myTemp;
            currPiece.GetComponent<SpriteRenderer>().sortingLayerName = "PieceMask";
            currPiece.name = newName;
        }
        Time.timeScale = 1;
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        //FindObjectOfType<TheWorld>().capture.SelectedPiece();
        FindObjectOfType<NetworkManager>().SendBoard(world.board);

    }
}
