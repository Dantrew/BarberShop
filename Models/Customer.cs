using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    internal class Customer : Person
    {
        public string Gender { get; set; }
        public long BirthYear { get; set; }

    }
}
