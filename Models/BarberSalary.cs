using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    internal class BarberSalary
    {
        public int Id { get; set; }
        public int BarberId { get; set; }
        public int AddedWage { get; set; }
        public DateTime NewWageDate { get; set; }
    }
}
