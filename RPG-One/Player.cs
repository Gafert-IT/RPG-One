using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace RPG_One
{
    class Player
    {
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Geschicklichkeit { get; set; }
        public int Intelligence { get; set; }
        public int Charisma { get; set; }
        public int Wisdom { get; set; }
        public int Life { get; set; }
        public int Armor { get; set; }
        public int Gold { get; set; }

        public Player(string name, int strength, int geschicklichkeit, int intelligence, int charisma, int wisdom, int life, int armor)
        {
            Name = name;
            Strength = strength;
            Geschicklichkeit = geschicklichkeit;
            Intelligence = intelligence;
            Charisma = charisma;
            Wisdom = wisdom;
            Life = life;
            Armor = armor;
        }

        public static Player NeuerChar()
        {
            Console.Write("\n\tWie soll dein Charakter heißen? ");
            string name = Console.ReadLine();
            Console.WriteLine($"\tOK, dein Name ist: {name}");

            Console.Write("\nDu hast 4 Eigenschaften:\n" +
                            "\tStärke\n" +
                            "\tGeschicklichkeit\n" +
                            "\tIntelligenz\n" +
                            "\tCharisma\n" +
                            "\tWeisheit.\n" +
                            "Das Programm würfelt jetzt deine Werte aus...\n\n");
            Console.WriteLine("(Weiter mit Enter)");
            Console.ReadKey();
            Console.Clear();

            Console.Write("Stärke: ");
            int[] strength = Extensions.Wuerfeln(3, 6, 3);
            for (int i = 0; i < strength.Length; i++)
            {
                Console.Write($"{i + 1}.Wurf: {strength[i]} | ");
            }
            Console.WriteLine($"Deine Stärke beträgt: {strength.Max()}");

            Console.Write("Geschicklichkeit: ");
            int[] geschicklichkeit = Extensions.Wuerfeln(3, 6, 3);
            for (int i = 0; i < geschicklichkeit.Length; i++)
            {
                Console.Write($"{i + 1}.Wurf: {geschicklichkeit[i]} | ");
            }
            Console.WriteLine($"Deine Geschicklichkeit beträgt: {geschicklichkeit.Max()}");

            Console.Write("Intelligenz: ");
            int[] intelligence = Extensions.Wuerfeln(3, 6, 3);
            for (int i = 0; i < intelligence.Length; i++)
            {
                Console.Write($"{i + 1}.Wurf: {intelligence[i]} | ");
            }
            Console.WriteLine($"Deine Intelligenz beträgt: {intelligence.Max()}");

            Console.Write("Charisma: ");
            int[] charisma = Extensions.Wuerfeln(3, 6, 3);
            for (int i = 0; i < charisma.Length; i++)
            {
                Console.Write($"{i + 1}.Wurf: {charisma[i]} | ");
            }
            Console.WriteLine($"Dein Charisma beträgt: {charisma.Max()}");

            Console.Write("Weisheit: ");
            int[] wisdom = Extensions.Wuerfeln(3, 6, 3);
            for (int i = 0; i < wisdom.Length; i++)
            {
                Console.Write($"{i + 1}.Wurf: {wisdom[i]} | ");
            }
            Console.WriteLine($"Deine Weisheit beträgt: {wisdom.Max()}");

            Console.WriteLine();

            Console.Write("Lebensenergie: ");
            int[] life = Extensions.Wuerfeln(4, 6, 3);
            for (int i = 0; i < life.Length; i++)
            {
                Console.Write($"{i + 1}.Wurf: {life[i]} | ");
            }
            Console.WriteLine($"Deine Lebensenergie beträgt: {life.Max()}");

            Console.Write("Rüstung: ");
            int[] armor = Extensions.Wuerfeln(2, 6, 3);
            for (int i = 0; i < armor.Length; i++)
            {
                Console.Write($"{i + 1}.Wurf: {armor[i]} | ");
            }
            Console.WriteLine($"Deine Rüstung beträgt: {armor.Max()}");

            Console.WriteLine();
            Console.WriteLine("(Weiter mit Enter)");
            Console.ReadKey();
            Console.Clear();

            Player player = new Player(name, strength.Max(), geschicklichkeit.Max(), intelligence.Max(), charisma.Max(), wisdom.Max(), life.Max(), armor.Max());            

            Console.WriteLine("Du hast erfolgreich deinen Charakter erstellt\n" +
                    $"Name: {player.Name}\n" +
                    $"Stärke: {player.Strength}\n" +
                    $"Geschicklichkeit: {player.Geschicklichkeit}\n" +
                    $"Intelligenz: {player.Intelligence}\n" +
                    $"Charisma: {player.Charisma}\n" +
                    $"Weisheit: {player.Wisdom}\n" +
                    $"Leben: {player.Life}\n" +
                    $"Rüstung: {player.Armor}\n" +
                   "(Weiter mit Enter)");
            Console.ReadKey();
            Console.Clear();

            return player;
        }

        public void attack(Attacker enemy)
        {
            int attack = Extensions.Wuerfeln(1, 20, 5).Max();

            if (attack > this.Geschicklichkeit)
            {
                int damage = Extensions.Wuerfeln(3, 6, 1).Max();
                Console.WriteLine($"Der Angriff erfolgt mit {damage} Angriffspunkten.");
                Console.WriteLine($"Die Rüstung deines Gegners beträgt {enemy.Armor}.");
                if (damage > enemy.Armor)
                {
                    int lifeLoss = (damage - enemy.Armor);
                    enemy.Hitpoints -= lifeLoss;
                    Console.WriteLine($"Dein Gegner verliert {lifeLoss} Lebenspunkte.\n" +
                        $"Er hat noch {enemy.Hitpoints} Lebenspunkte.\n");
                }
                else
                {
                    Console.WriteLine("Dein Gegner erleidet keinen Schaden.");
                }
            }
            else
            {
                Console.WriteLine("Der Angriff misslingt. Dein Gegner erleidet keinen Schaden.");
            }

        }
    }
}
