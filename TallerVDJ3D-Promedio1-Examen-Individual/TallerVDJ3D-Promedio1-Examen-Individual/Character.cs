using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerVDJ3D_Promedio1_Examen_Individual
{
    public abstract class Character
    {
        public string Name;
        public int Health;
        public int Damage;
        public List<Item> Items;

        public Character(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Items = new List<Item>();
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0)
                Health = 0;
        }
    }
}
