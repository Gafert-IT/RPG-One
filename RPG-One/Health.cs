using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_One
{
    class Health : ITreasure
    {
        public string Name { get; set; } = "Heiltrank";
        public string Type { get; set; }
        private int _life;
        public int Life
        {
            get { return _life; }
            set
            {
                if (Type == "small")
                {
                    _life = 5;
                }
                else if (Type == "big")
                {
                    _life = 10;
                }
            }
        }

        public Health(string type)
        {
            Type = type;            
        }

        public Health(string type, int life)
        {
            Type = type;
            Life = life;
        }
    }
}
