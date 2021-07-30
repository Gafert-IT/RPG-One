using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_One
{
    interface IEquipment
    {
        public string Name { get; set; }
        public int SizeHands { get; set; }
        public string Type { get; set; }
    }
}
