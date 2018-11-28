using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Entities.Misc
{
    [Serializable]
    public class Adress
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public Adress() { }
        public Adress(string city, string street, string building, int apartment)
        {
            City = city;
            Street = street;
            Building = building;
        }
    }
}
