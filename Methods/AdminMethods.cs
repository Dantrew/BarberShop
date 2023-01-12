using BarberShop.Migrations;
using BarberShop.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dapper;

namespace BarberShop.Methods
{
    internal class AdminMethods
    {
        public static void AdminMenu()
        {
            bool runMenu = false;
            Console.Clear();
            while (!runMenu)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("  .--.  .----. .-.  .-..-..-. .-. \r\n / {} \\ } {-. \\}  \\/  {{ ||  \\{ | \r\n/  /\\  \\} '-} /| {  } || }| }\\  { \r\n`-'  `-'`----' `-'  `-'`-'`-' `-' ");
                Console.ResetColor();
                Console.WriteLine($"\nSee options below!\n\n"
                    + "\n1. Booking options."
                    + "\n2. Barber options."
                    + "\n3. Treatment options."
                    + "\n4. Statistics."
                    + "\n5. Return to main menu.");



                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        SeeBookingsFromCustomer();
                        break;
                    case '2':
                        BarberOptions();
                        break;
                    case '3':
                        TreatmentOptions();
                        break;
                    case '4':
                        Statistics();
                        break;
                    case '5':
                        runMenu = true;
                        break;
                    default:
                        HelpMethods.InputInstructions();
                        break;
                }
                Console.Clear();
            }
        }

        private static void Statistics()
        {
            bool runMenu = false;
            Console.Clear();
            while (!runMenu)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" .----..-----. .--. .-----..-. .----..-----..-..----. .----. \r\n{ {__-``-' '-'/ {} \\`-' '-'{ |{ {__-``-' '-'{ || }`-'{ {__-` \r\n.-._} }  } { /  /\\  \\ } {  | }.-._} }  } {  | }| },-..-._} } \r\n`----'   `-' `-'  `-' `-'  `-'`----'   `-'  `-'`----'`----'  ");
                Console.ResetColor();
                Console.WriteLine($"\nSee options for statistics below!\n\n"
                    + "\n1. Show most popular treatment."
                    + "\n2. Show percentage of customers gender."
                    + "\n3. Which barberer has most bookings?"
                    + "\n4. Which is the top 3 most loyal customers?"
                    + "\n5. Return to main menu.");



                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        ShowMostPopularTreatment();
                        break;
                    case '2':
                        ShowPercentageOfCustomersGender();
                        break;
                    case '3':
                        BarberMostBookings();
                        break;
                    case '4':
                        TopThreeLoyalCustomer();
                        break;
                    case '5':
                        runMenu = true;
                        break;
                    default:
                        HelpMethods.InputInstructions();
                        break;
                }
                Console.Clear();
            }
        }

        private static void TopThreeLoyalCustomer()
        {
            using (var db = new BarberShopDbContext())
            {
                var mostPopularBarber = (from b in db.Bookings
                                         join c in db.Customers on b.CustomerId equals c.Id
                                         select new { CustomerName = c.Name, CustomerCount = b.CustomerId }).ToList().GroupBy(t => t.CustomerName);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nYour top three most loyal customers and their amount of booking. \n");
                Console.ResetColor();
                foreach (var mpb in mostPopularBarber.OrderByDescending(t => t.Count()).Take(3))
                {
                    Console.WriteLine($"{mpb.Count()} \t {mpb.Key}");
                }
                Console.ReadLine();
            }
        }

        private static void BarberMostBookings()
        {
            using (var db = new BarberShopDbContext())
            {
                var mostPopularBarber = (from b in db.Bookings
                                        join ba in db.Barbers on b.BarberId equals ba.Id
                                        select new { BarberName = ba.Name, BarberCount = b.BarberId }).ToList().GroupBy(t => t.BarberName);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nBarbers booked customers in descending order. \n");
                Console.ResetColor();
                foreach (var mpb in mostPopularBarber.OrderByDescending(t => t.Count()))
                {
                    Console.WriteLine($"{mpb.Count()} \t {mpb.Key}");
                }
                Console.ReadLine();
            }
        }

        private static void ShowPercentageOfCustomersGender()
        {
            using (var db = new BarberShopDbContext())
            {
                var percentageGender = (from c in db.Customers select c).ToList();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nCustomers gender! \n");
                Console.ResetColor();
                float countMen = 0;
                float countWomen = 0;
                foreach (var p in percentageGender)
                {
                    if (p.Gender == "Man")
                    {
                        countMen++;
                    }
                    else if (p.Gender == "Woman")
                    {
                        countWomen++;
                    }
                }

                float value = 92.197354542F;
                value = (float)System.Math.Round(value, 2);

                float percentageMan = (countMen / (countWomen + countMen)) * 100;
                percentageMan = (float)System.Math.Round(percentageMan, 2);
                float percentageWomen = 100 - percentageMan;
                percentageWomen = (float)System.Math.Round(percentageWomen, 2);
                Console.WriteLine($"{percentageMan}% is men and {percentageWomen}% is women.");
                Console.ReadLine();
            }

        }

        private static void ShowMostPopularTreatment()
        {
            using (var db = new BarberShopDbContext())
            {
                var popularTreatment = (from b in db.Bookings
                                        join t in db.Treatments on b.TreatmentId equals t.Id
                                        select new { TreatmentName = t.Name, TreatmentCount = b.TreatmentId }).ToList().GroupBy(t => t.TreatmentName);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nMost popular treatments! \n");
                Console.ResetColor();
                foreach (var pop in popularTreatment.OrderByDescending(t => t.Count()))
                {
                    Console.WriteLine($"{pop.Count()} \t {pop.Key}");
                }
                Console.ReadLine();
            }
        }

        private static void SeeBookingsFromCustomer()  //Måste lägga till så man inte kan fela när man skriver fel barberId
        {
            using (var db = new BarberShopDbContext())
            {

                var customers = (from d in db.Customers
                                 join b in db.Bookings on d.Id equals b.CustomerId
                                 join t in db.Treatments on b.TreatmentId equals t.Id
                                 join ba in db.Barbers on b.BarberId equals ba.Id
                                 select new { Id = d.Id, Name = d.Name, LastName = d.LastName, BookingId = b.Id, Booking = b.TimeBooking, Treatment = t.Name, Barber = ba.Name, BarberId = ba.Id }).ToList();

                HelpMethods.ShowAllBarbers();
                Console.SetCursorPosition(0, 11);

                Console.WriteLine("\n\nChoose a barber to see his booking calendar.");
                bool failSafeWrongInputBarber = false;
                int whichBarber = 0;
                while (!failSafeWrongInputBarber)
                {
                    whichBarber = HelpMethods.TryNumberInt();
                    if (whichBarber == 2 || whichBarber == 3 || whichBarber == 4)
                    {
                        failSafeWrongInputBarber = true;
                    }
                    else
                    {
                        Console.WriteLine("There is no barber with that Id.");
                    }
                }
                Console.WriteLine("Insert year: ");
                int whichYear = HelpMethods.TryNumberInt();
                Console.WriteLine("Insert week: ");
                int whichWeek = HelpMethods.TryNumberInt();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\t\t\t\t\t Booking Calendar");
                Console.ResetColor();

                string[,] dateTime = new string[7, 8];
                Console.WriteLine();
                bool runMenu = false;
                var barberName = db.Barbers.Where(x => x.Id == whichBarber).ToList().First().Name;
                while (!runMenu)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(".----.  .---.  .---. .-..-..-..-. .-..----. \r\n| {_} }/ {-. \\/ {-. \\| ' / { ||  \\{ || |--' \r\n| {_} }\\ '-} /\\ '-} /| . \\ | }| }\\  {| }-`} \r\n`----'  `---'  `---' `-'`-``-'`-' `-'`----'");
                    Console.ResetColor();
                    Console.WriteLine($"\nSee the customers that have booked times!\n[*] = The customers bookingID.\n");
                    int countDays = 0;
                    for (int i = 0; i < dateTime.GetLength(0); i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        var findRightDayDate = HelpMethods.FindOutTheRightDateFromYearAndWeek(whichYear, whichWeek, countDays);
                        long convertFindRightDayDate = long.Parse(findRightDayDate.ToString("yyyyMMdd"));
                        Console.WriteLine($"[Day {i + 1}: {convertFindRightDayDate}]");
                        Console.ResetColor();
                        for (int j = 0; j < dateTime.GetLength(1); j++)
                        {
                            bool dontWriteOutTime = false;
                            foreach (var c in customers)
                            {

                                int convertToWeek = HelpMethods.GetWeekFromDate(c.Booking);
                                long getDateFromBooking = long.Parse(c.Booking.ToString("yyyyMMddHHmm"));
                                string getDateFromBooking2 = Convert.ToString(getDateFromBooking);
                                long stringTime = long.Parse(findRightDayDate.ToString("yyyyMMdd"));
                                string stringTime2 = Convert.ToString(stringTime);
                                int compareTime = int.Parse(c.Booking.ToString("HH"));
                                string compareTime2 = Convert.ToString(compareTime);
                                if (getDateFromBooking2.Contains(stringTime2) && whichWeek == convertToWeek && compareTime == j + 10 && whichBarber == c.BarberId)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write($"[{c.BookingId}]{c.Name}\t");
                                    Console.ResetColor();
                                    dontWriteOutTime = true;

                                }
                            }
                            if (i == 6)
                            {
                                Console.Write($"Closed\t\t");
                            }
                            else if (dontWriteOutTime == false)
                            {
                                Console.Write($"{j + 10}:00\t\t");
                            }

                        }
                        countDays++;
                        Console.WriteLine($"\n");
                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n\t\t\t\t Year: {whichYear} \t\t\t Week: {whichWeek} ");
                    Console.ResetColor();
                    Console.WriteLine($"\n\t\t[N]ext week \t|\t [P]revious week \t|\t [C]ancel a booking \t|\t [Q]uit calendar");
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.KeyChar)
                    {
                        case 'N':
                        case 'n':
                            if (whichWeek == 52)
                            {
                                whichWeek = 0;
                                whichYear++;
                            }
                            whichWeek++;
                            break;
                        case 'P':
                        case 'p':
                            if (whichWeek == 1)
                            {
                                whichWeek = 53;
                                whichYear--;
                            }
                            whichWeek--;
                            break;
                        case 'C':
                        case 'c':
                            CancelCustomerBoooking();
                            break;
                        case 'Q':
                        case 'q':
                            runMenu = true;
                            break;
                        default:
                            HelpMethods.InputInstructions();
                            break;

                    }
                    Console.Clear();
                }
            }
        }

        private static void CancelCustomerBoooking()
        {
            using (var db = new BarberShopDbContext())
            {

                Console.WriteLine("\nWhich treatment do you want to cancel? Enter ID. (0 returns you to menu)");
                int bookingId = HelpMethods.TryNumberInt();
                var booking = db.Bookings.Where(x => x.Id == bookingId).SingleOrDefault();

                if (booking != null)
                {
                    db.Bookings.Remove((Booking)booking);
                    db.SaveChanges();
                    Console.WriteLine("\nYou have canceled that time!");
                    Console.ReadLine();
                }

            }
        }

        private static void TreatmentOptions()
        {
            bool runMenu = false;
            Console.Clear();
            while (!runMenu)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("  .--.  .----. .-.  .-..-..-. .-. \r\n / {} \\ } {-. \\}  \\/  {{ ||  \\{ | \r\n/  /\\  \\} '-} /| {  } || }| }\\  { \r\n`-'  `-'`----' `-'  `-'`-'`-' `-' ");
                Console.ResetColor();
                Console.WriteLine($"\nSee options for treatment below!\n\n"
                    + "\n1. Add treatment."
                    + "\n2. Change treatment."
                    + "\n3. Remove treatment."
                    + "\n4. Return to admin menu.");

                HelpMethods.ShowAllTreatments();
                Console.SetCursorPosition(0, 11);
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        AddTreatment();
                        break;
                    case '2':
                        ChangeTreatment();
                        break;
                    case '3':

                        break;
                    case '4':
                        runMenu = true;
                        break;
                    default:
                        HelpMethods.InputInstructions();
                        break;
                }

                Console.Clear();
            }
        }

        private static void ChangeTreatment()
        {
            Console.WriteLine("\n\nInput id of the treatment you want to edit.");
            var treatmentId = HelpMethods.TryNumberInt();

            Console.WriteLine("What do you want to edit?\n" +
                "\n1. Name." +
                "\n2. Time." +
                "\n3. Price." +
                "\n4. Return to treatment options.\n");

            ConsoleKeyInfo key = Console.ReadKey(true);
            bool runMenu = false;
            while (!runMenu)
                using (var db = new BarberShopDbContext())
                {
                    switch (key.KeyChar)
                    {
                        case '1':
                            Console.WriteLine("What is the new name");
                            var newName = Console.ReadLine();
                            var treatment = db.Treatments.Where(x => x.Id == treatmentId).SingleOrDefault();

                            if (treatment != null)
                            {
                                treatment.Name = newName;
                                db.SaveChanges();
                            }
                            break;

                        case '2':
                            Console.WriteLine("What is the new price");
                            int newPrice = HelpMethods.TryNumberInt();
                            treatment = db.Treatments.Where(x => x.Id == treatmentId).SingleOrDefault();
                            if (treatment != null)
                            {
                                treatment.Price = newPrice;
                                db.SaveChanges();
                            }
                            break;
                        case '3':
                            Console.WriteLine("What is the new time estimated");
                            int newTime = HelpMethods.TryNumberInt();
                            treatment = db.Treatments.Where(x => x.Id == treatmentId).SingleOrDefault();
                            if (treatment != null)
                            {
                                treatment.Time = newTime;
                                db.SaveChanges();
                            }
                            break;
                        case '4':
                            runMenu = true;
                            break;
                        default:
                            HelpMethods.InputInstructions();
                            break;
                    }
                }
        }

        private static void AddTreatment()
        {
            Console.Write("\n\nTreatment name: ");
            string treatmentName = Console.ReadLine();
            Console.Write("Estimated treatment time in minutes: ");
            int treatmentTime = HelpMethods.TryNumberInt();
            Console.Write("Price in SEK: ");
            int treatmentPrice = HelpMethods.TryNumberInt();

            using (var db = new BarberShopDbContext())
            {
                var newTreatment = new Treatment
                {
                    Name = treatmentName,
                    Time = treatmentTime,
                    Price = treatmentPrice



                };
                var treatmentList = db.Treatments;
                treatmentList.Add(newTreatment);
                db.SaveChanges();
            }
        }

        private static void BarberOptions()
        {
            bool runMenu = false;
            Console.Clear();
            while (!runMenu)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("  .--.  .----. .-.  .-..-..-. .-. \r\n / {} \\ } {-. \\}  \\/  {{ ||  \\{ | \r\n/  /\\  \\} '-} /| {  } || }| }\\  { \r\n`-'  `-'`----' `-'  `-'`-'`-' `-' ");
                Console.ResetColor();
                Console.WriteLine($"\nSee options for barber below!\n\n"
                    + "\n1. Add barber."
                    + "\n2. Change barber."
                    + "\n3. Remove barber."
                    + "\n4. Return to admin menu.");

                HelpMethods.ShowAllBarbers();
                Console.SetCursorPosition(0, 11);
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        AddBarber();
                        break;
                    case '2':
                        ChangeBarber();
                        break;
                    case '3':
                        RemoveBarber();
                        break;
                    case '4':
                        runMenu = true;
                        break;
                    default:
                        HelpMethods.InputInstructions();
                        break;
                }

                Console.Clear();
            }
        }

        private static void RemoveBarber()
        {
            Console.WriteLine("Input id of the barber you want to delete");
            var barberId = HelpMethods.TryNumberInt();

            using (var db = new BarberShopDbContext())
            {
                var barber = db.Barbers.Where(x => x.Id == barberId).SingleOrDefault();

                if (barber != null)
                {
                    db.Barbers.Remove((Barber)barber);
                    db.SaveChanges();
                }
            }
        }

        private static void ChangeBarber()
        {
            HelpMethods.ShowAllBarbers();
            Console.SetCursorPosition(0, 11);
            Console.WriteLine("\n\nInput id of the barber you want to edit.");
            var barberId = HelpMethods.TryNumberInt();
            bool runMenu = false;
            while (!runMenu)
            {

                Console.WriteLine("What do you want to edit?\n" +
                    "\n1. Name." +
                    "\n2. Last Name." +
                    "\n3. Phonenumber." +
                    "\n4. Quit.\n");

                ConsoleKeyInfo key = Console.ReadKey(true);
                using (var db = new BarberShopDbContext())
                {
                    switch (key.KeyChar)
                    {
                        case '1':
                            Console.WriteLine("What is the new name?");
                            var newName = Console.ReadLine();
                            var barber = db.Barbers.Where(x => x.Id == barberId).SingleOrDefault();

                            if (barber != null)
                            {
                                barber.Name = newName;
                                db.SaveChanges();
                            }

                            break;

                        case '2':
                            Console.WriteLine("What is the new last name?");
                            var newLastName = Console.ReadLine();
                            barber = db.Barbers.Where(x => x.Id == barberId).SingleOrDefault();
                            if (barber != null)
                            {
                                barber.LastName = newLastName;
                                db.SaveChanges();
                            }
                            runMenu = true;
                            break;
                        case '3':
                            Console.WriteLine("What is the new phonenumber?");
                            var newPhoneNumber = HelpMethods.TryNumberLong();
                            barber = db.Barbers.Where(x => x.Id == barberId).SingleOrDefault();
                            if (barber != null)
                            {
                                barber.PhoneNumber = newPhoneNumber;
                                db.SaveChanges();
                            }
                            runMenu = true;
                            break;
                        case '4':
                            runMenu = true;
                            break;
                        default:
                            HelpMethods.InputInstructions();
                            break;
                    }
                }
            }
        }

        private static void AddBarber()
        {
            Console.Write("\n\nFirst Name: ");
            string barberName = Console.ReadLine();
            Console.Write("Last Name: ");
            string barberLastName = Console.ReadLine();
            Console.Write("Phonenumber: ");
            long barberPhonenumber = HelpMethods.TryNumberLong();

            using (var db = new BarberShopDbContext())
            {
                var newBarber = new Barber
                {
                    Name = barberName,
                    LastName = barberLastName,
                    PhoneNumber = barberPhonenumber

                };
                var barberList = db.Barbers;
                barberList.Add(newBarber);
                db.SaveChanges();
            }
        }
    }
}
