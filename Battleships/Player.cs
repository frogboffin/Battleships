using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class Player
    {
        const int boardSize = 10;

        string[,] playerBoard;
        string[,] trackingBoard;
        string[] index = new string[10] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

        string playerName;

        int totalWins = 0;
        int totalLosses = 0;
        int hitCount = 0;

        List<string> movesTaken = new List<string>();

        public Player(string name)
        {
            playerName = name;
            CreateGameBoard();
        }

        public List<string> MovesTaken
        {
            get
            {
                return movesTaken;
            }
            set
            {
                movesTaken = value;
            }
        }

        public string PlayerName
        {
            get
            {
                return playerName;
            }
        }

        public int TotalWins
        {
            get
            {
                return totalWins;
            }
            set
            {
                totalWins = value;
            }
        }

        public int TotalLosses
        {
            get
            {
                return totalLosses;
            }
            set
            {
                totalLosses = value;
            }
        }

        public double WinLossRatio
        {
            get
            {
                if (totalLosses == 0)
                {
                    return totalWins;
                }
                else
                {
                    return (totalWins / totalLosses);
                }
            }
        }

        public int HitCount
        {
            get
            {
                return hitCount;
            }
            set 
            {
                hitCount = value;
            }
        }

        public string[,] PlayerBoard
        {
            get
            {
                return playerBoard;
            }
            set
            {
                playerBoard = value;
            }
        }

        public string[,] TrackBoard
        {
            get
            {
                return trackingBoard;
            }
            set
            {
                trackingBoard = value;
            }
        }

        public void GameBoard(int y, int x, string letter)
        {
            playerBoard[y, x] = letter;
        }

        public void CreateGameBoard()
        {
            playerBoard = new string[boardSize, boardSize];

            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    playerBoard[x, y] = " x ";
                }
            }
        }

        public void DrawPlayerBoard()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("  ¦  1  2  3  4  5  6  7  8  9  10");
            Console.WriteLine("--+-------------------------------"); 
            Console.ResetColor();
            for (int x = 0; x < boardSize; x++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write((index[x]).ToString() + " ¦ ");
                Console.ResetColor();
                for (int y = 0; y < boardSize; y++)
                {
                    if (playerBoard[x, y] != " x ")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    Console.Write(playerBoard[x, y]);

                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        public void DrawTrackBoard()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  ¦  1  2  3  4  5  6  7  8  9  10");
            Console.WriteLine("--+-------------------------------");
            Console.ResetColor();
            for (int x = 0; x < boardSize; x++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write((index[x]).ToString() + " ¦ ");
                Console.ResetColor();
                for (int y = 0; y < boardSize; y++)
                {
                    if (trackingBoard[x, y] == " o ")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    if (trackingBoard[x, y] == " o " || trackingBoard[x, y] == " m ")
                    {
                        Console.Write(trackingBoard[x, y]);
                    }
                    else
                    {
                        Console.Write(" x ");
                    }

                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        public void LoadGameBoard(string grid)
        {
            trackingBoard = new string[boardSize, boardSize];
            int index = 0;

            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    trackingBoard[x, y] = (" " + grid.Substring(index, 1) + " ");
                    index++;
                }
            }
        }

        public string SavePlayerBoard()
        {
            string board = "";

            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    board = board + playerBoard[x, y].Trim();
                }
            }

            return board;
        }

        public string SelectAIMove()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            string x = index[rnd.Next(0, 10)];
            string y = rnd.Next(1, 11).ToString();
            return x + y;
        }

        public void AIBoard()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            string[] board = new string[] { "ABSDPxxxxxABSDPxxxxxABSDxxxxxxABxxxxxxxxAxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "ABSDPxxxxxABSDPxxxxxABSDxxxxxxABxxxxxxxxAxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xAAAAABBBBxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxPPxxxxxxxxxxxxxxxxxxxxxxDxxxSxxxxxDxxxSxxxxxDxxxSxxxxxxxx", "xPPxxxxxxxxxxxxxxxxxxxxBBBBxxDxAxxxxxxxDxAxxxxxxxDxAxxxxxxxxxAxxxxxxxxxAxxxxxxxxxxxxxSSSxxxxxxxxxxxx"};
            
            int index = 0;
            string grid = board[rnd.Next(0, board.Length)];
            
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    playerBoard[x, y] = (" " + grid.Substring(index, 1) + " ");
                    index++;
                }
            }
            
        }
    }
}
