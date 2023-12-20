using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Media.Media3D;

namespace ChristmasGiftClient.Model.Gifts
{
    public class Clothes : Gift
    {
        public Fabric Fabric { get; set; }
        private Fabric _fabric;

        public Clothes(string id, string name, double price, double weight, Fabric fabric) : base(id, name, price, weight)
        {
            _fabric = fabric;
        }
    }
}
