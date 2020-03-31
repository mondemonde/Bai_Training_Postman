using System;
using System.Collections.Generic;
using System.Text;

namespace Dota.Models
{
    public class Character
    {
        public string Name { get; set; }
        public int Life { get; set; }
        public int Mana { get; set; }

        public List<Item> Items { get; set; }



    }
}
