using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeballLostPieceAdder : LostPieceAdder
{
    DodgeballCapture capture = null;
    // Start is called before the first frame update
    protected override void Start()
    {
        world = FindObjectOfType<TheWorld>();
        capture = (DodgeballCapture)world.capture;
    }

    // Update is called once per frame
    protected override void Update()
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
                k.AddComponent<LostPieceBehavior>();
                k.AddComponent<BoxCollider2D>();
                k.GetComponent<LostPieceBehavior>().me = 'K';
                k.layer = 12; // lostPieceMask Layer
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
                k.AddComponent<LostPieceBehavior>();
                k.AddComponent<BoxCollider2D>();
                k.GetComponent<LostPieceBehavior>().me = 'Q';
                k.layer = 12; // lostPieceMask Layer
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
                k.AddComponent<LostPieceBehavior>();
                k.AddComponent<BoxCollider2D>();
                k.GetComponent<LostPieceBehavior>().me = 'R';
                k.layer = 12; // lostPieceMask Layer
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
                k.AddComponent<LostPieceBehavior>();
                k.AddComponent<BoxCollider2D>();
                k.GetComponent<LostPieceBehavior>().me = 'N';
                k.layer = 12; // lostPieceMask Layer
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
                k.AddComponent<LostPieceBehavior>();
                k.AddComponent<BoxCollider2D>();
                k.GetComponent<LostPieceBehavior>().me = 'B';
                k.layer = 12; // lostPieceMask Layer
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
                k.AddComponent<LostPieceBehavior>();
                k.AddComponent<BoxCollider2D>();
                k.GetComponent<LostPieceBehavior>().me = 'P';
                k.layer = 12; // lostPieceMask Layer
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

    public virtual void gotAPiece(char type)
    {
        switch (type)
        {
            case 'P':
                lostPawns -= 1;
                break;
            case 'N':
                lostKnights -= 1;
                break;
            case 'B':
                lostBishops -= 1;
                break;
            case 'R':
                lostRooks -= 1;
                break;
            case 'Q':
                lostQueens -= 1;
                break;
            case 'K':
                lostKings -= 1;
                break;
        }
        updateDisplay = true;
    }
}
