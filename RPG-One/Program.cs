using System;

/***************************************************************************************
*    Title: RPG-One
*    Author: Mike Gafert
*    Date: 30.07.2021
*    Time: 10:34
*    Code version: 0.1.2
*    Availability: https://github.com/Gafert-IT/RPG-One
*    License: GNU General Public License v3.0
*
***************************************************************************************/

namespace RPG_One
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();

                Extensions.Begruessung();

                Player player = Player.NeuerChar();

                Extensions.Spielbeginn();

                while (player.Gold < 100 && player.Life > 0)
                {
                    Extensions.GoToNewRoom(player);

                    if (player.Life > 0)
                    {
                        Extensions.CheckAttackersInRoom(player);
                    }
                    if (player.Life > 0 && !player.Weggelaufen)
                    {
                        Extensions.GetTreasures(player);
                    }
                }

                Console.WriteLine("\n\tDas ist das Ende!");
                Console.ReadKey();                               

            } while (true);
        }
    }
}
