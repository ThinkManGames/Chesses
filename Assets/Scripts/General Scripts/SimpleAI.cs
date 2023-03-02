using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour
{
    private TheWorld world = null;
    // Start is called before the first frame update
    void Start()
    {
        world = FindObjectOfType<TheWorld>();
    }

    private double[,] bPawn = new double[8, 8] {
        {0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0},
        {5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0},
        {1.0,  1.0,  2.0,  3.0,  3.0,  2.0,  1.0,  1.0},
        {0.5,  0.5,  1.0,  2.5,  2.5,  1.0,  0.5,  0.5},
        {0.0,  0.0,  0.0,  2.0,  2.0,  0.0,  0.0,  0.0},
        {0.5, -0.5, -1.0,  0.0,  0.0, -1.0, -0.5,  0.5},
        {0.5,  1.0, 1.0,  -2.0, -2.0,  1.0,  1.0,  0.5},
        {0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0}
    };

    private double[,] wPawn = new double[8, 8] {
        {0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0},
        {0.5,  1.0, 1.0,  -2.0, -2.0,  1.0,  1.0,  0.5},
        {0.5, -0.5, -1.0,  0.0,  0.0, -1.0, -0.5,  0.5},
        {0.0,  0.0,  0.0,  2.0,  2.0,  0.0,  0.0,  0.0},
        {0.5,  0.5,  1.0,  2.5,  2.5,  1.0,  0.5,  0.5},
        {1.0,  1.0,  2.0,  3.0,  3.0,  2.0,  1.0,  1.0},
        {5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0},
        {0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0}
    };

    private double[,] knight = new double[8, 8] {
        { -5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0},
        { -4.0, -2.0, 0.0, 0.0, 0.0, 0.0, -2.0, -4.0},
        { -3.0, 0.0, 1.0, 1.5, 1.5, 1.0, 0.0, -3.0},
        { -3.0, 0.5, 1.5, 2.0, 2.0, 1.5, 0.5, -3.0},
        { -3.0, 0.0, 1.5, 2.0, 2.0, 1.5, 0.0, -3.0},
        { -3.0, 0.5, 1.0, 1.5, 1.5, 1.0, 0.5, -3.0},
        { -4.0, -2.0, 0.0, 0.5, 0.5, 0.0, -2.0, -4.0},
        { -5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0}
    };



    private double[,] bBishop = new double[8, 8] {
        { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0 },
        { -1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0 },
        { -1.0, 0.0, 0.5, 1.0, 1.0, 0.5, 0.0, -1.0 },
        { -1.0, 0.5, 0.5, 1.0, 1.0, 0.5, 0.5, -1.0 },
        { -1.0, 0.0, 1.0, 1.0, 1.0, 1.0, 0.0, -1.0 },
        { -1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, -1.0 },
        { -1.0, 0.5, 0.0, 0.0, 0.0, 0.0, 0.5, -1.0 },
        { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0 }
    };

    private double[,] wBishop = new double[8, 8] {
        { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0 },
        { -1.0, 0.5, 0.0, 0.0, 0.0, 0.0, 0.5, -1.0 },
        { -1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, -1.0 },
        { -1.0, 0.0, 1.0, 1.0, 1.0, 1.0, 0.0, -1.0 },
        { -1.0, 0.5, 0.5, 1.0, 1.0, 0.5, 0.5, -1.0 },
        { -1.0, 0.0, 0.5, 1.0, 1.0, 0.5, 0.0, -1.0 },
        { -1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0 },
        { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0 }
    };

    private double[,] bRook = new double[8, 8] {
        { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
        { 0.5, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.5 },
        { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 },
        { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 },
        { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 },
        { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 },
        { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 },
        { 0.0, 0.0, 0.0, 0.5, 0.5, 0.0, 0.0, 0.0 }
    };

    private double[,] wRook = new double[8, 8] {
        { 0.0, 0.0, 0.0, 0.5, 0.5, 0.0, 0.0, 0.0 },
        { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 },
        { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 },
        { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 },
        { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 },
        { -0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5 },
        { 0.5, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.5 },
        { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 }
    };

    private double[,] queen = new double[8, 8] {
        { -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0 },
        { -1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0 },
        { -1.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0 },
        { -0.5, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -0.5 },
        { 0.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -0.5 },
        { -1.0, 0.5, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0 },
        { -1.0, 0.0, 0.5, 0.0, 0.0, 0.0, 0.0, -1.0 },
        { -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0 }
    };

    private double[,] bking = new double[8, 8] {
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 },
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 },
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 },
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 },
        { -2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0 },
        { -1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0 },
        { 2.0, 2.0, 0.0, 0.0, 0.0, 0.0, 2.0, 2.0 },
        { 2.0, 3.0, 1.0, 0.0, 0.0, 1.0, 3.0, 2.0 }
    };

    private double[,] wking = new double[8, 8] {
        { 2.0, 3.0, 1.0, 0.0, 0.0, 1.0, 3.0, 2.0 },
        { 2.0, 2.0, 0.0, 0.0, 0.0, 0.0, 2.0, 2.0 },
        { -1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0 },
        { -2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0 },
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 },
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 },
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 },
        { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 }
    };
    public int[] getBestMove(string[,] board, char color, int depth)
    {
        Debug.Log(bking[0, 0]);
        int nRow = board.GetLength(0);
        int nCol = board.Length / nRow;
        double bestMove = -Mathf.Infinity;
        bool changed = false;
        double alpha = -Mathf.Infinity;
        int[] moveToDo = new int[4];
        for (int r = 0; r < nRow; r++)
        {
            for (int c = 0; c < nCol; c++)
            {
                if (board[r, c][0] == color)
                {
                    List<int[]> currMoves = world.moves.posMoveList(color, board[r, c], r, c, board);
                    foreach (int[] pair in currMoves)
                    {
                        int sr = pair[0];
                        int sc = pair[1];
                        SpotBehavior endSpot = GameObject.Find(char.ConvertFromUtf32(sc + 65) + " (" + (sr + 1).ToString() + ")").GetComponent<SpotBehavior>();
                        if (world.win.isGoodMove(color, world.capture.movementCheck(endSpot, board, r, c)) && (world.wRepCount < 2 || world.wPrevBoard != world.capture.movementCheck(endSpot, board, r, c)) && (world.bRepCount < 2 || world.bPrevBoard != world.capture.movementCheck(endSpot, board, r, c)))
                        {
                            //Debug.Log("found black move: " + depth);
                            int[] thisMove = new int[4];
                            thisMove[0] = r;
                            thisMove[1] = c;
                            thisMove[2] = sr;
                            thisMove[3] = sc;
                            char otherColor = color == 'B' ? 'W' : 'B';
                            double moveValue = minimax(depth - 1, world.capture.movementCheck(endSpot, board, r, c), alpha, Mathf.Infinity, false, otherColor, color);
                            print(moveValue);
                            if (moveValue > bestMove)
                            {
                                changed = true;
                                bestMove = moveValue;
                                moveToDo = thisMove;
                                if (bestMove > 100000)
                                {
                                    //Debug.Break();
                                }
                            }
                            else if (moveValue == bestMove && moveValue < 100000)
                            {
                                System.Random rand = new System.Random();
                                int next = rand.Next(0, 10);
                                if (next > 5)
                                {
                                    bestMove = moveValue;
                                    moveToDo = thisMove;
                                }

                            }
                            if (bestMove > alpha)
                            {
                                alpha = bestMove;
                                if (bestMove > 100000)
                                {
                                    //Debug.Break();
                                }
                            }
                        }
                    }
                }

            }
        }
        if(!changed)
        {
            //Debug.Log("something went wrong");
        }
        //Debug.Log(changed);
        Debug.Log("best score: " + bestMove);
        //Debug.Log(moveToDo[0] + " " + moveToDo[1] + " " + moveToDo[2] + " " + moveToDo[3]);
        return moveToDo;
    }

    private double minimax(int depth, string[,] board, double alpha, double beta, bool isMaximisingPlayer, char color, char bestColor)
    {
        char otherColor1 = bestColor == 'B' ? 'W' : 'B';
        if (world.win.isGameOver(bestColor, board) || world.win.isDraw(bestColor, board) || world.win.isDraw(otherColor1, board))
        {
            return -1000000;
        }
        
        if (world.win.isGameOver(otherColor1, board))
        {
            return 1000000;
        }
        if (depth == 0) 
        {
            return getBoardValue(board, bestColor);
        }
        int nRow = board.GetLength(0);
        int nCol = board.Length / nRow;
        //char color = isMaximisingPlayer ? 'B' : 'W';

        if (isMaximisingPlayer)
        {
            bool changed = false;
            double bestMove = -Mathf.Infinity;
            for (int r = 0; r < nRow; r++)
            {
                for (int c = 0; c < nCol; c++)
                {
                    if (board[r, c][0] == color)
                    {
                        List<int[]> currMoves = world.moves.posMoveList(color, board[r, c], r, c, board);
                        foreach (int[] pair in currMoves)
                        {
                            int sr = pair[0];
                            int sc = pair[1];
                            SpotBehavior endSpot = GameObject.Find(char.ConvertFromUtf32(sc + 65) + " (" + (sr + 1).ToString() + ")").GetComponent<SpotBehavior>();
                            if (world.win.isGoodMove(color, world.capture.movementCheck(endSpot, board, r, c)))
                            {
                                //Debug.Log("found black move: " + depth);
                                int[] thisMove = new int[4];
                                thisMove[0] = r;
                                thisMove[1] = c;
                                thisMove[2] = sr;
                                thisMove[3] = sc;
                                char otherColor = color == 'B' ? 'W' : 'B';
                                double moveValue = minimax(depth - 1, world.capture.movementCheck(endSpot, board, r, c), alpha, beta, false, otherColor, bestColor);
                                //print(moveValue);
                                if (moveValue > bestMove)
                                {
                                    changed = true;
                                    bestMove = moveValue;
                                    if (bestMove > 100000)
                                    {
                                        //Debug.Break();
                                    }
                                }
                                else if (moveValue == bestMove)
                                {
                                    System.Random rand = new System.Random();
                                    int next = rand.Next(0, 10);
                                    if (next > 5)
                                    {
                                        bestMove = moveValue;
                                    }

                                }
                                if (bestMove > alpha)
                                {
                                    alpha = bestMove;
                                    if (bestMove > 100000)
                                    {
                                        //Debug.Break();
                                    }
                                }
                                if (beta < alpha)
                                {
                                    if (changed)
                                    {
                                        //Debug.Log("white changed at " + depth);
                                    }
                                    return bestMove;
                                }
                            }
                        }
                    }
                }
            }
            if (changed)
            {
               // Debug.Log("black changed at " + depth);
            }
            return bestMove;
        } else
        {
            //Debug.Log(color);
            double bestMove = Mathf.Infinity;
            bool changed = false;
            for (int r = 0; r < nRow; r++)
            {
                for (int c = 0; c < nCol; c++)
                {
                    if (board[r, c][0] == color)
                    {
                        List<int[]> currMoves = world.moves.posMoveList(color, board[r, c], r, c, board);
                        foreach (int[] pair in currMoves)
                        {
                            int sr = pair[0];
                            int sc = pair[1];
                            SpotBehavior endSpot = GameObject.Find(char.ConvertFromUtf32(sc + 65) + " (" + (sr + 1).ToString() + ")").GetComponent<SpotBehavior>();
                            if (world.win.isGoodMove(color, world.capture.movementCheck(endSpot, board, r, c)))
                            {
                                //Debug.Log("found white move: " + depth);
                                int[] thisMove = new int[4];
                                thisMove[0] = r;
                                thisMove[1] = c;
                                thisMove[2] = sr;
                                thisMove[3] = sc;
                                string[,] tempBoard = world.capture.movementCheck(endSpot, board, r, c);
                                char otherColor = color == 'B' ? 'W' : 'B';
                                double moveValue = minimax(depth - 1, tempBoard, alpha, beta, true, otherColor, bestColor);
                                if (moveValue < bestMove)
                                {
                                    changed = true;
                                    bestMove = moveValue;
                                    if (bestMove < -100000)
                                    {
                                        //Debug.Break();
                                    }
                                }
                                if (bestMove < beta)
                                {
                                    if(bestMove < -100000)
                                    {
                                        //Debug.Break();
                                    }
                                    beta = bestMove;
                                }
                                if (beta < alpha)
                                {
                                    if (changed)
                                    {
                                        //Debug.Log("white changed at " + depth);
                                    }
                                    return bestMove;
                                }
                            }
                        }
                    }
                }
            }
            if (changed)
            {
                //Debug.Log("white changed at " + depth);
            }
            return bestMove;
        }
    }

    private double getBoardValue(string[,] board, char bestColor)
    {
        double value = 0;
        int nRow = board.GetLength(0);
        int nCol = board.Length / nRow;
        for (int r = 0; r < nRow; r++)
        {
            for (int c = 0; c < nCol; c++)
            {
                if(board[r,c].Length > 1)
                {
                    switch (board[r, c][1])
                    {
                        case 'P':
                            value += (board[r, c][0] == 'B') ? 100 + bPawn[r, c] : -(100 + wPawn[r, c]);
                            break;
                        case 'N':
                            value += (board[r, c][0] == 'B') ? 350 + knight[r, c] : -(350 + knight[r, c]);
                            break;
                        case 'B':
                            value += (board[r, c][0] == 'B') ? 350 + bBishop[r, c] : -(350 + wBishop[r, c]);
                            break;
                        case 'R':
                            value += (board[r, c][0] == 'B') ? 525 + bRook[r, c] : -(525 + wRook[r, c]);
                            break;
                        case 'Q':
                            value += (board[r, c][0] == 'B') ? 1000 + queen[r, c] : -(1000 + queen[r, c]);
                            break;
                        case 'K':
                            value += (board[r, c][0] == 'B') ? 10000 + bking[r, c] : -(10000 + wking[r, c]);
                            break;
                    }
                    //switch (board[r, c][1])
                    //{
                    //    case 'P':
                    //        value += (board[r, c][0] == 'B') ? 100 : -100;
                    //        break;
                    //    case 'N':
                    //        value += (board[r, c][0] == 'B') ? 350 : -350;
                    //        break;
                    //    case 'B':
                    //        value += (board[r, c][0] == 'B') ? 350 : -350;
                    //        break;
                    //    case 'R':
                    //        value += (board[r, c][0] == 'B') ? 525 : -525;
                    //        break;
                    //    case 'Q':
                    //        value += (board[r, c][0] == 'B') ? 1000 : -1000;
                    //        break;
                    //    case 'K':
                    //        value += (board[r, c][0] == 'B') ? 10000 : -10000;
                    //        break;
                    //}
                }
            }
        }
        if(bestColor == 'W')
        {
            value *= -1;
        }
        return value;
    }
}
