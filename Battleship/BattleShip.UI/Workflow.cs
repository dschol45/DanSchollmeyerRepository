using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;

namespace BattleShip.UI
{
    public class Workflow
    {
        public void Run()
        {
            Player player1;
            Player player2;
            Board board1;
            Board board2;
            do
            {
                //splash screen mehtod
                UserIO.SplashScreen();

                //dec
                player1 = new Player();
                player2 = new Player();
                board1 = new Board();
                board2 = new Board();

                //players will take turns entering name and placing ships
                UserIO.GameSetup(player1, player2, board1, board2);
                
            } while (Turn.TurnMain(player1, player2, board1, board2) && Turn.PlayAgain() == true);

            Console.WriteLine("Press any key to exit game.");
            Console.ReadKey();
            Environment.Exit(0);
            //display board2 for player1

            //get player1 coords for shot at board2
            //determine result of shot
            //clear
            //prompt and readline to start player 2 turn
            //display board1 for player2
            //get player2 coords for shot at board1
            //determine result of shot
            //clear
            //loop back and forth between players for shots
            //check for victory
            //back to player1 shot


        }
    }
}
