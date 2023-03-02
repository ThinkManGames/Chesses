using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
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

    public TheWorld world = null;


    public virtual bool isGoodMove(char color, string[,] board)
    {
        Vector2 rowCol = findKingSpot(color, board);
        int row = (int)rowCol.x;
        int col = (int)rowCol.y;
        if(row == -1)
        {
            return false;
        }
        bool isSafe = getDangerousSpots(color, row, col, board) == "";
        return isSafe;
    }
    public virtual Vector2 findKingSpot(char color, string[,] board)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        for (int row = 0; row < tRow; row++)
        {
            for (int col = 0; col < tCol; col++)
            {
                if (board[row, col][0] == color && board[row, col][1] == 'K')
                {
                    return new Vector2(row, col);
                }
            }
        }
        //Debug.Log("NO KING ON BOARD, OH GOD!");
        // should never happen because there should always be a king on the board
        return new Vector2(-1, -1);
    }

    public virtual string getDangerousSpots(char color, int _row, int _col, string[,] board)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        string[,] toReturn = new string[tRow, tCol];
        for (int i = 0; i < tRow; i++)
        {
            for (int j = 0; j < tCol; j++)
            {
                toReturn[i, j] = "";
            }
        }

        string rowCols = "";
        char otherColor = color == 'B' ? 'W' : 'B';
        int[,] knightMoves = world.moves.possibleMoves(color, "TN1", _row, _col, board);
        int[,] rookMoves = world.moves.possibleMoves(color, "TR1", _row, _col, board);
        int[,] bishopMoves = world.moves.possibleMoves(color, "TB1", _row, _col, board);
        int[,] kingMoves = world.moves.possibleMoves(color, "TK1", _row, _col, board);
        for (int row = 0; row < tRow; row++)
        {
            for (int col = 0; col < tCol; col++)
            {
                if (knightMoves[row, col] == 1)
                {
                    toReturn[row, col] += "N";
                }
                if (rookMoves[row, col] == 1)
                {
                    toReturn[row, col] += "RQ";
                }
                if (bishopMoves[row, col] == 1)
                {
                    if (_row - row == 1 && color == 'B')
                    {
                        toReturn[row, col] += "P";
                    }
                    else if (_row - row == -1 && color == 'W')
                    {
                        toReturn[row, col] += "P";
                    }
                    if (!toReturn[row, col].Contains("Q"))
                    {
                        toReturn[row, col] += "BQ";
                    }
                    else
                    {
                        toReturn[row, col] += "B";
                    }
                }
                if (kingMoves[row,col] == 1)
                {
                    toReturn[row, col] += "K";
                }
                if (board[row, col][0] == otherColor && toReturn[row, col].Contains(board[row, col][1].ToString()))
                {
                    rowCols += row.ToString() + col.ToString();
                }
            }
        }
        return rowCols;
    }

    public virtual bool isGameOver(char color, string[,] board)
    {
        //Debug.Log("checking if Checkmate");
        Vector2 rowCol = findKingSpot(color, board);
        int row = (int)rowCol.x;
        int col = (int)rowCol.y;
        if (getDangerousSpots(color, row, col, board) != "")
        {
            world.showDanger = char.ConvertFromUtf32(col + 65) + " (" + (row + 1).ToString() + ")" + "S";
            //Debug.Log("king in check");
            if (kingCantMove(color, row, col, board))
            {
                //Debug.Log("king cant move");
                if (attackingCantDie(color, row, col, board))
                {
                    //Debug.Log("attacking cant die");
                    if (noneCanBlock(color, row, col, board))
                    {
                        //Debug.Log("is Checkmate");
                        return true;
                    }
                }
            }
        }
        else
        {
            world.showDanger = null;
        }
        //Debug.Log("is not checkmate");
        return false;
    }

    public virtual bool isDraw(char color, string[,] board)
    {
        Vector2 rowCol = findKingSpot(color, board);
        int Krow = (int)rowCol.x;
        int Kcol = (int)rowCol.y;
        if (getDangerousSpots(color, Krow, Kcol, board) != "")
        {
            return false;
        }
            
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        for(int r = 0; r < tRow; r++)
        {
            for(int c = 0; c < tCol; c++)
            {
                if(board[r,c][0] == color) // found a piece
                {
                    int[,] checkForMoves = world.moves.possibleMoves(color, board[r, c], r, c, board);
                    int[,] baseline = new int[tRow, tCol];
                    if(checkForMoves != baseline)
                    {
                        for (int row = 0; row < tRow; row++)
                        {
                            for (int col = 0; col < tCol; col++)
                            {
                                if (checkForMoves[row, col] == 1) // found a possible move
                                {
                                    string spotName = char.ConvertFromUtf32(col + 65) + " (" + (row + 1).ToString() + ")";
                                    SpotBehavior tempSpot = GameObject.Find(spotName).GetComponent<SpotBehavior>();
                                    string[,] tempBoard = world.capture.movementCheck(tempSpot, board, r, c);
                                    if (isGoodMove(color, tempBoard))
                                    {
                                        return false;
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
        return true;
    }

    protected virtual bool kingCantMove(char color, int row, int col, string[,] board)
    {
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        for (int i = 0; i < 8; i++)
        {
            int rowAdder = QueenKingMoves[i, 0];
            int colAdder = QueenKingMoves[i, 1];
            int currRow = row + rowAdder;
            int currCol = col + colAdder;
            if (currRow >= 0 && currRow < tRow && currCol >= 0 && currCol < tCol && board[currRow, currCol][0] != color && board[currRow,currCol][0] != '0')
            {
                string spotName = char.ConvertFromUtf32(currCol + 65) + " (" + (currRow + 1).ToString() + ")";
                SpotBehavior spot = GameObject.Find(spotName).GetComponent<SpotBehavior>();
                string[,] tempBoard = world.capture.movementCheck(spot, board, row, col);
                // means there is a piece the king can take that might be a safe spot
                if (getDangerousSpots(color, currRow, currCol, tempBoard) == "" && findKingSpot('B', tempBoard) != new Vector2(-1,-1) && findKingSpot('W', tempBoard) != new Vector2(-1, -1))
                {
                    return false;
                }
            }
        }
        return true;
    }

    protected virtual bool attackingCantDie(char color, int row, int col, string[,] board)
    {
        // row col is a list of all the pieces that can attack the king organized by char i is the row and char i+1 is the col
        string rowCol = getDangerousSpots(color, row, col, board);
        //if more than once piece is attacking the king, and the king cant move, even if you take one, the other still attacks you. you lose
        if (rowCol.Length > 2)
        {
            return true;
        }
        int rowc = rowCol[0];
        rowc -= '0';
        int colc = rowCol[1];
        colc -= '0';
        //find if there are any pieces that can take the piece attacking the king
        string canAttackAttacking = getDangerousSpots(board[rowc, colc][0], rowc, colc, board);
        if (canAttackAttacking != "")
        {
            //if there is a piece that can attack thee piece attacking the king, check to see if moving it puts the king in check a different way. if not, then return false
            for (int i = 0; i < canAttackAttacking.Length; i += 2)
            {
                int canAttackrow = canAttackAttacking[i] - '0';
                int canAttackcol = canAttackAttacking[i + 1] - '0';
                if(board[canAttackrow,canAttackcol][1] == 'K')
                {
                    continue;
                }
                string spotName = char.ConvertFromUtf32(colc + 65) + " (" + (rowc + 1).ToString() + ")";
                SpotBehavior spot = GameObject.Find(spotName).GetComponent<SpotBehavior>();
                string[,] tempBoard = world.capture.movementCheck(spot, board, canAttackrow,canAttackcol);
                if (getDangerousSpots(color, row, col, tempBoard) == "" && findKingSpot('B', tempBoard) != new Vector2(-1, -1) && findKingSpot('W', tempBoard) != new Vector2(-1, -1))
                {
                    return false;
                }
            }
        }
        // if no pieces can take the attacking piece
        return true;
    }

    protected virtual bool noneCanBlock(char color, int row, int col, string[,] board)
    {
        string rowCol = getDangerousSpots(color, row, col, board);
        // if more than once piece is attacking, blocking wont do anything
        if (rowCol.Length > 2)
        {
            return true;
        }

        int attackingRow = rowCol[0];
        attackingRow -= '0';
        int attackignCol = rowCol[1];
        attackignCol -= '0';
        if (board[attackingRow, attackignCol][1] == 'N')
        {
            //if a horse is attacking, nothing can block it, so youre screwed
            return true;
        }
        else if (board[attackingRow, attackignCol][1] == 'P')
        {
            return true;
        }
        int[,] dangerousSpots = world.getPossibleMoves(board[attackingRow, attackignCol][0], board[attackingRow, attackignCol], attackingRow, attackignCol);
        
        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                if (dangerousSpots[r, c] == 1 && board[r, c] == "E")
                {
                    char otherColor = color == 'W' ? 'B' : 'W';
                    // there is a spot that this dangerous piece can go to
                    string piecesThatCanBlock = getDangerousSpots(otherColor, r, c, board);
                    for (int i = 0; i < piecesThatCanBlock.Length; i += 2)
                    {
                        int blockingPieceRow = piecesThatCanBlock[i];
                        int blockingPieceCol = piecesThatCanBlock[i + 1];
                        blockingPieceCol -= '0';
                        blockingPieceRow -= '0';
                        if(board[blockingPieceRow,blockingPieceCol][1] == 'K')
                        {
                            continue;
                        }
                        string spotName = char.ConvertFromUtf32(c + 65) + " (" + (r + 1).ToString() + ")";
                        SpotBehavior spot = GameObject.Find(spotName).GetComponent<SpotBehavior>();
                        string[,] tempBoard = world.capture.movementCheck(spot, board, blockingPieceRow,blockingPieceCol);
                        if (getDangerousSpots(color, row, col, tempBoard) == "" && findKingSpot('B', tempBoard) != new Vector2(-1, -1) && findKingSpot('W', tempBoard) != new Vector2(-1, -1))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
}
