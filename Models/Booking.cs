using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    internal class Booking
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int BarberId { get; set; }
        public int TreatmentId { get; set; }
        public DateTime TimeBooking { get; set; }

        //public ICollection<Customer> Customers { get; set; }
        //public ICollection<Barber> Barbers { get; set; } 
    }
}
