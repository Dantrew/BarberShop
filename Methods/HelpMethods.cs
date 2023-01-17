using BarberShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Methods
{
    internal class HelpMethods
    {
        public static void ShowAllCustomers()
        {
            using (var db = new BarberShopDbContext())
            {
                var customers = (from d in db.Customers
                                 join b in db.Bookings on d.Id equals b.CustomerId
                                 join t in db.Treatments on b.TreatmentId equals t.Id
                                 join ba in db.Barbers on b.BarberId equals ba.Id
                                 select new { Id = d.Id, Name = d.Name, LastName = d.LastName, Booking = b.TimeBooking, Treatment = t.Name, Barber = ba.Name }).ToList();

                foreach (var b in customers)
                {
                    Console.WriteLine($"{b.Id} {b.Name} {b.LastName} {b.Booking} {b.Treatment} {b.Barber}");
                }
            }
        }
        public static int GetWeekFromDate(DateTime insertDate)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(insertDate);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                insertDate = insertDate.AddDays(3);
            }
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(insertDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        public static DateTime FindOutTheRightDateFromYearAndWeek(int year, int weekOfYear, int countUpDays)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;
           
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
           
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            var result = firstThursday.AddDays((weekNum * 7) + countUpDays);

            return result.AddDays(-3);
        }

        public static void ShowAllBarbers()
        {
            int i = 8;
            using (var db = new BarberShopDbContext())
            {
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(55, i);
                Console.WriteLine("Barbers.");
                Console.ResetColor();
                foreach (var b in db.Barbers)
                {
                    i++;
                    Console.SetCursorPosition(55, i);
                    Console.WriteLine($"ID[{b.Id}] {b.Name} {b.LastName}");
                }
            }
        }

        public static void ShowAllTreatments()
        {
            int i = 8;
            using (var db = new BarberShopDbContext())
            {

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(55, i);
                Console.WriteLine("Treatments.");
                Console.ResetColor();
                foreach (var b in db.Treatments)
                {
                    i++;
                    Console.SetCursorPosition(55, i);
                    Console.WriteLine($"ID[{b.Id}] {b.Name}, {b.Time}Min, {b.Price}SEK");
                }
            }
        }

        internal static string TryStringIn()
        {
            bool correctInput = false;
            string anyWord = null;

            while (!correctInput)
            {
                anyWord = Console.ReadLine();
                if (string.IsNullOrEmpty(anyWord))
                {
                    Console.WriteLine("Wrong input! Input can not be null.");
                }
                else
                {
                    correctInput = true;
                }
            }
            return anyWord;
        }

        internal static void InputInstructions()
        {
            Console.WriteLine("Wrong input. Try something else!");
        }

        internal static int TryNumberInt()
        {
            int number = 0;
            bool correctInput = false;

            while (!correctInput)
            {
                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Wrong input! Need a number.");
                }
                else
                {
                    correctInput = true;
                }
            }
            return number;
        }

        internal static long TryNumberLong()
        {
            long number = 0;
            bool correctInput = false;

            while (!correctInput)
            {
                if (!long.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Wrong input! Need a number.");
                }
                else
                {
                    correctInput = true;
                }
            }
            return number;
        }
    }
}
