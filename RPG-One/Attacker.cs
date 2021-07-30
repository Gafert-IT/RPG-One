using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            int attack = Extensions.Wuerfeln(1, 20, 5).Max();

            if (attack > player.Geschicklichkeit)
            {
                int damage = Extensions.Wuerfeln(3, 6, 1).Max();
                Console.WriteLine($"Der Angriff erfolgt mit {damage} Angriffspunkten.");
                Console.WriteLine($"Deine Rüstung beträgt {player.Armor}.");
                if (damage > player.Armor)
                {
                    int lifeLoss = (damage - player.Armor);
                    player.Life -= lifeLoss;
                    Console.WriteLine($"Du verlierst {lifeLoss} Lebenspunkte.\n" +
                        $"Du hast noch {player.Life} Lebenspunkte.\n");
                }
                else
                {
                    Console.WriteLine("Du erleidest keinen Schaden.");
                }
            }
            else
            {
                Console.WriteLine("Der Angriff misslingt. Du erleidest keinen Schaden.");
            }

        }
    }
}
