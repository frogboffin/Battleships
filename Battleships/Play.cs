using System;
using System.Collections.Generic;
using System.IO;

namespace Battleships
{
    class Play
    {
        string name1;
        string name2;

        bool playing;

        Player player1;
        Player player2;
        List<Player> players = new List<Player>();

        ReadWrite rw = new ReadWrite();
        Battleship bships;

        public void OnePlayer()
        {
            Console.Clear();
            playing = true;

            do
            {
                Console.Write("\nPlease Enter the name of player 1: ");
                name1 = Console.ReadLine();
            }
            while (name1 == "");

            Console.Clear();

            player1 = new Player(name1);
            player2 = new Player("Robot");
            players.Add(player1);
            players.Add(player2);
            bships = new Battleship(player1, player2);

            bships.PlaceShips(player1);
            player2.AIBoard();

            Console.WriteLine("\nPress any button to continue...");
            Console.ReadKey();
            Console.Clear();

            player1.TrackBoard = player2.PlayerBoard;
            player2.TrackBoard = player1.PlayerBoard;

            //loop for the actual game
            while (playing == true)
            {
                foreach (Player player in players)
                {
                    if (GameOver())
                    {
                        Console.Clear();
                        rw.SaveLog(player1.PlayerName, player2.PlayerName);

                        playing = false;
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        PlayerMove(player);

                        //if player2 has just taken his turn, allow the game to be saved by the players then exit
                        if (player == player2)
                        {
                            Console.WriteLine("\nPress the \"s\" key if you would like to save and quit, else press any key to continue. . . ");
                            ConsoleKeyInfo key = Console.ReadKey(false);
                            if (key.Key == ConsoleKey.S)
                            {
                                rw.SaveGame(player1, player2);
                                Console.WriteLine("\nSaved.\n");
                                Environment.Exit(0);
                            }
                        }

                        Console.Clear();
                        Console.WriteLine("Please pass to the next player.\n\nPress any button to start your turn.");
                        Console.ReadKey();
                    }
                }
            }
        }

