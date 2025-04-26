using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerVDJ3D_Promedio1_Examen_Individual
{
    public class Power
    {
        public string Name;
        public int Damage;
        public int ManaCost;

        public Power(string name, int damage, int manaCost)
        {
            Name = name;
            Damage = damage;
            ManaCost = manaCost;
        }
    }
}
