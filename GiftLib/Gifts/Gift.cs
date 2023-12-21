using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json.Serialization;
using System.Globalization;

namespace GiftLib
{
    //[JsonDerivedType(typeof(Gift))]
    //[JsonDerivedType(typeof(Candles))]
    //[JsonDerivedType(typeof(Clothes))]
    //[JsonDerivedType(typeof(Cookie))]

    [Serializable]
    public abstract class Gift
    {
        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
        public double Price { get { return _price; }}
        public double Weight { get { return _weight; } }

        private int _id;
        private string _name;
        private double _price;
        private double _weight;

        public Gift() { }
        public Gift(int id, string name, double price, double weight)
        {
            _id = id;
            _name = name;
            _price = price;
            _weight = weight;
        }
        public Gift(string[] data)
        {
            _id = Convert.ToInt32(data[0]);
            _name = data[1];
            _price = Double.Parse(data[2], CultureInfo.InvariantCulture);
            _weight = Double.Parse(data[3], CultureInfo.InvariantCulture);
        }

        public override string ToString()
        {
            return $"{_name} {_price}";
        }
    }
}
