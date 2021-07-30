using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_One
{
    public class Room
    {
        public int Attackers { get; set; }
        public int Treasures { get; set; }
        public int Doors { get; set; }

        public Room(int attackers, int treasures, int doors)
        {
            Attackers = attackers;
            Treasures = treasures;
            Doors = doors;
        }

        public static Room RandomNewRoom()
        {
            Random rnd = new Random();

            int attackers = rnd.Next(0, 2);
            int treasures = rnd.Next(1, 2);
            int doors = rnd.Next(1, 4);

            Room room = new Room(attackers, treasures, doors);
            return room;
        }
    }
}
