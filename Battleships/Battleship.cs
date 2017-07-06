using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class Battleship
    {
        Player player1;
        Player player2;

        string[] boatNames = new string[5] { "Aircraft Carrier", "Battleship", "Submarine", "Destroyer", "Patrol Boat" };
        int[] boatLengths = new int[5] { 5, 4, 3, 3, 2 };

        public Battleship(Player _player1, Player _player2)
        {
            player1 = _player1;
            player2 = _player2;
        }

        //place all ships for player
        public void PlaceShips(Player player)
        {
            string[] directions = new string[4] { "UP", "DOWN", "LEFT", "RIGHT" };
            bool error = false;
            bool check = false;

            //For loop to run through all boats in boatNames[]
            for (int i = 0; i < boatNames.Length; i++) 
            {
                player.DrawPlayerBoard();

                //User is asked to enter the first coordinate for the current boat
                string place;
                Console.WriteLine("\n\n" + player.PlayerName + ": Select first coordinate for " + boatNames[i] + " (" + boatLengths[i] + ") (Example: A1 to J10)\n");
                place = Console.ReadLine();
                string direction;
                //Letter is the first letter of the current boat. Used to mark the map
                string letter = " " + boatNames[i].Substring(0, 1) + " ";

                while (!CheckValidMove(place))
                {
                    Console.WriteLine("\nInvalid coordinates entered. Please enter a new coordinate (A1 to J10)\n");
                    place = Console.ReadLine();
                }

                int x = GetXCoord(place);
                int y = GetYCoord(place);

                //While loop to check if that square already contains another boat
                while (player.PlayerBoard[y, x] != " x ")
                {
                    Console.WriteLine("\nOverlapping boat, please enter a new starting coordinate: (A1 to J10)\n");
                    
                    place = Console.ReadLine();

                    while (!CheckValidMove(place))
                    {
                        Console.WriteLine("\nInvalid coordinates entered. Please enter a new coordinate (A1 to J10)\n");
                        place = Console.ReadLine();
                    } 

                    while (place == "")
                    {
                        Console.WriteLine("\nNo coordinate entered. Try again.\n");
                        place = Console.ReadLine();
                    }

                    x = GetXCoord(place);
                    y = GetYCoord(place);
                }
                   
                //do while loop to determine the direction of the boat
                Console.WriteLine("\nPlease enter direction of boat. (UP/DOWN/LEFT/RIGHT)\n");
                do
                {
                    error = false;
                    check = false;
                    direction = Console.ReadLine().ToUpper();

                    //direction must be UP LEFT DOWN or RIGHT
                    while (direction != "UP" && direction != "DOWN" && direction != "LEFT" && direction != "RIGHT")
                    {
                        Console.WriteLine("\nInvalid input. Try again.\n");
                        direction = Console.ReadLine().ToUpper();
                    }
                    
                    //for loop which runs through the length of the current boat selected
                    for (int l = 0; l < boatLengths[i]; l++)
                    {
                        switch (direction)
                        {
                            case "UP":
                                try
                                {
                                    //if statement to check boatlengths in direction only once per choice
                                    if (!check)
                                    {
                                        for (int d = 0; d < boatLengths[i]; d++)
                                        {
                                            if (player.PlayerBoard[y - d, x] != " x ")
                                            {
                                                //checks all boatlengths in the current direction and makes error true if not all tiles are clear
                                                Console.WriteLine("\nBoats cannot be on top of eachother! Pick a new direction.\n");
                                                error = true;
                                                l = boatLengths.Length;
                                                break;
                                            }
                                            check = true;
                                        }
                                    }
                                    else
                                    {
                                        //places the boat tile for y - L (up)
                                        player.GameBoard(y - l, x, letter);
                                        player.GameBoard(y, x, letter);
                                    }
                                    
                                }
                                catch (Exception)
                                {
                                    if (!error)
                                    {
                                        Console.WriteLine("\n" + boatNames[i] + " Fell off the map!\n\nPlease enter a new direction.\n");
                                        error = true;
                                        l = boatLengths.Length;
                                    }
                                }
                                break;
                            case "DOWN":
                                try
                                {
                                    if (!check)
                                    {
                                        for (int d = 0; d < boatLengths[i]; d++)
                                        {
                                            if (player.PlayerBoard[y + d, x] != " x ")
                                            {
                                                Console.WriteLine("\nBoats cannot be on top of eachother! Pick a new direction.\n");
                                                error = true;
                                                l = boatLengths.Length;
                                                break;
                                            }
                                            check = true;
                                        }
                                    }
                                    else
                                    {
                                        player.GameBoard(y + l, x, letter);
                                        player.GameBoard(y, x, letter);
                                    }
                                }
                                catch (Exception)
                                {
                                    if (!error)
                                    {
                                        Console.WriteLine("\n" + boatNames[i] + " Fell off the map!\n\nPlease enter a new direction.\n");
                                        error = true;
                                        l = boatLengths.Length;
                                    }
                                }
                                break;
                            case "LEFT":
                                try
                                {
                                    if (!check)
                                    {
                                        for (int d = 0; d < boatLengths[i]; d++)
                                        {
                                            if (player.PlayerBoard[y, x - d] != " x ")
                                            {
                                                Console.WriteLine("\nBoats cannot be on top of eachother! Pick a new direction.\n");
                                                error = true;
                                                l = boatLengths.Length;
                                                break;
                                            }
                                            check = true;
                                        }
                                    }
                                    else
                                    {
                                        player.GameBoard(y, x - l, letter);
                                        player.GameBoard(y, x, letter);
                                    }
                                }
                                catch (Exception)
                                {
                                    if (!error)
                                    {
                                        Console.WriteLine("\n" + boatNames[i] + " Fell off the map!\n\nPlease enter a new direction.\n");
                                        error = true;
                                        l = boatLengths.Length;
                                    }
                                }
                                break;
                            case "RIGHT":
                                try
                                {
                                    if (!check)
                                    {
                                        for (int d = 0; d < boatLengths[i]; d++)
                                        {
                                            if (player.PlayerBoard[y, x + d] != " x ")
                                            {
                                                Console.WriteLine("\nBoats cannot be on top of eachother! Pick a new direction.\n");
                                                error = true;
                                                l = boatLengths.Length;
                                                break;
                                            }
                                            check = true;
                                        }
                                    }
                                    else
                                    {
                                        player.GameBoard(y, x + l, letter);
                                        player.GameBoard(y, x, letter);
                                    }
                                }
                                catch (Exception)
                                {
                                    if (!error)
                                    {
                                        Console.WriteLine("\n" + boatNames[i] + " Fell off the map!\n\nPlease enter a new direction.\n");
                                        error = true;
                                        l = boatLengths.Length;
                                    }
                                }
                                break;
                            default:
                                Console.WriteLine("Your boat was ravaged by sharks! You lost! (If you see this message please report it to a developer)");
                                break;
                        }
                    }
                } 
                while (error == true);

                Console.Clear();
            }
        }

        //checks a move is valid and whether it's a hit or not
        public bool PlayMove(Player player, String move)
        {
            bool notDouble = false;
            List<string> movesTaken = player.MovesTaken;

            //While loop to check for an invalid move
            while (!CheckValidMove(move))
            {
                if (player.PlayerName == "Robot")
                {
                    move = player.SelectAIMove();
                }
                else
                {
                    Console.WriteLine("\nInvalid coordinates entered. Please enter a new coordinate to shoot: (A1 to J10)\n");
                    move = Console.ReadLine();
                }
            }

            //while loop to check for a double hit. Does not run on the first move
            while (!notDouble && movesTaken.Count != 0)
            {
                foreach (string moveTaken in movesTaken)
                {
                    if (moveTaken == move)
                    {
                        notDouble = false;

                        if (player.PlayerName == "Robot")
                        {
                            move = player.SelectAIMove();
                        }
                        else
                        {
                            Console.WriteLine("\nCoordinate already shot. Please enter a new coordinate to shoot: (A1 to J10)\n");
                            move = Console.ReadLine();

                            while (!CheckValidMove(move))
                            {
                                Console.WriteLine("\nInvalid coordinates entered. Please enter a new coordinate to shoot: (A1 to J10)\n");
                                move = Console.ReadLine();
                            }
                            break;
                        }
                    }
                    else
                    {
                        notDouble = true;
                    }
                }
            }

            player.MovesTaken.Add(move);

            int x = GetXCoord(move);
            int y = GetYCoord(move);

            if (player.TrackBoard[y, x] != " x ")
            {
                string sunkCheck = player.TrackBoard[y, x];

                player.TrackBoard[y, x] = " o ";

                CheckBoats(sunkCheck, player);
                 
                player.HitCount++;
                return true;
            }
            else
            {
                player.TrackBoard[y, x] = " m "; 
                return false;
            }
        }

        //Methods used to return X and Y values usable in a 2d array from user input of (A1 - J10)
        int GetXCoord(string place)
        {
            string xPlace = (place.Substring(1)).ToUpper();
            int x = -1;

            try
            {
                x = int.Parse(xPlace);
            }
            catch (FormatException)
            {
                return -1;
            }
            
            if (x > 10 || x <= 0)
            {
                return -1;
            }
            else
            {
                return x - 1;
            }
        }

        int GetYCoord(string place)
        {
            string yPlace = (place.Substring(0, 1)).ToUpper();

            string[] index = new string[10] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

            for (int q = 0; q < index.Length; q++)
            {
                if (index[q] == yPlace)
                {
                    return q;
                }
            }
            return -1;
        }

        //checks if a given move is valid
        public bool CheckValidMove(string move)
        {
            if (move == "")
            { 
                return false; 
            }

            int x = GetXCoord(move);
            int y = GetYCoord(move);

            if (x == -1 || y == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //gets the boatName from the Letter hit and if there are no letters of that type left on the board, that boat is sunk.
        void CheckBoats(string shipLetter, Player player)
        {
            int equalCount = 0;
            int boatNameIndex = 0;
            string[] index = new string[5] { " A ", " B ", " S ", " D ", " P " };

            for (int q = 0; q < index.Length; q++)
            {
                if (index[q] == shipLetter)
                {
                    boatNameIndex = q;
                }
            }
            
            foreach (string boat in player.TrackBoard)
            {
                if (boat == shipLetter)
                {
                    equalCount++;
                }
            }

            if (equalCount == 0)
            {
                Console.WriteLine("\nYou sunk the enemy " + boatNames[boatNameIndex] + ".");
            }
        }
    }
}
