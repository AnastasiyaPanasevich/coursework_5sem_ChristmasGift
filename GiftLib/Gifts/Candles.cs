using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftLib
{
    public class Candles : Gift
    {
        public CandleMaterial Material { get; set; }
        private CandleMaterial _material;

        public Candles(int id, string name, double price, double weight, CandleMaterial material) : base(id, name, price, weight)
        {
            _material = material;
        }
        public Candles(string[] data) : base(data)
        {
            _material = (CandleMaterial)Convert.ToInt32(data[4]);
        }

        public override string ToString()
        {
            return base.ToString() + $" ({_material})";
        }
    }
}
