using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftLib
{
    public class Ornaments : Gift
    {
        public OrnamentsMaterial Material { get; set; }
        private OrnamentsMaterial _material;

        public Ornaments(string id, string name, double price, double weight, OrnamentsMaterial material) : base(id, name, price, weight)
        {
            _material = material;
        }
        public Ornaments(string[] data) : base(data)
        {
            _material = (OrnamentsMaterial)Convert.ToInt32(data[4]);
        }
    }
}
