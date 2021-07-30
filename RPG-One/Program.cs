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
                    Extensions.GettingAttacked(player);
                    if (player.Life > 0)
                    {
                        Extensions.GetTreasures(player);
                    }                    
                }
                
                Console.WriteLine("Ende!");
                Console.ReadKey();
            } while (true);
        }
    }
}
