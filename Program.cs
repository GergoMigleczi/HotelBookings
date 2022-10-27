using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HotelBookings
{
    struct Booking
    {
        public int ID;
        public int roomNumber;
        public int arrival;
        public int leaving;
        public int numberOfGuests;
        public int breakfast;
        public string GuestName;

        public Booking(int ID, int roomNumber, int arrival, int leaving, int numberOfGuests, int breakfast, string GuestName)
        {
            this.ID = ID;
            this.roomNumber = roomNumber;
            this.arrival = arrival;
            this.leaving = leaving;
            this.numberOfGuests = numberOfGuests;
            this.breakfast = breakfast;
            this.GuestName = GuestName;
        }
    }
    class HotelBookings
    {
        static List<Booking> Bookinglist = new List<Booking>();

        //Read the pitypang.txt file, and store the data
        static void Task1()
        {
            StreamReader sr = new StreamReader("pitypang.txt");
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split();
                int ID = int.Parse(line[0]);
                int roomNumber = int.Parse(line[1]);
                int arrival = int.Parse(line[2]); //The day of the year on which the guests arrived 1-365
                int leaving = int.Parse(line[3]);
                int numberOfGuests = int.Parse(line[4]);
                int breakfast = int.Parse(line[5]);
                string GuestName = line[6];

                Booking item = new Booking(ID, roomNumber, arrival, leaving, numberOfGuests, breakfast, GuestName);
                Bookinglist.Add(item);
            }
            sr.Close();
        }

        //Print the longest stay on the screen
        static void Task2()
        {
            Console.WriteLine("Task 2");

            int longestStay = 0;
            foreach (Booking item in Bookinglist)
            {
                int length = item.leaving - item.arrival;
                if (longestStay < length)
                {
                    longestStay = length;
                }
            }
            foreach (Booking item in Bookinglist)
            {
                if (longestStay == item.leaving - item.arrival)
                {
                    Console.WriteLine($"{item.GuestName} ({item.arrival}) - {longestStay} stayed the longest");
                    break;
                }
            }
        }
        //List the price of each stay in the txt file into a "income.txt" file (income=income) 
        static void Task3()
        {
            Console.WriteLine("Task 3 - bevetel.txt");
            StreamWriter sw = new StreamWriter("bevetel.txt");
            int income = 0;
            foreach (Booking item in Bookinglist)
            {
                int price = 0;
                int length = item.leaving - item.arrival;
                if (item.arrival < 121) // between 01.01 and 04.30 so 1-120 the price is 9000/night 
                {
                    if (item.breakfast == 1) //breakfast is 1100/night/guest Additional bed over two people is 2000/night
                        price = length * 9000 + length * (item.numberOfGuests - 2) * 2000 + item.numberOfGuests * length * 1100;
                    else
                        price = length * 9000 + length * (item.numberOfGuests - 2) * 2000;
                    sw.WriteLine($"{item.ID}:{price}");
                    income = income + price;
                }
                else if (item.arrival < 244) // between 05.01 and 08.31 so 121-243 the price is 10000/night
                {

                    if (item.breakfast == 1) //breakfast is 1100/night/guest Additional bed over two people is 2000/night
                        price = length * 10000 + length * (item.numberOfGuests - 2) * 2000 + item.numberOfGuests * length * 1100;
                    else
                        price = length * 10000 + length * (item.numberOfGuests - 2) * 2000;
                    sw.WriteLine($"{item.ID}:{price}");
                    income = income + price;
                }
                else // rest of the year: 8000/night
                {

                    if (item.breakfast == 1) //breakfast is 1100/night/guest Additional bed over two people is 2000/night
                        price = length * 8000 + length * (item.numberOfGuests - 2) * 2000 + item.numberOfGuests * length * 1100;
                    else
                        price = length * 8000 + length * (item.numberOfGuests - 2) * 2000;
                    sw.WriteLine($"{item.ID}:{price}");
                    income = income + price;
                }
            }
            sw.Flush();
            sw.Close();
            Console.WriteLine($"The income is: {income} Ft");
        }

        //Print how many available rooms are in the time period given by the user
        static void Task4()
        {
            Console.WriteLine("Task 4");
            Console.WriteLine("The day of the year on which you would like to start your holiday:");
            int startingDay = int.Parse(Console.ReadLine());
            Console.WriteLine("The length of your stay: ");
            int length = int.Parse(Console.ReadLine());
            List<int> bookedRooms = new List<int>();
            foreach (Booking item in Bookinglist)
            {
                if (item.arrival <= startingDay + length && item.leaving >= startingDay)
                    if (!bookedRooms.Contains(item.roomNumber))
                        bookedRooms.Add(item.roomNumber);

            }
            Console.WriteLine($"Number of available rooms in that time period: {27 - bookedRooms.Count}.");

        }
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
            Task4();
            Console.ReadKey();
        }
    }
}
