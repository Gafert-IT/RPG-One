using System;
using System.Linq;
using System.Threading;

namespace RPG_One
{
    class Attacker
    {
        public string Name { get; set; }
        public int Hitpoints { get; set; }
        public int Armor { get; set; }

        public Attacker(string name, int armor, int hitpoints)
        {
            Name = name;
            Armor = armor;
            Hitpoints = hitpoints;
        }
        public Attacker(int armor, int hitpoints)
        {
            Armor = armor;
            Hitpoints = hitpoints;
        }
        public Attacker(int hitpoints)
        {
            Hitpoints = hitpoints;
        }

        public static Attacker RandomEnemy()
        {
            int[] armor = Extensions.Wuerfeln(2, 4, 3);
            int[] hitpoints = Extensions.Wuerfeln(2, 6, 3);

            Attacker enemy = new Attacker(armor.Max(), hitpoints.Max());

            return enemy;
        }
        public void attack(Player player)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Der Angriff deines Gegners beginnt!\n");
            int attack = Extensions.Wuerfeln(1, 20, 5).Max();

            if (attack > player.Geschicklichkeit)
            {
                Console.WriteLine("\tDer Angriff deines Gegners gelingt.\n");
                Thread.Sleep(2000);
                int damage = Extensions.Wuerfeln(3, 6, 1).Max();
                Console.WriteLine($"\tDer Angriff erfolgt mit {damage} Angriffspunkten.");
                Console.WriteLine($"\tDeine Rüstung beträgt {player.Armor}.\n");
                Thread.Sleep(2000);
                if (damage > player.Armor)
                {
                    int lifeLoss = (damage - player.Armor);
                    player.Life -= lifeLoss;
                    Console.WriteLine($"\tDu verlierst {lifeLoss} Lebenspunkte.\n" +
                        $"\tDu hast noch {player.Life} Lebenspunkte.\n");
                }
                else
                {
                    Console.WriteLine("\tDu erleidest keinen Schaden.\n");
                }
            }
            else
            {
                Console.WriteLine("\tDer Angriff deines Gegners misslingt. Du erleidest keinen Schaden.\n");
                Thread.Sleep(2000);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
