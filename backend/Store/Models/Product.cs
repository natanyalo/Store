using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string PathImage { get; set; }
        public DateTime Valid { get; set; }

    }
}
