using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    internal class Treatment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Time { get; set; }
        public int Price { get; set; }
    }
}
