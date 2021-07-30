using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_One
{
    class Weapon : IEquipment
    {
        public string Name { get; set; }
        public int SizeHands { get; set; }
        public string Type { get; set; }
        public int Damage { get; set; }        

        public static void Attack()
        {

        }
    }
}
