using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class Menu
    {
        Play play = new Play();

        static void Main()
        {
            Menu menu = new Menu();
            menu.StartMenu();
        }

        void StartMenu()
        {
            int menuOption = 0;
            
            while (menuOption == 0)
            {
                Console.Clear();
                DrawMenu();

                Console.Write("- ");
                if (!int.TryParse(Console.ReadLine(), out menuOption))
                {
                    menuOption = 0;
                }

                switch (menuOption)
                {
                    case 1:
                        play.TwoPlayer(false);
                        menuOption = 0;
                        break;
                    case 2:
                        play.TwoPlayer(true);
                        menuOption = 0;
                        break;
                    case 3:
                        play.OnePlayer();
                        menuOption = 0;
                        break;
                    case 4:
                        play.PlayerStats();
                        menuOption = 0;
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Thanks for playing!");
                        menuOption = -1;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input.\n");
                        break;
                }
            }
        }

        void DrawMenu()
        {
            Console.Clear();

            //Ascii ship by Matthew Bace ("http://ascii.co.uk/art/battleship")
            Console.WriteLine("                                     |__");
            Console.WriteLine("                                     |\\/");
            Console.WriteLine("                                     ---");
            Console.WriteLine("                                     / | [");
            Console.WriteLine("                              !      | |||");
            Console.WriteLine("                            _/|     _/|-++'");
            Console.WriteLine("                        +  +--|    |--|--|_ |-");
            Console.WriteLine("                     { /|__|  |/\\__|  |--- |||__/");
            Console.WriteLine("                    +---------------___[}-_===_.'____                 /\\");
            Console.WriteLine("                ____`-' ||___-{]_| _[}-  |     |_[___\\==--            \\/   _");
            Console.WriteLine(" __..._____--==/___]_|__|_____________________________[___\\==--____,------' .7");
            Console.WriteLine("|                                                                     BB-61/");
            Console.WriteLine(" \\_________________________________________________________________________|\n\n");
            Console.WriteLine("                             WELCOME TO BATTLESHIPS                  \n\n");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1]New Game");
            Console.WriteLine("[2]Load previous game");
            Console.WriteLine("[3]New One Player Game");
            Console.WriteLine("[4]See stats for players");
            Console.WriteLine("[5]Exit\n");
        }
    }
}
