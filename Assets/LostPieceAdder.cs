using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostPieceAdder : MonoBehaviour
{

    public GameObject Pawn = null;
    public GameObject Rook = null;
    public GameObject Knight = null;
    public GameObject Bishop = null;
    public GameObject Queen = null;
    public GameObject King = null;

    protected TheWorld world = null;
    [SerializeField] protected int startX;
    [SerializeField] protected int startY;
    protected int lostPawns = 0;
    protected int lostBishops = 0;
    protected int lostKnights = 0;
    protected int lostRooks = 0;
    protected int lostQueens = 0;
    protected int lostKings = 0;
    protected List<GameObject> lostPieces = new List<GameObject>();
    protected bool updateDisplay = false;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        world = FindObjectOfType<TheWorld>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(updateDisplay)
        {
            updateDisplay = false;
            foreach (GameObject piece in lostPieces)
            {
                Destroy(piece);
            }
            lostPieces.Clear();
            int xInc = 0;
            int yInc = 0;
            for(int i = 1; i <= lostKings; i++)
            {
                GameObject k = Instantiate(King,gameObject.transform);
                lostPieces.Add(k);
                k.transform.localPosition = new Vector3(startX + xInc, startY + yInc, 0);
                xInc += 50;
                if(xInc == 350)
                {
                    xInc = 0;
                    yInc += -50;
                }
            }
            for (int i = 1; i <= lostQueens; i++)
            {
                GameObject k = Instantiate(Queen, gameObject.transform);
                lostPieces.Add(k);
                k.transform.localPosition = new Vector3(startX + xInc, startY + yInc, 0);
                xInc += 50;
                if (xInc == 350)
                {
                    xInc = 0;
                    yInc += -50;
                }
            }
            for (int i = 1; i <= lostRooks; i++)
            {
                GameObject k = Instantiate(Rook, gameObject.transform);
                lostPieces.Add(k);
                k.transform.localPosition = new Vector3(startX + xInc, startY + yInc, 0);
                xInc += 50;
                if (xInc == 350)
                {
                    xInc = 0;
                    yInc += -50;
                }
            }
            for (int i = 1; i <= lostKnights; i++)
            {
                GameObject k = Instantiate(Knight, gameObject.transform);
                lostPieces.Add(k);
                k.transform.localPosition = new Vector3(startX + xInc, startY + yInc, 0);
                xInc += 50;
                if (xInc == 350)
                {
                    xInc = 0;
                    yInc += -50;
                }
            }
            for (int i = 1; i <= lostBishops; i++)
            {
                GameObject k = Instantiate(Bishop, gameObject.transform);
                lostPieces.Add(k);
                k.transform.localPosition = new Vector3(startX + xInc, startY + yInc, 0);
                xInc += 50;
                if (xInc == 350)
                {
                    xInc = 0;
                    yInc += -50;
                }
            }
            for (int i = 1; i <= lostPawns; i++)
            {
                GameObject k = Instantiate(Pawn, gameObject.transform);
                lostPieces.Add(k);
                k.transform.localPosition = new Vector3(startX + xInc, startY + yInc, 0);
                xInc += 50;
                if (xInc == 350)
                {
                    xInc = 0;
                    yInc += -50;
                }
            }
        }
    }

    public virtual void lostAPiece(char type) {
        switch(type)
        {
            case 'P':
                lostPawns += 1;
                break;
            case 'N':
                lostKnights += 1;
                break;
            case 'B':
                lostBishops += 1;
                break;
            case 'R':
                lostRooks += 1;
                break;
            case 'Q':
                lostQueens += 1;
                break;
            case 'K':
                lostKings += 1;
                break;
        }
        updateDisplay = true;
    }

    public virtual string GetAllLostPieces()
    {
        string toSend = "";
        for(int i = 0; i < lostPawns;i++)
        {
            toSend += 'P';
        }
        for (int i = 0; i < lostKnights; i++)
        {
            toSend += 'N';
        }
        for (int i = 0; i < lostBishops; i++)
        {
            toSend += 'B';
        }
        for (int i = 0; i < lostRooks; i++)
        {
            toSend += 'R';
        }
        for (int i = 0; i < lostQueens; i++)
        {
            toSend += 'Q';
        }
        for (int i = 0; i < lostKings; i++)
        {
            toSend += 'K';
        }
        return toSend;
    }

    public virtual void PossibleDeaths(string[] deaths)
    {
        lostPawns = 0;
        lostKnights = 0;
        lostBishops = 0;
        lostRooks = 0;
        lostQueens = 0;
        lostKings = 0;
        if(deaths.Length < 4)
        {
            return;
        }
        for (int i = 2; i < deaths.Length - 1; i++)
        {
            lostAPiece(deaths[i][0]);
        }
    }
}
