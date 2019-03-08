using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Ships;
using BattleShip.BLL.Responses;
using BattleShip.BLL.GameLogic;



namespace BattleShip.UI
{
    class UserIO
    {
        //method: splashscreen
        public static void SplashScreen()
        {
            Console.WriteLine("    ==========================");
            Console.WriteLine("    Welcome to BATTLESHIP !!!!");
            Console.WriteLine("    ==========================");
            Console.WriteLine("\n\n    Press any key to begin game.");
            Console.ReadLine();
            Console.Clear();
        }

        public static void GameSetup(Player player1, Player player2, Board board1, Board board2)
        {
            GetPlayerName(player1);
            ShipPlacementIO.PlaceAllShips(board1);
            GetPlayerName(player2);
            ShipPlacementIO.PlaceAllShips(board2);
        }

        //method: set up player
        public static void GetPlayerName(Player currentPlayer)
        {
            //get name
            currentPlayer.Name = GetStringFromUser("Please enter your name: ");
            Console.Clear();
        }

        //get coords (ship placement and shots)
        public static Coordinate GetCoordinates()
        {
            while (true)
            {
                
                string coords = GetStringFromUser("Please enter coordinates (A1 format): ");
                //set up validation of coords entry
                coords = coords.ToUpper();
                string xStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                char xChar = coords[0];
                int xCoord = 0;
                int yCoord = 0;
                
                if (char.IsLetter(xChar) == true)
                {
                    xCoord = xStr.IndexOf(coords.Substring(0, 1)) + 1;
                    if (xCoord > 10)
                    {
                        continue;
                    }
                }

                if (int.TryParse(coords.Substring(1),out yCoord))
                {
                    if (yCoord < 1 || yCoord > 10)
                    {
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid entry.");
                    continue;
                }
                
                Coordinate xy = new Coordinate(xCoord, yCoord);
                return xy;
            }
        }

        // set up a loop to determine results
        // set up another method which reads it and displays as needed

        public static string GetStringFromUser(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string result = Console.ReadLine();
                if (result == "" || result == string.Empty)
                {
                    Console.WriteLine("You entered blank, please try again");
                    continue;
                }
                if (result.ToUpper() == "Q")
                {
                    Environment.Exit(0);
                }
                return result;
            }
        }
        
        public static bool GoesFirst(Player player1,Player player2)
        {
            Random first = new Random();
            if (first.Next(1, 2) == 1)
            {
                Console.WriteLine($"{player1.Name} goes first!");
                return true;
            }
            else
            {
                Console.WriteLine($"{player2.Name} goes first!");
                return false;
            }
        }

        public static void DisplayBoard(Board board)
        {
            string xStr = "ABCDEFGHIJ";
            char row = 'Z';
            ShotHistory displayShot = new ShotHistory();

            Console.Write("   [1  2  3  4  5  6  7  8  9 10]");
            for (int x = 0; x < 10; x++)
            {
                row = xStr[x];
                Console.Write($"\n[{row}]");
                for (int y = 0; y < 10; y++)
                {
                    Coordinate coord = new Coordinate(x+1,y+1);
                    displayShot = board.CheckCoordinate(coord);
                    if (displayShot == ShotHistory.Unknown)
                    {
                        Console.Write($"[ ]");
                    }
                    if (displayShot == ShotHistory.Hit)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[H]");
                    }
                    if (displayShot == ShotHistory.Miss)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("[M]");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.WriteLine("");
        }
    }
}