        public void TwoPlayer(bool load)
        {
            Console.Clear();
            playing = true;

            //sets the game state to that loaded from the file chosen
            if (load)
            {
                string name = "";

                while (name == "")
                {
                    Console.WriteLine("\nPlease enter the name of the game to load.\n");
                    name = Console.ReadLine();
                }
                while (!rw.LoadTest(name))
                {
                    Console.WriteLine("\nSave file not found or corrupt. Please try again.\n");
                    name = Console.ReadLine();
                }

                rw.LoadGame(name);

                player1 = new Player(rw.Player1Name);
                player2 = new Player(rw.Player2Name);
                players.Add(player1);
                players.Add(player2);
                bships = new Battleship(player1, player2);
                player1.HitCount = rw.Player1Hit;
                player2.HitCount = rw.Player2Hit;
                player1.MovesTaken = rw.Player1Moves;
                player2.MovesTaken = rw.Player2Moves;
                player1.LoadGameBoard(rw.Player2Track);
                player2.LoadGameBoard(rw.Player1Track);

                Console.WriteLine("\nLoaded!");
                Console.ReadKey();
            }
            //allow the players to set up the gameboards if a game is not loaded
            else
            {
                do
                {
                    do
                    {
                        Console.Write("\nPlease Enter the name of player 1: ");
                        name1 = Console.ReadLine();
                    }
                    while (name1 == "");

                    Console.Clear();

                    do
                    {
                        Console.Write("\nPlease Enter the name of player 2: ");
                        name2 = Console.ReadLine();
                    }
                    while (name2 == "");

                    Console.Clear();

                    if (name1 == name2)
                    {
                        Console.WriteLine("\nTwo players cannot use the same name!\n");
                    }
                }
                while (name1 == name2);

                Console.Clear();

                player1 = new Player(name1);
                player2 = new Player(name2);
                players.Add(player1);
                players.Add(player2);
                bships = new Battleship(player1, player2);


                foreach (Player player in players)
                {
                    bships.PlaceShips(player);

                    Console.WriteLine("\nPress any button to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }

                player1.TrackBoard = player2.PlayerBoard;
                player2.TrackBoard = player1.PlayerBoard;
            }

            //loop for the actual game
            while (playing == true)
            {
                foreach (Player player in players)
                {
                    if (GameOver())
                    {
                        Console.Clear();
                        rw.SaveLog(player1.PlayerName, player2.PlayerName);

                        playing = false;
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        PlayerMove(player);

                        //if player2 has just taken his turn, allow the game to be saved by the players then exit
                        if (player == player2)
                        {
                            Console.WriteLine("\nPress the \"s\" key if you would like to save and quit, else press any key to continue. . . ");
                            ConsoleKeyInfo key = Console.ReadKey(false);
                            if (key.Key == ConsoleKey.S)
                            {
                                rw.SaveGame(player1, player2);
                                Console.WriteLine("\nSaved.\n");
                                Environment.Exit(0);
                            }
                        }

                        Console.Clear();
                        Console.WriteLine("Please pass to the next player.\n\nPress any button to start your turn.");
                        Console.ReadKey();
                    }
                }
            }
        }

        public void PlayerStats()
        {
            if (player1 != null)
            {
                foreach (Player player in players)
                {
                    Console.WriteLine("\n" + player.PlayerName + " has " + player.TotalWins + " win(s)! c:");
                    Console.WriteLine(player.PlayerName + " has " + player.TotalLosses + " loss(es)! :c");
                    Console.WriteLine(player.PlayerName + "'s win/loss ratio is: " + player.WinLossRatio);   
                } 
                
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNo player data found\n");
                Console.ReadKey();
            }
            
        }

        //allows the current player's turn to make a move
        private void PlayerMove(Player player)
        {
            string move;

            player.DrawTrackBoard();

            if (player.PlayerName == "Robot")
            {
                move = player.SelectAIMove();

                if (bships.PlayMove(player, move))
                {
                    Console.WriteLine("\nHIT! \n");
                    rw.Log = (player.PlayerName + " " + move + " Hit");
                }
                else
                {
                    Console.WriteLine("\nMISS! \n");
                    rw.Log = (player.PlayerName + " " + move + " Miss");
                }

                if (player.HitCount == 1)
                {
                    Console.WriteLine("Robot has hit " + player.HitCount + " square!\n\nPress any key to continue: ");
                }
                else
                {
                    Console.WriteLine("Robot has hit " + player.HitCount + " squares!\n\nPress any key to continue: ");
                }
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("\n" + player.PlayerName + " please choose your move.");
                move = Console.ReadLine();

                if (bships.PlayMove(player, move))
                {
                    Console.WriteLine("\nHIT! \n");
                    rw.Log = (player.PlayerName + " " + move + " Hit");
                }
                else
                {
                    Console.WriteLine("\nMISS! \n");
                    rw.Log = (player.PlayerName + " " + move + " Miss");
                }

                if (player.HitCount == 1)
                {
                    Console.WriteLine("You've hit " + player.HitCount + " square!\n\nPress any key to continue: ");
                }
                else
                {
                    Console.WriteLine("You've hit " + player.HitCount + " squares!\n\nPress any key to continue: ");
                }
                Console.ReadKey();
                Console.Clear();
            }
        }

        //checks if the game is over and declares the winner and returns accordingly
        private bool GameOver()
        {
            if (player1.HitCount >= 17)
            {
                Console.Clear();
                Console.WriteLine("\nGAME OVER! " + player1.PlayerName + " Won!\n");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

                rw.Log = (player1.PlayerName + " Won!");

                player1.TotalWins++;
                player2.TotalLosses++;

                return true;
            }
            else if (player2.HitCount >= 17)
            {
                Console.Clear();
                Console.WriteLine("\nGAME OVER! " + player2.PlayerName + " Won!\n");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

                rw.Log = (player1.PlayerName + " Won!");

                player2.TotalWins++;
                player1.TotalLosses++;

                return true;
            }
            else
            {
                return false;
            }
        }
        
        //resets both playerboards and clears the list of movesTaken
        public void NewGame()
        {
            foreach (Player player in players)
            {
                player.CreateGameBoard();
                player.MovesTaken.Clear();
            }
            bships = null; 
            players.Clear();
            player1 = null;
            player2 = null;
        }
    }
}
