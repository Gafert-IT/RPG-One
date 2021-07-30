using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Numerics;
using System.IO;

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
            string[] knightArray= File.ReadAllLines(@"Knight.txt");
            foreach (string line in knightArray)
            {
                Console.WriteLine(line);
            }
            
            Console.WriteLine("Willkommen fremder Krieger!\n\n" +
                              "Als erstes musst du einen neuen Charakter erstellen...\n" +
                                "(Weiter mit Enter)");
            Console.ReadKey();
            Console.Clear();
        }
        internal static void Spielbeginn()
        {
            Console.Write("\n\tDas Spiel beginnt!\n" +
                                "\tDein Ziel ist es Reichtümer anzuhäufen!\n" +
                                "\tWenn du 100 Goldmünzen gesammelt hast, gewinnst du!\n" +
                                "\tWenn du stirbst... rate mal...\n" +
                                "\t(Weiter mit Enter)");
            Console.ReadKey();
            Console.Clear();
            Console.Write("\n\tDu stehst in einem dunklen, feuchten Keller.\n" +
                "\tDas licht einer kleinen, flackernden Kerze an der Wand\n" +
                "\tzeigt dir drei Türe, die sich direkt vor dir befinden.\n" +
                "\tEs gibt für dich keinen anderen Weg, als durch eine dieser Türen.");
        }
        internal static void GoToNewRoom(Player player)
        {
            room = Room.RandomNewRoom();

            do
            {
                Console.Write($"\n\tWelche Türe möchtest du öffnen? (1-{room.Doors}) ");

            } while (!int.TryParse(Console.ReadLine(), out int output));

            Console.Clear();
            Console.WriteLine($"\n\n\tDu gehst in den nächsten Raum...\n");
        }
        internal static void GettingAttacked(Player player)
        {
            if (room.Attackers > 0)
            {
                Console.WriteLine($"Es wartet {room.Attackers} Feind auf Dich!\n");
                Console.WriteLine("Du wirst angegriffen!\n");
                Console.Write("Möchtest du (K)ämpfen oder (W)eglaufen? ");
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
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Das war die falsche Auswahl. Du bist Tot!");
                        player.Life = 0;
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Du hast Glück, es warten keine Feinde auf Dich.\n");
            }
        }
        internal static void Battle(Player player)
        {
            Attacker enemy = Attacker.RandomEnemy();

            while (player.Life > 0 && enemy.Hitpoints > 0)
            {
                enemy.attack(player);
                player.attack(enemy);
            }
        }
        internal static void GetTreasures(Player player)
        {
            if (room.Treasures > 0)
            {
                Console.WriteLine($"Du hast {room.Treasures} Schätze gefunden!\n");
                GetRandomTreasure(room.Treasures, player);
            }
            else
            {
                Console.WriteLine($"Du hast Pech, es warten keine Schätze auf Dich.\n");
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
                        Console.WriteLine($"Du hast einen grossen Beutel Gold gefunden.\n" +
                            $"Im Beutel befinden sich {treasure.Coins} Goldstuecke.\n" +
                            $"Du besitzt jetzt {player.Gold} Goldstuecke.");
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 6: // small Gold
                        treasure = new Gold("small", 5);
                        player.Gold += treasure.Coins;
                        Console.WriteLine($"Du hast einen kleinen Beutel Gold gefunden.\n" +
                            $"Im Beutel befinden sich {treasure.Coins} Goldstuecke.\n" +
                            $"Du besitzt jetzt {player.Gold} Goldstuecke.");
                        break;
                    case 7 - 10:
                    case 8:
                    case 9:
                    case 10:// Big Health
                        life = new Health("big", 3);
                        player.Life += life.Life;
                        Console.WriteLine($"Du hast einen grossen Heiltrank gefunden.\n" +
                            $"Der Heiltrank fuellt {life.Life} Lebensenergie wieder auf.\n" +
                            $"Du hast jetzt {player.Life} Lebensenergie.");
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
                        Console.WriteLine($"Du hast einen kleinen Heiltrank gefunden.\n" +
                            $"Der Heiltrank fuellt {life.Life} Lebensenergie wieder auf.\n" +
                            $"Du hast jetzt {player.Life} Lebensenergie.");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
