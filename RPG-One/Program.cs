using System;


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
