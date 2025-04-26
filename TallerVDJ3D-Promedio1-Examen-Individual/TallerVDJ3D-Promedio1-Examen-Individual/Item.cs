using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerVDJ3D_Promedio1_Examen_Individual
{
    public enum ItemType
    {
        Health,
        Mana
    }

    public class Item
    {
        public string Name;
        public ItemType Type;
        public int Amount;

        public Item(string name, ItemType type, int amount)
        {
            Name = name;
            Type = type;
            Amount = amount;
        }
    }
}
