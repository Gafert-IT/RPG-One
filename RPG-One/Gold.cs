using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_One
{
    class Gold : ITreasure
    {
        public string Name { get; set; }
        public string Type { get; set; }

        private int _coins;  
        public int Coins
        {
            get { return _coins; }
            set
            {
                if (this.Type == "small")
                {
                    _coins = 5;
                }
                else if (this.Type == "big")
                {
                    _coins = 10;
                }
                else
                {
                    _coins = 0;
                }
            }
        }

        public Gold(string type)
        {
            Type = type;         
        }
        public Gold(string type, int coins)
        {
            Type = type;
            Coins = coins;
        }
    }
}
