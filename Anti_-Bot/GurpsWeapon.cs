using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anti__Bot
{
    public class GurpsWeapon
    {
        public string TL { get; set; }

        public string Weapon { get; set; }

        public string Damage { get; set; }

        public string Acc { get; set; }

        public string Range { get; set; }

        public string Weight { get; set; }

        public string RoF { get; set; }

        public string Shots { get; set; }

        public string ST { get; set; }

        public string Bulk { get; set; }

        public string Rcl { get; set; }

        public string Cost { get; set; }

        public string LC { get; set; }


        public GurpsWeapon()
        {

        }

        public GurpsWeapon(string tL, string weapon, string damage, string acc, string range, string weight, string roF, string shots, string sT, string bulk, string rcl, string cost, string lC)
        {
            TL = tL;
            Weapon = weapon;
            Damage = damage;
            Acc = acc;
            Range = range;
            Weight = weight;
            RoF = roF;
            Shots = shots;
            ST = sT;
            Bulk = bulk;
            Rcl = rcl;
            Cost = cost;
            LC = lC;
        }
    }
}
