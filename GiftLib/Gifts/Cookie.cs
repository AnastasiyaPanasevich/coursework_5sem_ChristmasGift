using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftLib
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
