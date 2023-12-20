using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace ChristmasGiftClient.Model.Gifts
{
    public class Cookie : Gift
    {
        public Dough Dough { get; set; }
        private Dough _dough { get; set; }

        public Cookie(string id, string name, double price, double weight, Dough dough) : base(id, name, price, weight)
        {
            _dough = dough;
        }
    }
}
