using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Numerics;
using System.IO;
using System.Threading;

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
    class Extensions
    {
        static Room room;

        internal static int[] Wuerfeln(int anzahlWuerfel, int wuerfelSeiten, int haeufigkeit)
        {
            Random rnd = new Random();

            int[] wuerfe = new int[haeufigkeit];
            for (int i = 0; i < wuerfe.Length; i++)
            {
                wuerfe[i] = rnd.Next(anzahlWuerfel, (anzahlWuerfel * wuerfelSeiten) + 1);
            }
            return wuerfe;
        }
        internal static void Begruessung()
        {
            string[] knightArray = File.ReadAllLines(@"Knight.txt");
            foreach (string line in knightArray)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("Willkommen fremder Krieger!\n\n" +
                "Dieses Abenteuer wurde nur fuer DICH geschrieben!\n\n" +

                                "(Weiter mit Enter)");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Als erstes musst du einen neuen Charakter erstellen...\n");
        }
        internal static void Spielbeginn()
        {
            Console.WriteLine("\n\tDas Spiel beginnt!\n");
            Thread.Sleep(3000);
            Console.WriteLine("\tDein Ziel ist es Reichtümer anzuhäufen!\n");
            Thread.Sleep(3000);
            Console.WriteLine("\tWenn du 100 Goldmünzen gesammelt hast, gewinnst du!\n");
            Thread.Sleep(3000);
            Console.WriteLine("\tWenn du stirbst... rate mal...Ist alles futsch!\n");
            Thread.Sleep(3000);
            Console.WriteLine("\t(Weiter mit Enter)");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("\n\tDu stehst in einem dunklen, feuchten Keller.\n");
            Thread.Sleep(2000);
            Console.WriteLine("\tDas licht einer kleinen, flackernden Kerze an der Wand");
            Console.WriteLine("\tzeigt dir drei Türe, die sich direkt vor dir befinden.\n");
            Thread.Sleep(5000);
            Console.WriteLine("\tEs gibt für dich keinen anderen Weg, als durch eine dieser Türen.");

        }
        internal static void GoToNewRoom(Player player)
        {
            room = Room.RandomNewRoom();
            player.Weggelaufen = false;

            Thread.Sleep(3000);

            if (room.Doors > 1)
            {
                int output;
                do
                {
                    Console.Write($"\n\tWelche Türe möchtest du öffnen? (1-{room.Doors}) ");
                } while (!int.TryParse(Console.ReadLine(), out output));
                if (output > room.Doors || output < 1)
                {
                    player.falscheAuswahl();
                }
            }
            else
            {
                Console.Write("\n\t(Weiter mit Enter)");
                Console.ReadKey();
            }
            if (player.Life > 0)
            {
                Console.Clear();
                Console.WriteLine($"\n\n\tDu gehst in den nächsten Raum...\n");
                Thread.Sleep(3000);
            }
        }
        internal static void CheckAttackersInRoom(Player player)
        {
            if (room.Attackers > 0)
            {
                Console.WriteLine($"\tEs wartet {room.Attackers} Feind auf Dich!\n");
                Thread.Sleep(2000);
                Console.WriteLine("\tDu wirst angegriffen!\n");
                Thread.Sleep(2000);
                Console.Write("\tMöchtest du (K)ämpfen oder (W)eglaufen? ");
                ConsoleKeyInfo cki = Console.ReadKey();
                Console.WriteLine();

                switch (cki.Key)
                {
                    case ConsoleKey.K:
                        Battle(player);
                        break;
                    case ConsoleKey.W:
                        Random rnd = new Random();
                        int[] lifeLoss = Wuerfeln(1, 6, 2);
                        player.Life -= lifeLoss.Min();
                        Console.WriteLine($"Du verlierst {lifeLoss.Min()} Lebenspunkte.\n" +
                            $"Du hast noch {player.Life} Lebenspunkte.\n");
                        player.Weggelaufen = true;
                        break;
                    default:
                        player.falscheAuswahl();
                        break;
                }
            }
            else
            {
                Console.WriteLine($"\tDu hast Glück, es warten keine Feinde auf Dich.\n");
            }
        }
        internal static void Battle(Player player)
        {
            Attacker enemy = Attacker.RandomEnemy();

            Console.Clear();

            while (player.Life > 0 && enemy.Hitpoints > 0 && !player.Weggelaufen)
            {
                enemy.attack(player);

                Console.Write("\tMöchtest du deinen Gegner ebenfalls (A)ngreifen oder (W)eglaufen? ");
                ConsoleKeyInfo cki = Console.ReadKey();
                Console.WriteLine();

                switch (cki.Key)
                {
                    case ConsoleKey.A:
                        player.attack(enemy);
                        break;
                    case ConsoleKey.W:
                        Console.Clear();
                        Random rnd = new Random();
                        int[] lifeLoss = Wuerfeln(1, 6, 2);
                        player.Life -= lifeLoss.Min();
                        Console.WriteLine($"\n\tDu verlierst {lifeLoss.Min()} Lebenspunkte.\n" +
                            $"\tDu hast noch {player.Life} Lebenspunkte.\n");
                        player.Weggelaufen = true;
                        break;
                    default:
                        player.falscheAuswahl();
                        break;
                }
            }

            if (enemy.Hitpoints == 0)
            {
                Console.WriteLine("\tDein Gegner ist tot! Du hast den Kapf gewonnen!\n");
                Thread.Sleep(2000);
                Console.Write("\t(Weiter mit Enter)");
                Console.ReadKey();
                Console.Clear();
            }
            else if (player.Life == 0)
            {
                player.die();
            }
        }
        internal static void GetTreasures(Player player)
        {
            if (room.Treasures > 0)
            {
                if (room.Treasures == 1)
                {
                    Console.WriteLine($"\tDu hast {room.Treasures} Schatz gefunden!\n");
                    Thread.Sleep(3000);
                    Console.Write("\tmoechtest du den Schatz oeffnen? (j/n): ");
                }
                else // bis jetzt kann nur ein Schatz gefunden werden
                {
                    Console.WriteLine($"\tDu hast {room.Treasures} Schätze gefunden!\n");
                    Thread.Sleep(3000);
                    Console.Write("\tmoechtest du den Schatz oeffnen? ");
                }
                ConsoleKeyInfo cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.J:
                        GetRandomTreasure(room.Treasures, player);
                        break;
                    case ConsoleKey.N:
                        break;
                    default:
                        player.falscheAuswahl();
                        break;
                }

            }
            else
            {
                Console.WriteLine($"\tDu hast Pech, es warten keine Schätze auf Dich.\n");
            }
        }
        internal static void GetRandomTreasure(int treasuresCount, Player player)
        {
            for (int i = 1; i <= treasuresCount; i++)
            {
                int zufall = Wuerfeln(1, 20, 1).Min();
                Gold treasure;
                Health life;

                switch (zufall)
                {
                    case 1:
                    case 2: // Big Gold
                        treasure = new Gold("big", 10);
                        player.Gold += treasure.Coins;
                        Console.WriteLine($"\n\tDu hast einen grossen Beutel Gold gefunden.\n");
                        Thread.Sleep(2000);
                        Console.WriteLine($"\tIm Beutel befinden sich {treasure.Coins} Goldstuecke.");
                        Console.WriteLine($"\tDu besitzt jetzt {player.Gold} Goldstuecke.");
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 6: // small Gold
                        treasure = new Gold("small", 5);
                        player.Gold += treasure.Coins;
                        Console.WriteLine($"\n\tDu hast einen kleinen Beutel Gold gefunden.\n");
                        Thread.Sleep(2000);
                        Console.WriteLine($"\tIm Beutel befinden sich {treasure.Coins} Goldstuecke.");
                        Console.WriteLine($"\tDu besitzt jetzt {player.Gold} Goldstuecke.");
                        break;
                    case 7 - 10:
                    case 8:
                    case 9:
                    case 10:// Big Health
                        life = new Health("big", 3);
                        player.Life += life.Life;
                        Console.WriteLine($"\n\tDu hast einen grossen Heiltrank gefunden.\n");
                        Thread.Sleep(2000);
                        Console.WriteLine($"\tDer Heiltrank fuellt {life.Life} Lebensenergie wieder auf.");
                        Console.WriteLine($"\tDu hast jetzt {player.Life} Lebensenergie.");
                        break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20: // small Health
                        life = new Health("small", 1);
                        player.Life += life.Life;
                        Console.WriteLine($"\n\tDu hast einen kleinen Heiltrank gefunden.\n");
                        Thread.Sleep(2000);
                        Console.WriteLine($"\tDer Heiltrank fuellt {life.Life} Lebensenergie wieder auf.");
                        Console.WriteLine($"\tDu hast jetzt {player.Life} Lebensenergie.");
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
