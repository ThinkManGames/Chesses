using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client
{
    private bool isGameOver = false;
    public Socket sock;
    public string name;
    public NetworkManager manager;
    private bool isSocketClosed = false;

    public bool couldConnect;
    public Client(string _name, string _serverName)
    {
        try
        {


            IPHostEntry IPHost = Dns.GetHostEntry(_serverName);
            IPAddress[] addr = IPHost.AddressList;
            EndPoint ep = new IPEndPoint(addr[0], 1620);
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(ep);
            couldConnect = true;
            sock.Blocking = false;
            if (sock.Connected)
            {
                Console.WriteLine("OK");
            }
            name = _name;
        }
        catch
        {
            couldConnect = false;
        }
    }

    public string ReadData()
    {
        if (!isSocketClosed)
        {
            Byte[] oneByte = new byte[1];
            List<byte> allMessageBytes = new List<byte>();
            Byte prev = 0;
            while (true)
            {
                if (sock.Receive(oneByte, 1, 0) == 0)
                {

                    string[] message = new string[2] { "DISCONNECTED", "FORCEFUL" };
                    if(isGameOver)
                    {
                        message[1] = "GRACEFUL";
                    }
                    OpponentLeft(message);
                    return "";
                }

                if (oneByte[0] == '\n')
                {
                    if (prev == '\n')
                    {
                        break;
                    }
                    allMessageBytes.Add(oneByte[0]);
                }
                else
                {
                    allMessageBytes.Add(oneByte[0]);
                }
                prev = oneByte[0];

            }
            string content = Encoding.ASCII.GetString(allMessageBytes.ToArray());
            Console.WriteLine(content);
            string[] lines = content.Split('\n');
            switch (lines[0])
            {
                case "GAMELIST":
                    GotGameList(lines);
                    break;
                case "INVALIDGAME":
                    GotInvalidGame(lines);
                    break;
                case "UPDATEBOARD":
                    GotNewBoard(lines);
                    break;
                case "MYTURN":
                    MyTurn(lines);
                    break;
                case "STARTGAME":
                    StartGame(lines);
                    break;
                case "GAMEOVER":
                    EndGame(lines);
                    break;
                case "DISCONNECT":
                    OpponentLeft(lines);
                    break;
                case "UPDATESQUARES":
                    UpdateMySquares(lines);
                    break;
                case "LOSTPIECES":
                    UpdateLostPieces(lines);
                    break;
            }
            return content;
        }
        return "";
    }

    public void OpponentLeft(string[] lines)
    {
        if(lines[1] == "GRACEFUL")
        {
            manager.NoRematch();
        }
        if(lines[1] == "FORCEFUL")
        {
            manager.CancelGame();
        }
        if(!isSocketClosed)
        {
            sock.Close();
            isSocketClosed = true;
        }
    }

    public void GotGameList(string[] lines)
    {
        Debug.Log(lines.Length);
        for(int i = 1; i < lines.Length - 1;)
        {
            manager.AddGameOption(lines[i], lines[i + 1], lines[i + 2]);
            i += 3;
        }
    }

    public void GotInvalidGame(string[] lines)
    {
        manager.InvalidGame();
    }

    public void GotNewBoard(string[] lines)
    {
        Debug.Log("got new board");
        int cols = lines[1].Split(' ').Length - 1;
        int rows = lines.Length - 2;
        Debug.Log("cols: " + cols.ToString() + " rows: " + rows.ToString());
        string[,] boardToSend = new string[rows,cols];
        for(int i = 1; i < rows + 1; i++)
        {
            Debug.Log(lines[i]);
            string[] currRow = lines[i].Split(' ');
            Debug.Log(currRow.Length);
            for(int j = 0; j < cols; j++)
            {
                boardToSend[i - 1, j] = currRow[j];
            }
        }
        manager.GotNewBoard(boardToSend);
    }

    public void MyTurn(string[] lines)
    {
        manager.StartTurn();
    }

    public void Disconnect()
    {
        if(!isSocketClosed)
        {
            string message = "DISCONNECT\n";
            if (isGameOver)
            {
                message += "GRACEFUL\n\n";
            }
            else
            {
                message += "FORCEFUL\n\n";
            }
            Encoding ASCII = Encoding.ASCII;
            Byte[] BytesToSend = ASCII.GetBytes(message);
            sock.Send(BytesToSend, BytesToSend.Length, 0);
            sock.Close();
            isSocketClosed = true;
        }
    }

    public void StartGame(string[] lines)
    {
        manager.StartGame(lines[1], lines[2], lines[3], lines[4]);
    }

    public void EndGame(string[] lines)
    {
        isGameOver = true;
        manager.EndGame(lines[1]);
    }

    public void SendBoard(string[,] board)
    {
        string message = "UPDATEBOARD\n";
        int tRow = board.GetLength(0);
        int tCol = board.Length / board.GetLength(0);
        for (int i = 0; i < tRow; i++)
        {
            for (int j = 0; j < tCol; j++)
            {
                message += board[i, j] + " ";
            }
            message.Remove(message.Length - 1, 1);
            message += "\n";
        }
        message += "\n";
        Encoding ASCII = Encoding.ASCII;
        Byte[] BytesToSend = ASCII.GetBytes(message);
        sock.Send(BytesToSend, BytesToSend.Length, 0);
    }

    public void SendJoin(string gameID)
    {
        string message = "JOIN\n" + name + "\n" + gameID + "\n\n";
        Encoding ASCII = Encoding.ASCII;
        Byte[] BytesToSend = ASCII.GetBytes(message);
        sock.Send(BytesToSend, BytesToSend.Length, 0);
    }

    public void SendHost(string gameType)
    {
        string message = "HOST\n" + name + "\n" + gameType + "\n\n";
        Encoding ASCII = Encoding.ASCII;
        Byte[] BytesToSend = ASCII.GetBytes(message);
        sock.Send(BytesToSend, BytesToSend.Length, 0);
    }

    public void MyTurnEnded()
    {
        string message = "TURNEND\n\n";
        Encoding ASCII = Encoding.ASCII;
        Byte[] BytesToSend = ASCII.GetBytes(message);
        sock.Send(BytesToSend, BytesToSend.Length, 0);
    }

    public void SendGameOver(string condition)
    {
        isGameOver = true;
        string message = "GAMEOVER\n" + condition +"\n\n";
        Encoding ASCII = Encoding.ASCII;
        Byte[] BytesToSend = ASCII.GetBytes(message);
        sock.Send(BytesToSend, BytesToSend.Length, 0);
    }

    public void GetGameList()
    {
        string message = "GAMELIST\n\n";
        Encoding ASCII = Encoding.ASCII;
        Byte[] BytesToSend = ASCII.GetBytes(message);
        sock.Send(BytesToSend, BytesToSend.Length, 0);
    }

    public void StopHosting()
    {
        string message = "STOPHOST\n\n";
        Encoding ASCII = Encoding.ASCII;
        Byte[] BytesToSend = ASCII.GetBytes(message);
        sock.Send(BytesToSend, BytesToSend.Length, 0);
    }
    
    public void UpdateOpponentsSquares(string[] squares)
    {
        string message = "UPDATESQUARES\n";
        message += squares[0] + "\n" + squares[1] + "\n" + squares[2] + "\n\n";
        Encoding ASCII = Encoding.ASCII;
        Byte[] BytesToSend = ASCII.GetBytes(message);
        sock.Send(BytesToSend, BytesToSend.Length, 0);
    }

    public void UpdateMySquares(string[] squares)
    {
        manager.UpdateMySquares(squares);
    }

    public void SendLostPieces(string lostPieces)
    {
        string message = "LOSTPIECES\n";
        for(int i = 0; i < lostPieces.Length; i++)
        {
            message += lostPieces[i] + "\n";
        }
        message += "\n";
        Encoding ASCII = Encoding.ASCII;
        Byte[] BytesToSend = ASCII.GetBytes(message);
        sock.Send(BytesToSend, BytesToSend.Length, 0);
    }

    public void UpdateLostPieces(string[] lines)
    {
        manager.UpdateMyDeaths(lines);
    }
}

