using BarberShop.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics;
using Microsoft.Data.SqlClient.Server;
using BarberShop.Migrations;

namespace BarberShop.Methods
{
    internal class Methods
    {
        static int globalInlogId;
        public static void RunningMenu()
        {
            bool runProgram = false;
            Console.Clear();
            while (!runProgram)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(".----.   .--.  .---. .----. .----..---.      .----..-. .-. .---. .-.-.  \r\n| {_} } / {} \\ } }}_}| {_} }} |__}} }}_}    { {__-`{ {_} |/ {-. \\| } }} \r\n| {_} }/  /\\  \\| } \\ | {_} }} '__}| } \\     .-._} }| { } }\\ '-} /| |-'  \r\n`----' `-'  `-'`-'-' `----' `----'`-'-'     `----' `-' `-' `---' `-'  ");
                Console.ResetColor();
                Console.WriteLine($"\nWe cut hair and/or beard. See options below!\n\n"
                    + "\n1. Customerbooking."
                    + "\n2. Admin log in."
                    + "\n3. Quit");

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        BookingOptions();
                        break;
                    case '2':
                        AdminMethods.AdminMenu();
                        break;
                    case '3':
                        runProgram = true;
                        break;
                    default:
                        HelpMethods.InputInstructions();
                        break;
                }
                Console.Clear();
            }
        }

        private static void BookingOptions()
        {
            bool runProgram = false;
            Console.Clear();
            while (!runProgram)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(".----.  .---.  .---. .-..-..-..-. .-..----. \r\n| {_} }/ {-. \\/ {-. \\| ' / { ||  \\{ || |--' \r\n| {_} }\\ '-} /\\ '-} /| . \\ | }| }\\  {| }-`} \r\n`----'  `---'  `---' `-'`-``-'`-' `-'`----'");
                Console.ResetColor();
                Console.WriteLine($"\nSee options below for your booking!\n\n"
                    + "\n1. Register as a customer."
                    + "\n2. Log in."
                    + "\n3. Return to main menu. ");



                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        RegisterAsACustomer();
                        break;
                    case '2':
                        globalInlogId = 0;
                        Login();
                        break;
                    case '3':
                        runProgram = true;
                        break;
                    default:
                        HelpMethods.InputInstructions();
                        break;
                }
                Console.Clear();
            }
        }

        private static void InlogedMenu()
        {
            bool runProgram = false;
            Console.Clear();
            while (!runProgram)
            {
                using (var db = new BarberShopDbContext())
                {
                    var id = db.Customers.Where(i => i.Id == globalInlogId).Select(x => x.Name).SingleOrDefault();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(".-.  .-..-.  .-.    .-.-.  .--.  .----..----. .----. \r\n}  \\/  { \\ \\/ /     | } }}/ {} \\ | |--'} |__}{ {__-` \r\n| {  } |  `-\\ }     | |-'/  /\\  \\| }-`}} '__}.-._} } \r\n`-'  `-'    `-'     `-'  `-'  `-'`----'`----'`----'  ");
                    Console.ResetColor();
                    Console.WriteLine($"\nHello {id}, You look good today! Welcome to your page!\n\n"
                        + "\n1. See calendar and make a booking."
                        + "\n2. See your booked times."
                        + "\n3. Change personal information."
                        + "\n4. Log out. ");



                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.KeyChar)
                    {
                        case '1':
                            CustomerCalendarAndBooking();
                            break;
                        case '2':
                            SeeYourBookedTimes();
                            break;
                        case '3':
                            ChangePersonalInformation();
                            break;
                        case '4':
                            runProgram = true;
                            break;
                        default:
                            HelpMethods.InputInstructions();
                            break;
                    }
                    Console.Clear();
                }
            }
        }

        private static void ChangePersonalInformation()
        {
            using (var db = new BarberShopDbContext())
            {
                bool runMenu = false;
                while (!runMenu)
                {
                    foreach (var c in db.Customers.Where(x => x.Id == globalInlogId))
                    {
                        Console.WriteLine($"\n\n{c.Name} | {c.LastName} | {c.Gender} | {c.BirthYear} | {c.PhoneNumber}");
                    }

                    Console.WriteLine("\n\nWhat do you want to edit?\n" +
                         "\n1. Name." +
                         "\n2. Last Name." +
                         "\n3. Phonenumber." +
                         "\n4. Quit.\n");

                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.KeyChar)
                    {
                        case '1':
                            Console.WriteLine("What is the new name?");
                            var newName = Console.ReadLine();
                            var customerEdit = db.Customers.Where(x => x.Id == globalInlogId).SingleOrDefault();

                            if (customerEdit != null)
                            {
                                customerEdit.Name = newName;
                                db.SaveChanges();
                            }
                            runMenu = true;
                            break;

                        case '2':
                            Console.WriteLine("What is the new last name?");
                            var newLastName = Console.ReadLine();
                            customerEdit = db.Customers.Where(x => x.Id == globalInlogId).SingleOrDefault();
                            if (customerEdit != null)
                            {
                                customerEdit.LastName = newLastName;
                                db.SaveChanges();
                            }
                            runMenu = true;
                            break;
                        case '3':
                            Console.WriteLine("What is the new phonenumber?");
                            var newPhoneNumber = HelpMethods.TryNumberLong();
                            customerEdit = db.Customers.Where(x => x.Id == globalInlogId).SingleOrDefault();
                            if (customerEdit != null)
                            {
                                customerEdit.PhoneNumber = newPhoneNumber;
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

        private static void SeeYourBookedTimes()
        {
            using (var db = new BarberShopDbContext())
            {
                var customers = (from d in db.Customers
                                 join b in db.Bookings on d.Id equals b.CustomerId
                                 join t in db.Treatments on b.TreatmentId equals t.Id
                                 join ba in db.Barbers on b.BarberId equals ba.Id
                                 select new { ID = d.Id, Booking = b.TimeBooking, Treatment = t.Name, Barber = ba.Name, BookingId = b.Id }).ToList();

                Console.WriteLine("");
                bool runMenu = false;
                while (!runMenu)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(".-.  .-..-.  .-.    .-.-.  .--.  .----..----. .----. \r\n}  \\/  { \\ \\/ /     | } }}/ {} \\ | |--'} |__}{ {__-` \r\n| {  } |  `-\\ }     | |-'/  /\\  \\| }-`}} '__}.-._} } \r\n`-'  `-'    `-'     `-'  `-'  `-'`----'`----'`----'  ");
                    Console.ResetColor();
                    Console.WriteLine($"\nHere is your booked times!\n\n");

                    foreach (var b in customers.Where(x => x.ID == globalInlogId))
                    {
                        Console.WriteLine($"BookingId: [{b.BookingId}] | {b.Booking} | Treatment: {b.Treatment} | Barber: {b.Barber}");
                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n\n[D]ecline booking \t|\t [B]ack to menu");
                    Console.ResetColor();
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.KeyChar)
                    {
                        case 'D':
                        case 'd':
                            DeclineBooking();
                            runMenu = true;
                            break;
                        case 'B':
                        case 'b':
                            runMenu = true;
                            break;
                        default:
                            HelpMethods.InputInstructions();
                            break;

                    }

                }
            }
        }


        private static void DeclineBooking()
        {
            using (var db = new BarberShopDbContext())
            {

                Console.WriteLine("Which treatment do you want to cancel? Enter ID. (0 returns you to menu)");
                int treatmentId = HelpMethods.TryNumberInt();
                var booking = db.Bookings.Where(x => x.Id == treatmentId).SingleOrDefault();

                if (booking != null)
                {
                    db.Bookings.Remove((Booking)booking);
                    db.SaveChanges();
                    Console.WriteLine("\nYou have canceled your time!");
                    Console.ReadLine();
                }

            }
        }

        private static void CustomerCalendarAndBooking()  // TODO: Ändra så barber inte är hårdkodat.
        {
            using (var db = new BarberShopDbContext())
            {

                var customers = (from d in db.Customers
                                 join b in db.Bookings on d.Id equals b.CustomerId
                                 join t in db.Treatments on b.TreatmentId equals t.Id
                                 join ba in db.Barbers on b.BarberId equals ba.Id
                                 select new { Id = d.Id, Name = d.Name, LastName = d.LastName, Booking = b.TimeBooking, Treatment = t.Name, Barber = ba.Name, BarberId = ba.Id }).ToList();

                HelpMethods.ShowAllBarbers();
                Console.SetCursorPosition(0, 11);
                Console.WriteLine("\n\nChoose a barber to see available times in calendar.");
                int whichBarber = 0;
                bool failSafeWrongInputBarber = false;
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
                string getDateFromBooking2 = "";
                Console.WriteLine();
                bool runMenu = false;
                var barberName = db.Barbers.Where(x => x.Id == whichBarber).ToList().First().Name;
                while (!runMenu)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(".----.  .---.  .---. .-..-..-..-. .-..----. \r\n| {_} }/ {-. \\/ {-. \\| ' / { ||  \\{ || |--' \r\n| {_} }\\ '-} /\\ '-} /| . \\ | }| }\\  {| }-`} \r\n`----'  `---'  `---' `-'`-``-'`-' `-'`----'");
                    Console.ResetColor();
                    Console.WriteLine($"\nSee available times, and save the time for booking!\n\n");
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
                                getDateFromBooking2 = Convert.ToString(getDateFromBooking);
                                long stringTime = long.Parse(findRightDayDate.ToString("yyyyMMdd"));
                                string stringTime2 = Convert.ToString(stringTime);
                                int compareTime = int.Parse(c.Booking.ToString("HH"));
                                string compareTime2 = Convert.ToString(compareTime);
                                if (getDateFromBooking2.Contains(stringTime2) && whichWeek == convertToWeek && compareTime == j + 10 && whichBarber == c.BarberId)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write($"Reserved\t");
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
                    Console.WriteLine($"\n\t\t\t Year: {whichYear} \t\t\t Week: {whichWeek} ");
                    Console.ResetColor();
                    Console.WriteLine($"\n[N]ext week \t|\t [P]revious week \t|\t [B]ook a time with {barberName} \t|\t [Q]uit calendar");
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
                        case 'B':
                        case 'b':
                            BookATime(whichBarber, getDateFromBooking2);
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

        private static void BookATime(int whichBarber, string getDateFromBooking2)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(".----.  .---.  .---. .-..-..-..-. .-..----. \r\n| {_} }/ {-. \\/ {-. \\| ' / { ||  \\{ || |--' \r\n| {_} }\\ '-} /\\ '-} /| . \\ | }| }\\  {| }-`} \r\n`----'  `---'  `---' `-'`-``-'`-' `-'`----'");
            Console.ResetColor();
            Console.WriteLine($"\nEnter the theese questions to make your booking!\n\n");
            HelpMethods.ShowAllTreatments();
            Console.Write("\n\nWhich treatment? Enter treatment ID. ");
            int treatmentId = HelpMethods.TryNumberInt();
            bool runOptions = false;
            DateTime whichDayAndTime = DateTime.Now;
            while (!runOptions)
            {

                Console.Write("Which date?(yyyyMMdd) ");
                string date = Console.ReadLine();
                Console.Write("Which time?(HHmm) ");
                string time = Console.ReadLine();
                string format = "yyyyMMddHHmm";
                string eventdatetime = date + time;

                if (eventdatetime == getDateFromBooking2)
                {
                    Console.WriteLine("That time is already reserved!");
                }
                else
                {
                    try
                    {
                        whichDayAndTime = DateTime.ParseExact(eventdatetime, format, CultureInfo.InvariantCulture);
                        runOptions = true;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Your booking was successful!");
                        Console.ResetColor();
                        Console.ReadLine();
                    }
                    catch
                    {
                        Console.WriteLine("Wrong input. Did you enter yyyy.MM.DD and HH:mm correct?");
                    }
                }
            }

            using (var db = new BarberShopDbContext())
            {
                var newBooking = new Booking
                {
                    CustomerId = globalInlogId,
                    BarberId = whichBarber,
                    TreatmentId = treatmentId,
                    TimeBooking = whichDayAndTime



                };
                var bookingList = db.Bookings;
                bookingList.Add(newBooking);
                db.SaveChanges();
            }
        }

        private static void RegisterAsACustomer()
        {

            Console.Write("\n\nFirst Name: ");
            string customerName = Console.ReadLine();
            Console.Write("Last Name: ");
            string customerLastName = Console.ReadLine();
            Console.Write("Gender [1]Man or [2]Woman: ");
            string customerGender = " ";
            bool chooseRightNumber = false;
            while (!chooseRightNumber)
            {
                int customerGenderChoise = HelpMethods.TryNumberInt();

                if (customerGenderChoise == 1)
                {
                    customerGender = "Man";
                    chooseRightNumber = true;
                }
                else if (customerGenderChoise == 2)
                {
                    customerGender = "Woman";
                    chooseRightNumber = true;
                }
                else
                    Console.WriteLine("Wrong input. See options above!");
            }
            Console.Write("Birthday (YYYYMMDDXXXX): ");
            long customerBirthday = HelpMethods.TryNumberLong();
            Console.WriteLine("Phonenumber: ");
            long customerPhoneNumber = HelpMethods.TryNumberLong();

            using (var db = new BarberShopDbContext())
            {
                var newCustomer = new Customer
                {
                    Name = customerName,
                    LastName = customerLastName,
                    Gender = customerGender,
                    BirthYear = customerBirthday,
                    PhoneNumber = customerPhoneNumber
                };
                var customerList = db.Customers;
                customerList.Add(newCustomer);
                db.SaveChanges();

                var showInlogId = db.Customers.Where(c => c.BirthYear == customerBirthday);
                foreach (var s in showInlogId)
                {
                    Console.WriteLine($"Here is your inlog ID [{s.Id}], remember it!");
                }
                Console.ReadLine();
            }
        }
        private static void Login()
        {

            Console.WriteLine("\nPlease enter your log in id: ");
            int loginId = HelpMethods.TryNumberInt();
            Console.WriteLine("Password (same as you social security number, YYYYMMDDXXXX): ");
            long socialSecurityNumber = HelpMethods.TryNumberLong();

            using (var db = new BarberShopDbContext())
            {
                var id = db.Customers.Where(i => i.BirthYear == socialSecurityNumber && i.Id == loginId);
                foreach (var i in id)
                {
                    globalInlogId = i.Id;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You have succesful logged in!");
                    Console.ResetColor();
                    Console.ReadLine();
                    InlogedMenu();
                }
                if (globalInlogId == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong user or password!");
                    Console.ResetColor();
                    Console.ReadLine();
                }
            }
        }
    }
}

