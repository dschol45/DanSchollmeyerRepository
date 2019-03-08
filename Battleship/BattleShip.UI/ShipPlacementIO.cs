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
    public class ShipPlacementIO
    {
        public static void PlaceAllShips(Board board)
        {
            foreach (var item in Enum.GetValues(typeof(ShipType)))
            {
                while (true)
                {
                    Console.WriteLine($"Place your {item}");

                    PlaceShipRequest request = new PlaceShipRequest
                    {
                        Coordinate = UserIO.GetCoordinates(),
                        Direction = PlayerShipDirection(),
                        ShipType = (ShipType)item, 
                    };
                    ShipPlacement response = new ShipPlacement();
                    response = board.PlaceShip(request);
                    switch (response)
                    {
                        case ShipPlacement.NotEnoughSpace:
                            Console.Clear();
                            Console.WriteLine($"Could not place {item}: Not enough space.\nPlease try again.");
                            continue;
                        case ShipPlacement.Overlap:
                            Console.Clear();
                            Console.WriteLine($"Could not place {item}: Ship overlap.\nPlease try again");
                            continue;
                        case ShipPlacement.Ok:
                            break;
                    }
                    break;
                }
                Console.Clear();
            }
        }
        
        //set up collection of coords to check for isletter and numbers on the appropriate spot and no extras
        public static ShipDirection PlayerShipDirection()
        {
            while (true)
            {
                string direction = UserIO.GetStringFromUser("Please choose direction[ (U)p, (D)own, (L)eft, (R)ight: ");
                switch (direction.ToUpper())
                {
                    case "UP":
                    case "U":
                        return ShipDirection.Up;
                    case "DOWN":
                    case "D":
                        return ShipDirection.Down;
                    case "LEFT":
                    case "L":
                        return ShipDirection.Left;
                    case "RIGHT":
                    case "R":
                        return ShipDirection.Right;
                }
            }
        }
    }
}
