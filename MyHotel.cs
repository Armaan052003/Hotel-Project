
using System;
using System.Collections.Generic;

public class Hotel
{   
    
    private static int _bookingId = 0;
    private Dictionary<int, Hotel> database = new Dictionary<int, Hotel>();

    // user side details
    
    private int _id;
    private string _username;

    private Dictionary<string, int> _user_rooms_bookingDays = new Dictionary<string, int>();
    private Dictionary<string, List<string>> _user_rooms_specialities = new Dictionary<string, List<string>>();

    private Dictionary<string, int> _user_rooms_booked = new Dictionary<string, int>
    {

        { "Gold",0},
        {"Silver",0 },
        {"Bronze",0 }

    };


    // System Details
    private int _totalBookingPrice = 0;


    private static Dictionary<string, int> _rooms_availiable = new Dictionary<string, int> {

        { "Gold",5},
        {"Silver",10 },
        {"Bronze",15 }

    };

    private Dictionary<string, int> _FixedBookingPrice = new Dictionary<string, int>
    {
        {"Gold",4500}, 
        {"Silver",3000},
        {"Bronze",2000}

    };

    
    private Dictionary<string, Dictionary<string, int>> _rooms_speciality_data = new Dictionary<string, Dictionary<string, int>>
    {
       
        {
            "Gold", new Dictionary<string, int>
            {
                {"Ac",1300 },
                {"Two Beds",2000},
                {"One Bed",1500},
                {"None",0}

            }
        },
        

        
        {
            "Silver", new Dictionary<string, int>
            {
                {"Ac", 1200 },
                {"Two Beds",1800},
                {"One Bed",1300},
                {"None",0}

            }
        },
       

        
        {
            "Bronze", new Dictionary<string, int>
            {
                {"Ac", 1000 },
                {"Two Beds",1500},
                {"One Bed",1100},
                {"None",0}

            }
        },
        
    };

    public Hotel(int id,string username)
    {
        _id = id;
        _username = username;
    }

  
    public void getBookingBill()
    {
        _totalBookingPrice = 0;
        int price = 0;

        foreach(KeyValuePair<string,List<string>> userRoom in _user_rooms_specialities)
        {
            
            price += _FixedBookingPrice[userRoom.Key];

            foreach (string speciality in userRoom.Value)
            {

                price += _rooms_speciality_data[userRoom.Key][speciality];
                           
            }

            price *= _user_rooms_booked[userRoom.Key] * _user_rooms_bookingDays[userRoom.Key];
            _totalBookingPrice+= price;
            price = 0;
        }

        
        Console.WriteLine($"\nYour Booking Bill is {_totalBookingPrice} INR/- Only");
        database.Add(_bookingId, this);
    }
    
    public void getMyBookingDetails()
    {
        foreach(KeyValuePair<string,List<string>> user_room_speciality in _user_rooms_specialities)
        {
            Console.WriteLine("*********************************************************************");
            Console.WriteLine($"Room Type - {user_room_speciality.Key}, No. of Rooms = {_user_rooms_booked[user_room_speciality.Key]} No. of Days = {_user_rooms_bookingDays[user_room_speciality.Key]}");

            foreach(string speciality in user_room_speciality.Value)
            {
                int cost = _rooms_speciality_data[user_room_speciality.Key][speciality];
                Console.WriteLine($"{speciality} = {cost} INR");
            }
        }
    }
 
    public void displayRoomsInformation()
    {
        Console.WriteLine("\n********************************************************");

        foreach (KeyValuePair<string,int> roomFixedCharges in _FixedBookingPrice)
        {
                Console.WriteLine($"\nRoom Fixed Charges per day : {roomFixedCharges.Key} Room => {roomFixedCharges.Value} INR\n");

               
                Console.WriteLine("Extra Prices for Specialities : \n");

                foreach (KeyValuePair<string, int> specialites_data in _rooms_speciality_data[roomFixedCharges.Key])
                {
                    Console.WriteLine($"{specialites_data.Key} = {specialites_data.Value} INR");   
                }

            Console.WriteLine("\n********************************************************");
        }

        
    }
                 
