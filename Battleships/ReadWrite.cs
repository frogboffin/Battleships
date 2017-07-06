using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Battleships
{
    class ReadWrite
    {
        string log;

        string player1Name;
        string player2Name;

        string player1Track;
        string player2Track;

        int player1Hit;
        int player2Hit;

        List<string> player1Moves = new List<string>();
        List<string> player2Moves = new List<string>();

        public string Log
        {
            set
            {
                log += (value + ".");
            }
        }

        public string Player1Name
        {
            get
            {
                return player1Name;
            }
        }

        public string Player2Name
        {
            get
            {
                return player2Name;
            }
        }

        public string Player1Track
        {
            get
            {
                return player1Track;
            }
        }
        
        public string Player2Track
        {
            get
            {
                return player2Track;
            }
        }

        public int Player1Hit
        {
            get
            {
                return player1Hit;
            }
        }

        public int Player2Hit
        {
            get
            {
                return player2Hit;
            }
        }

        public List<string> Player1Moves
        {
            get
            {
                return player1Moves;
            }
        }

        public List<string> Player2Moves
        {
            get
            {
                return player2Moves;
            }
        }

        //saves all necessary data to a text file of given name
        public void SaveGame(Player player1, Player player2)
        {        
            string saveName = "";

            while (saveName == "")
            {
                Console.WriteLine("\nPlease enter a name for the Save File.\n");
                saveName = Console.ReadLine();
            }

            using (StreamWriter writer = new StreamWriter(saveName + ".txt", false))
            {
                writer.WriteLine(player1.PlayerName + " vs. " + player2.PlayerName);

                writer.WriteLine("PlayerBoards");
                writer.WriteLine(player1.SavePlayerBoard());
                writer.WriteLine(player2.SavePlayerBoard());

                writer.WriteLine("HitCounts");
                writer.WriteLine(player1.HitCount);
                writer.WriteLine(player2.HitCount);

                writer.WriteLine("MovesTaken");
                foreach (string moveTaken in player1.MovesTaken)
                {
                    writer.Write(moveTaken + " ");
                }

                writer.WriteLine(" ");

                foreach (string moveTaken in player2.MovesTaken)
                {
                    writer.Write(moveTaken + " ");
                }

                writer.WriteLine(" ");

                writer.WriteLine(player1.PlayerName);
                writer.WriteLine(player2.PlayerName);

                writer.WriteLine(log);
            }
        }

        //tests whether loading a file works
        public bool LoadTest(string name)
        {
            try
            {
                using (StreamReader reader = new StreamReader(name + ".txt"))
                {
                    reader.ReadLine();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        //loads all necessary data from a text file and saves the data to variables in this Class
        public void LoadGame(string name)
        {
            try
            {
                using (StreamReader reader = new StreamReader(name + ".txt"))
                {
                    string moves;
                    
                    for (int i = 0; i < 13; i++)
                    {
                        switch (i)
                        {
                            case 2:
                                player1Track = reader.ReadLine();
                                break;
                            case 3:
                                player2Track = reader.ReadLine();
                                break;
                            case 5:
                                player1Hit = int.Parse(reader.ReadLine());
                                break;
                            case 6:
                                player2Hit = int.Parse(reader.ReadLine());
                                break;
                            case 8:
                                moves = reader.ReadLine();
                                foreach (string move in moves.Split(new char[0]))
                                {
                                    player1Moves.Add(move);
                                }
                                break;
                            case 9:
                                moves = reader.ReadLine();
                                foreach (string move in moves.Split(new char[0]))
                                {
                                    player2Moves.Add(move);
                                }
                                break;
                            case 10:
                                player1Name = reader.ReadLine();
                                break;
                            case 11:
                                player2Name = reader.ReadLine();
                                break;
                            case 12:
                                log = reader.ReadLine();
                                break;
                            default:
                                reader.ReadLine();
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: " + e + "\nPlease reload and retry loading");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        public void SaveLog(string name1, string name2)
        {
            using (StreamWriter writer = new StreamWriter("Log_" + name1 + name2 + ".txt", false))
            {
                string[] _log = log.Split('.');

                foreach (string line in _log)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}
