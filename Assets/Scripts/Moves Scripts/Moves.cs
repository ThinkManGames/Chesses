using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : MonoBehaviour
{
    public TheWorld world = null;
    public bool wcl = true; // white can castle to the left
    public bool wcr = true; // white can castle to the right
    public bool bcl = true; // black can castle to the left
    public bool bcr = true; // black can castle to the right

    protected int[,] knightMoves = new int[8, 2]
{
        {2, -1 },
        {2, 1 },
        {-2, -1 },
        {-2, 1 },
        {-1, 2 },
        {1, 2 },
        {-1, -2 },
        {1, -2 }
};

    protected int[,] BishopMoves = new int[4, 2]
    {
        {1, -1 },
        {1, 1 },
        {-1, 1 },
        {-1, -1 }
    };

    protected int[,] RookMoves = new int[4, 2]
    {
        {0, -1 },
        {0, 1 },
        {-1, 0 },
        {1, 0 }
    };

    protected int[,] QueenKingMoves = new int[8, 2]
{
        {0, -1 },
        {0, 1 },
        {-1, 0 },
        {1, 0 },
        {1, -1 },
        {1, 1 },
        {-1, 1 },
        {-1, -1 }
};

    private void Start()
    {
        wcl = true; // white can castle to the left
        wcr = true; // white can castle to the right
        bcl = true; // black can castle to the left
        bcr = true; // black can castle to the right
    }
    public virtual int[,] possibleMoves(char color, string pieceName, int row, int col, string[,] board)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        int[,] tempBoard = new int[tRow, tCol];
        char toSwitch = pieceName[1];
        switch (toSwitch)
        {
            case 'P':
                if (color == 'W')
                {
                    if (row < tRow - 1)
                    {
                        // if the pawn is not on the left edge and the left spot either is empty or has a black piece
                        if (col > 0 && board[row + 1, col - 1][0] == 'B')
                        {
                            //string spotName = char.ConvertFromUtf32(col - 1 + 65) + " (" + (row + 1 + 1).ToString() + ")";
                            //if(world.win.isGoodMove(color, world.capture.movementCheck(GameObject.Find(spotName).GetComponent<SpotBehavior>(), board, row, col)));
                            tempBoard[row + 1, col - 1] = 1;
                        }
                        // if the pawn is not on the right edge and the right spot is either empty or has a black piece
                        if (col < tCol - 1 && board[row + 1, col + 1][0] == 'B')
                        {
                            tempBoard[row + 1, col + 1] = 1;
                        }
                        if (board[row + 1, col] == "E")
                        {
                            if (row == 1 && board[row + 2, col] == "E")
                            {
                                tempBoard[row + 2, col] = 1;
                            }
                            tempBoard[row + 1, col] = 1;
                        }
                    }
                    else
                    {
                        // this should never happen if we implement pawn upgrades
                    }
                }
                else if (color == 'B')
                {
                    if (row > 0)
                    {
                        // if the pawn is not on the left edge and the left spot either is empty or has a black piece
                        if (col > 0 && board[row - 1, col - 1][0] == 'W')
                        {
                            tempBoard[row - 1, col - 1] = 1;
                        }
                        // if the pawn is not on the right edge and the right spot is either empty or has a black piece
                        if (col < tCol - 1 && board[row - 1, col + 1][0] == 'W')
                        {
                            tempBoard[row - 1, col + 1] = 1;
                        }
                        if (board[row - 1, col] == "E")
                        {
                            if (row == tRow - 2 && board[row - 2, col] == "E")
                            {
                                tempBoard[row - 2, col] = 1;
                            }
                            tempBoard[row - 1, col] = 1;
                        }
                    }
                    else
                    {
                        // this should never happen if we implement pawn upgrades
                    }
                }
                break;
            case 'N':

                for (int i = 0; i < 8; i++)
                {
                    if (row + knightMoves[i, 0] >= 0 && row + knightMoves[i, 0] < tRow && col + knightMoves[i, 1] >= 0 && col + knightMoves[i, 1] < tCol && board[row + knightMoves[i, 0], col + knightMoves[i, 1]][0] != color && board[row + knightMoves[i, 0], col + knightMoves[i, 1]] != "0")
                    {
                        tempBoard[row + knightMoves[i, 0], col + knightMoves[i, 1]] = 1;
                    }
                }
                break;
            case 'B':
                for (int i = 0; i < 4; i++)
                {
                    int rowAdder = BishopMoves[i, 0];
                    int colAdder = BishopMoves[i, 1];
                    int currRow = row + rowAdder;
                    int currCol = col + colAdder;
                    
                    while (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol][0] != color && board[currRow, currCol] != "0")
                    {
                        tempBoard[currRow, currCol] = 1;
                        if (board[currRow, currCol][0] != 'E')
                        {
                            break;
                        }
                        currRow += rowAdder;
                        currCol += colAdder;
                    }
                }
                break;
            case 'R':
                for (int i = 0; i < 4; i++)
                {
                    int rowAdder = RookMoves[i, 0];
                    int colAdder = RookMoves[i, 1];
                    int currRow = row + rowAdder;
                    int currCol = col + colAdder;
                    while (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol][0] != color && board[currRow, currCol] != "0")
                    {
                        tempBoard[currRow, currCol] = 1;
                        if (board[currRow, currCol][0] != 'E')
                        {
                            break;
                        }
                        currRow += rowAdder;
                        currCol += colAdder;
                    }
                }
                break;
            case 'Q':
                for (int i = 0; i < 8; i++)
                {
                    int rowAdder = QueenKingMoves[i, 0];
                    int colAdder = QueenKingMoves[i, 1];
                    int currRow = row + rowAdder;
                    int currCol = col + colAdder;
                    while (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol][0] != color && board[currRow, currCol] != "0")
                    {
                        tempBoard[currRow, currCol] = 1;
                        if (board[currRow, currCol][0] != 'E')
                        {
                            break;
                        }
                        currRow += rowAdder;
                        currCol += colAdder;
                    }
                }
                break;
            case 'K':
                for (int i = 0; i < 8; i++)
                {
                    int rowAdder = QueenKingMoves[i, 0];
                    int colAdder = QueenKingMoves[i, 1];
                    int currRow = row + rowAdder;
                    int currCol = col + colAdder;
                    if (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol][0] != color && board[currRow, currCol] != "0")
                    {
                        tempBoard[currRow, currCol] = 1;
                    }
                }
                if(pieceName[0] != 'T') // if we are testing
                {
                    if (world.win.getDangerousSpots(color, row, col, board) == "") // and the king is not in check
                    {
                        if (color == 'W') // if the king is white
                        {
                            if (wcl) // and the king and left rook have not moved
                            {
                                if (board[row, 1] == "E" && board[row, 2] == "E" && board[row, 3] == "E") // and the spots inbetween the king and rook are empty
                                {
                                    if (world.win.getDangerousSpots(color, row, 1, board) == "" && world.win.getDangerousSpots(color, row, 2, board) == "" && world.win.getDangerousSpots(color, row, 3, board) == "") // and nobody can attack either spot
                                    {
                                        // THEN we can castle to the left as white
                                        tempBoard[row, 2] = 1;
                                    }
                                }
                            }
                            if (wcr) // and the king and right rook have not moved
                            {
                                if (board[row, 5] == "E" && board[row, 6] == "E") // and the spots inbetween the king and rook are empty
                                {
                                    if (world.win.getDangerousSpots(color, row, 5, board) == "" && world.win.getDangerousSpots(color, row, 6, board) == "") // and nobody can attack either spot
                                    {
                                        // THEN we can castle to the right as white
                                        tempBoard[row, 6] = 1;
                                    }
                                }
                            }
                        }
                        if (color == 'B') // if the king is black
                        {
                            if (bcl) // and the king and left rook have not moved
                            {
                                if (board[row, 1] == "E" && board[row, 2] == "E" && board[row, 3] == "E") // and the spots inbetween the king and rook are empty
                                {
                                    if (world.win.getDangerousSpots(color, row, 1, board) == "" && world.win.getDangerousSpots(color, row, 2, board) == "" && world.win.getDangerousSpots(color, row, 3, board) == "") // and nobody can attack either spot
                                    {
                                        // THEN we can castle to the right as black
                                        tempBoard[row, 2] = 1;
                                    }
                                }
                            }
                            if (bcr) // and the king and right rook have not moved
                            {
                                if (board[row, 5] == "E" && board[row, 6] == "E") // and the spots inbetween the king and rook are empty
                                {
                                    if (world.win.getDangerousSpots(color, row, 5, board) == "" && world.win.getDangerousSpots(color, row, 6, board) == "") // and nobody can attack either spot
                                    {
                                        // THEN we can castle to the right as black
                                        tempBoard[row, 6] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
                break;
        }
        return tempBoard;

    }

    public virtual void castleCheck(string[,] board)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        if (board[0,0] == "E" || board[0, 0][1] != 'R') // if the left rook has moved, white cant castle left
        {
            wcl = false;
        }
        if (board[0, tCol - 1] == "E" || board[0, tCol - 1][1] != 'R') // if the right rook has moved, white cant castle right
        {
            wcr = false;
        }
        if (board[tRow - 1, 0][0] == 'E' || board[tRow - 1, 0][1] != 'R') // black cant castle left
        {
            bcl = false;
        }
        if (board[tRow - 1, tCol - 1][0] == 'E' || board[tRow - 1, tCol - 1][1] != 'R') // black cant castle right
        {
            bcr = false;
        }
        if (board[0, 4][0] == 'E' || board[0, 4][1] != 'K') // THIS ONLY WORKS FOR NORMAL BOARD SIZES, WILL MESS UP LARGER BOARDS
        {
            wcl = false;
            wcr = false;
        }
        if (board[tRow - 1, 4][0] == 'E' || board[tRow - 1, 4][1] != 'K') // THIS ONLY WORKS FOR NORMAL BOARD SIZES, WILL MESS UP LARGER BOARDS
        {
            bcl = false;
            bcr = false;
        }
    }


    public virtual List<int[]> posMoveList(char color, string pieceName, int row, int col, string[,] board)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        List<int[]> tempBoard = new List<int[]>();
        char toSwitch = pieceName[1];
        switch (toSwitch)
        {
            case 'P':
                if (color == 'W')
                {
                    if (row < tRow - 1)
                    {
                        // if the pawn is not on the left edge and the left spot either is empty or has a black piece
                        if (col > 0 && board[row + 1, col - 1][0] == 'B')
                        {
                            //string spotName = char.ConvertFromUtf32(col - 1 + 65) + " (" + (row + 1 + 1).ToString() + ")";
                            //if(world.win.isGoodMove(color, world.capture.movementCheck(GameObject.Find(spotName).GetComponent<SpotBehavior>(), board, row, col)));
                            tempBoard.Add(new int[] { row + 1, col - 1 });
                        }
                        // if the pawn is not on the right edge and the right spot is either empty or has a black piece
                        if (col < tCol - 1 && board[row + 1, col + 1][0] == 'B')
                        {
                            tempBoard.Add(new int[] { row + 1, col + 1 });
                        }
                        if (board[row + 1, col] == "E")
                        {
                            if (row == 1 && board[row + 2, col] == "E")
                            {
                                tempBoard.Add(new int[] { row + 2, col });
                            }
                            tempBoard.Add(new int[] { row + 1, col });
                        }
                    }
                    else
                    {
                        // this should never happen if we implement pawn upgrades
                    }
                }
                else if (color == 'B')
                {
                    if (row > 0)
                    {
                        // if the pawn is not on the left edge and the left spot either is empty or has a black piece
                        if (col > 0 && board[row - 1, col - 1][0] == 'W')
                        {
                            tempBoard.Add(new int[] { row - 1, col - 1 });
                        }
                        // if the pawn is not on the right edge and the right spot is either empty or has a black piece
                        if (col < tCol - 1 && board[row - 1, col + 1][0] == 'W')
                        {
                            tempBoard.Add(new int[] { row - 1, col + 1 });
                        }
                        if (board[row - 1, col] == "E")
                        {
                            if (row == tRow - 2 && board[row - 2, col] == "E")
                            {
                                tempBoard.Add(new int[] { row - 2, col });
                            }
                            tempBoard.Add(new int[] { row - 1, col });
                        }
                    }
                    else
                    {
                        // this should never happen if we implement pawn upgrades
                    }
                }
                break;
            case 'N':

                for (int i = 0; i < 8; i++)
                {
                    if (row + knightMoves[i, 0] >= 0 && row + knightMoves[i, 0] < tRow && col + knightMoves[i, 1] >= 0 && col + knightMoves[i, 1] < tCol && board[row + knightMoves[i, 0], col + knightMoves[i, 1]][0] != color && board[row + knightMoves[i, 0], col + knightMoves[i, 1]] != "0")
                    {
                        tempBoard.Add(new int[] { row + knightMoves[i, 0], col + knightMoves[i, 1] });
                    }
                }
                break;
            case 'B':
                for (int i = 0; i < 4; i++)
                {
                    int rowAdder = BishopMoves[i, 0];
                    int colAdder = BishopMoves[i, 1];
                    int currRow = row + rowAdder;
                    int currCol = col + colAdder;

                    while (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol][0] != color && board[currRow, currCol] != "0")
                    {
                        tempBoard.Add(new int[] { currRow, currCol });
                        if (board[currRow, currCol][0] != 'E')
                        {
                            break;
                        }
                        currRow += rowAdder;
                        currCol += colAdder;
                    }
                }
                break;
            case 'R':
                for (int i = 0; i < 4; i++)
                {
                    int rowAdder = RookMoves[i, 0];
                    int colAdder = RookMoves[i, 1];
                    int currRow = row + rowAdder;
                    int currCol = col + colAdder;
                    while (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol][0] != color && board[currRow, currCol] != "0")
                    {
                        tempBoard.Add(new int[] { currRow, currCol });
                        if (board[currRow, currCol][0] != 'E')
                        {
                            break;
                        }
                        currRow += rowAdder;
                        currCol += colAdder;
                    }
                }
                break;
            case 'Q':
                for (int i = 0; i < 8; i++)
                {
                    int rowAdder = QueenKingMoves[i, 0];
                    int colAdder = QueenKingMoves[i, 1];
                    int currRow = row + rowAdder;
                    int currCol = col + colAdder;
                    while (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol][0] != color && board[currRow, currCol] != "0")
                    {
                        tempBoard.Add(new int[] { currRow, currCol });
                        if (board[currRow, currCol][0] != 'E')
                        {
                            break;
                        }
                        currRow += rowAdder;
                        currCol += colAdder;
                    }
                }
                break;
            case 'K':
                for (int i = 0; i < 8; i++)
                {
                    int rowAdder = QueenKingMoves[i, 0];
                    int colAdder = QueenKingMoves[i, 1];
                    int currRow = row + rowAdder;
                    int currCol = col + colAdder;
                    if (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol][0] != color && board[currRow, currCol] != "0")
                    {
                        tempBoard.Add(new int[] { currRow, currCol });
                    }
                }
                if (pieceName[0] != 'T') // if we are testing
                {
                    if (world.win.getDangerousSpots(color, row, col, board) == "") // and the king is not in check
                    {
                        if (color == 'W') // if the king is white
                        {
                            if (wcl) // and the king and left rook have not moved
                            {
                                if (board[row, 1] == "E" && board[row, 2] == "E" && board[row, 3] == "E") // and the spots inbetween the king and rook are empty
                                {
                                    if (world.win.getDangerousSpots(color, row, 1, board) == "" && world.win.getDangerousSpots(color, row, 2, board) == "" && world.win.getDangerousSpots(color, row, 3, board) == "") // and nobody can attack either spot
                                    {
                                        // THEN we can castle to the left as white
                                        tempBoard.Add(new int[] { row, 2 });
                                    }
                                }
                            }
                            if (wcr) // and the king and right rook have not moved
                            {
                                if (board[row, 5] == "E" && board[row, 6] == "E") // and the spots inbetween the king and rook are empty
                                {
                                    if (world.win.getDangerousSpots(color, row, 5, board) == "" && world.win.getDangerousSpots(color, row, 6, board) == "") // and nobody can attack either spot
                                    {
                                        // THEN we can castle to the right as white
                                        tempBoard.Add(new int[] { row, 6 });
                                    }
                                }
                            }
                        }
                        if (color == 'B') // if the king is black
                        {
                            if (bcl) // and the king and left rook have not moved
                            {
                                if (board[row, 1] == "E" && board[row, 2] == "E" && board[row, 3] == "E") // and the spots inbetween the king and rook are empty
                                {
                                    if (world.win.getDangerousSpots(color, row, 1, board) == "" && world.win.getDangerousSpots(color, row, 2, board) == "" && world.win.getDangerousSpots(color, row, 3, board) == "") // and nobody can attack either spot
                                    {
                                        // THEN we can castle to the right as black
                                        tempBoard.Add(new int[] { row, 2 });
                                    }
                                }
                            }
                            if (bcr) // and the king and right rook have not moved
                            {
                                if (board[row, 5] == "E" && board[row, 6] == "E") // and the spots inbetween the king and rook are empty
                                {
                                    if (world.win.getDangerousSpots(color, row, 5, board) == "" && world.win.getDangerousSpots(color, row, 6, board) == "") // and nobody can attack either spot
                                    {
                                        // THEN we can castle to the right as black
                                        tempBoard.Add(new int[] { row, 6 });
                                    }
                                }
                            }
                        }
                    }
                }
                break;
        }
        return tempBoard;

    }
}
