using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerVDJ3D_Promedio1_Examen_Individual
{
    public class Player : Character
    {
        public int Mana { get; set; }
        public List<Power> Powers { get; set; }

        public Player(string name, int health, int damage, int mana) : base(name, health, damage)
        {
            Mana = mana;
            Powers = new List<Power>();
        }

        public void UseItem(Item item)
        {
            if (item.Type == ItemType.Health)
            {
                Health += item.Amount;
                System.Console.WriteLine($"Recuperaste {item.Amount} de vida.");
            }
            else if (item.Type == ItemType.Mana)
            {
                Mana += item.Amount;
                System.Console.WriteLine($"Recuperaste {item.Amount} de maná.");
            }
            Items.Remove(item);
        }

        public void UsePower(Power power, Character target)
        {
            if (Mana >= power.ManaCost)
            {
                target.TakeDamage(power.Damage);
                Mana -= power.ManaCost;
                System.Console.WriteLine($"Usaste el poder {power.Name} causando {power.Damage} de daño.");
            }
            else
            {
                System.Console.WriteLine("No tienes suficiente maná para usar ese poder.");
            }
        }
    }
}