    public void takeBooking()
    {   

        List<string> specialities = new List<string>();
        int nRooms=0, nDays=0;

        Console.WriteLine("\n================================================================");
        Console.WriteLine($"1. Gold");
        Console.WriteLine($"2. Silver");
        Console.WriteLine($"3. Bronze\n");

        Console.Write("Choose Room (by Typing only name as Shown in List) : ");
        string roomType = Console.ReadLine();
        Console.WriteLine();

        if (_rooms_speciality_data.ContainsKey(roomType))
        {
            foreach (KeyValuePair<string, int> specialites_data in _rooms_speciality_data[roomType])
            {
                Console.WriteLine($"{specialites_data.Key} = {specialites_data.Value} INR");
            }

            Console.Write("\nChoose Speciality (by Typing only name as Shown in List) : ");
           
            Console.WriteLine("Enter NONE to exit..");

            while (true)
            {
                Console.Write("Speciality Name = ");
                string specialityName = Console.ReadLine();

                if (specialityName != "NONE")
                {
                    if (_rooms_speciality_data[roomType].ContainsKey(specialityName))
                    {
                        specialities.Add(specialityName);
                    }
                    else
                    {
                        Console.WriteLine($"{specialityName} not availiable");
                    }
                }
                else
                {
                    break;
                }
            }

            Console.Write("No. of Rooms = ");
            int temp_nRooms = int.Parse(Console.ReadLine());

            
            if (_rooms_availiable[roomType] == 0)
            {
                Console.WriteLine("Rooms Not availiable for this category.. ");
                return;
            }

            else if ((_rooms_availiable[roomType] - temp_nRooms) > 0)
            {

                if (temp_nRooms < 0) 
                {
                    Console.WriteLine("Please enter greater than or equal to zero number of Rooms.. ");
                    
                    while(temp_nRooms<0)
                    {
                        Console.Write("No. of Rooms = ");
                        temp_nRooms = int.Parse(Console.ReadLine());
                    }
                    

                }


                nRooms = temp_nRooms;

            }

            if ((_rooms_availiable[roomType] - temp_nRooms) < 0)
            {   
                Console.WriteLine($" Only {_rooms_availiable[roomType]} availiable for this category ..");
                
                while(((_rooms_availiable[roomType] - temp_nRooms) <0) || (temp_nRooms < 0))
                {
                    Console.Write("No. of Rooms (greater than eaual or to zero) = ");
                    temp_nRooms = int.Parse(Console.ReadLine());
                }

                nRooms = temp_nRooms;
            }

            Console.Write("No of Booking Days = ");
            int temp_nDays = int.Parse(Console.ReadLine());

            if (temp_nDays > 0) 
            {
                nDays = temp_nDays;
            }
            else
            {
                Console.WriteLine("Please Enter No. of Booking days greater than 0");
                while(temp_nDays<=0)
                {
                    Console.Write("No of Booking Days = ");
                    temp_nDays = int.Parse(Console.ReadLine());
                }

                nDays = temp_nDays;
            }
            


        }
        else
        {
            Console.WriteLine("Booking Failed..");
            return;
        }

        _user_rooms_specialities.Add(roomType, specialities);
        _user_rooms_bookingDays.Add(roomType, nDays);
 
        _user_rooms_booked[roomType] += nRooms;
        _rooms_availiable[roomType] -= nRooms;

        _bookingId++;
    }
    

    public static void Main(string[] args)
    {
        Hotel customer1 = new Hotel(1001, "Mohan Kumar");

        customer1.displayRoomsInformation();
        customer1.takeBooking();
        customer1.getMyBookingDetails();
        customer1.getBookingBill();

        customer1.displayRoomsInformation();
        customer1.takeBooking();
        customer1.getMyBookingDetails();
        customer1.getBookingBill();

    }
}