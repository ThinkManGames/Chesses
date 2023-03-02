using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRandomBoard : Board
{
    public override string[,] getBoard()
    {
        string[] names = new string[5]
        {
            "R", "N", "B", "Q", "P"
        };
        int[] pieceCount = new int[10];
        // 0 is blackrook, 1 is blackknight, 2 is blackbishop, 3 is blackqueen, 4/5 is blackpawn, 6 is whiterook, 7 is whiteknight, 8 is whitebishop, 9 is whitequeen, 10/11 is whitepawn
        int[] pieces = new int[16];
        System.Random rand = new System.Random();
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i] = rand.Next(0, 6);
            if (pieces[i] == 5)
            {
                pieces[i] = 4;
            }
        }
        // 0 is rook, 1 is knight, 2 is bishop, 3 is queen, 4/5 is pawn

        string[,] randomBoard = new string[8, 8];
        int toPickWhite = 0;
        int toPickBlack = 0;
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                if (row <= 1)
                {
                    pieceCount[pieces[toPickWhite]]++;
                    randomBoard[row, col] = "W" + names[pieces[toPickWhite]] + pieceCount[pieces[toPickWhite]].ToString();
                    toPickWhite++;
                }
                else if (row == 7)
                {
                    pieceCount[pieces[toPickBlack - 8] + 5]++;
                    randomBoard[row, col] = "B" + names[pieces[toPickBlack - 8]] + pieceCount[pieces[toPickBlack - 8] + 5].ToString();
                    toPickBlack++;
                }
                else if (row == 6)
                {
                    pieceCount[pieces[toPickBlack + 8] + 5]++;
                    randomBoard[row, col] = "B" + names[pieces[toPickBlack + 8]] + pieceCount[pieces[toPickBlack + 8] + 5].ToString();
                    toPickBlack++;
                }
                else
                {
                    randomBoard[row, col] = "E";
                }
            }
        }
        randomBoard[0, 4] = "WK1";
        randomBoard[7, 4] = "BK1";
        return randomBoard;
    }
}
