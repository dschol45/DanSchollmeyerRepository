using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    public class Turn
    {
        public static bool TurnMain(Player player1, Player player2,Board board1, Board board2)
        {
            bool goesNext= UserIO.GoesFirst(player1, player2);
            Player currentPlayer = new Player();
            Board currentBoard = new Board();
            ShotStatus shotStatus;
            while (true) //set condition to victory == false?
            {
                if (goesNext == true)
                {
                    currentPlayer = player1;
                    currentBoard = board2;
                    goesNext = false;
                }
                else
                {
                    currentPlayer = player2;
                    currentBoard = board1;
                    goesNext = true;
                }
                
                
                //start current player's turn
                shotStatus = PlayerTurn(currentPlayer,currentBoard);
                //\\check for victory?
                if (shotStatus == ShotStatus.Victory)
                {
                    return true;
                }

                Console.WriteLine($"Press any key to end {currentPlayer.Name}'s turn");
                Console.ReadKey(true);
                Console.Clear();
            }
        }
        // method for turn, use player as input
        public static ShotStatus PlayerTurn(Player player, Board board)
        {
            Console.WriteLine($"Press any key to begin {player.Name}'s turn");
            Console.ReadKey(true);
            
            //display opponent's board
            UserIO.DisplayBoard(board);
            FireShotResponse response;
            do
            {
                //get coord for shot
                Coordinate coord = UserIO.GetCoordinates();

                //check result
                response = board.FireShot(coord);
                DisplayShotStatus(player, response);
                
            } while (response.ShotStatus == ShotStatus.Invalid || response.ShotStatus == ShotStatus.Duplicate);

            return response.ShotStatus;
        }

        public static void DisplayShotStatus(Player player, FireShotResponse response)
        {
            //display result
            switch (response.ShotStatus)
            {
                case ShotStatus.Invalid:
                    Console.WriteLine($"\n{response.ShotStatus}.");
                    break;
                case ShotStatus.Duplicate:
                    Console.WriteLine($"\n{response.ShotStatus}.");
                    break;
                case ShotStatus.Miss:
                    Console.WriteLine($"\n{response.ShotStatus}.");
                    break;
                case ShotStatus.Hit:
                    Console.WriteLine($"\n{response.ShotStatus}!");
                    break;
                // need to add which ship sunk
                case ShotStatus.HitAndSunk:
                    Console.WriteLine($"\n{response.ShotStatus}!!");
                    break;
                //check for victory and play again / exit
                case ShotStatus.Victory:
                    Console.WriteLine($"\n{response.ShotStatus}!!!!\n{player.Name} has Won!!!!");
                    break;
            }
        }
    public static bool PlayAgain()
        {
            do
            {
                string again = UserIO.GetStringFromUser("Would you like to start a new game?\n(Y)es / (N)o:");
                again = again.ToUpper();
                if (char.IsLetter(again[0]) == true)
                {
                    if (again.StartsWith("Y") == true)
                    {
                        return true;
                    }
                    if (again.StartsWith("N") == true)
                    {
                        return false;
                    }
                }
            } while (true);
        }
    }
}
