
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;

public class HotelRooms
{   
    

    // user side details
    private int _id;
    private string _username;

    private Dictionary<string, int> _user_rooms_bookingDays = new Dictionary<string, int>();
    private List<Dictionary<string, List<string>>> _user_rooms_specialities = new List<Dictionary<string, List<string>>>();


    // System Details
    private int _totalBookingPrice = 0;

    private Dictionary<string, int> _FixedBookingPrice = new Dictionary<string, int>
    {
        {"Gold",4500}, 
        {"Silver",3000},
        {"Bronze",2000}

    };

    
    private List<Dictionary<string, Dictionary<string, int>>> _rooms_speciality_data = new List<Dictionary<string, Dictionary<string, int>>>
    {
        new Dictionary<string, Dictionary<string, int>>
        {
            {
                "Gold", new Dictionary<string, int>
                {
                    {"Ac",1300 },
                    {"Two Beds",2000},
                    {"One Bed",1500},
                    {"None",0}

                }
            }
        },

        new Dictionary<string, Dictionary<string, int>>
        {
            {
                "Silver", new Dictionary<string, int>
                {
                    {"Ac", 1200 },
                    {"Two Beds",1800},
                    {"One Bed",1300},
                    {"None",0}

                }
            }
        },

        new Dictionary<string, Dictionary<string, int>>
        {
            {
                "Silver", new Dictionary<string, int>
                {
                    {"Ac", 1000 },
                    {"Two Beds",1500},
                    {"One Bed",1100},
                    {"None",0}

                }
            }
        }
    };

    public HotelRooms(int id,string username)
    {
        this._id = id;
        this._username = username;
    }

  
    public void provideBookingInformation(List<Dictionary<string, List<string>>> user_room_specialties, Dictionary<string, int> user_rooms_bookingDays)
    {
        foreach (Dictionary<string, List<string>> room_speciality in user_room_specialties)
        {
            foreach (KeyValuePair<string, List<string>> room_details in room_speciality)
            {
                foreach (Dictionary<string, Dictionary<string, int>> room_specialty_data in _rooms_speciality_data)
                {
                    if (room_specialty_data.ContainsKey(room_details.Key))
                    {
                        foreach (string speciality in room_details.Value)
                        {
                            if (room_specialty_data[room_details.Key].ContainsKey(speciality))
                            {
                                _user_rooms_specialities.Add(

                                    new Dictionary<string, List<string>>
                                    {
                                            {
                                                room_details.Key, new List<string>(room_details.Value)
                                            }
                                    }

                                );
                            }
                        }

                        _user_rooms_bookingDays.Add(room_details.Key, user_rooms_bookingDays[room_details.Key]);
                    }
                }
            }
        }

    }  

    public void getBookingBill()
    {
        foreach(Dictionary<string,List<string>> userRoom in _user_rooms_specialities)
        {
            foreach(KeyValuePair<string, List<string>> userRoom_Speciality in userRoom)
            {
                _totalBookingPrice += _FixedBookingPrice[userRoom_Speciality.Key];

                foreach (string speciality in userRoom_Speciality.Value)
                {
                    foreach(Dictionary<string, Dictionary<string, int>> roomData in _rooms_speciality_data)
                    {
                        foreach(KeyValuePair<string,Dictionary<string, int>> specialites_data in roomData)
                        {
                            _totalBookingPrice+=specialites_data.Value[speciality];
                        }
                    }
                }
            }
        }
    }
    
    public void displayUserRoomsBookingInformation()
    {

    }
    public void displayRoomsInformation()
    {
        Console.WriteLine("Room Fixed Charges per day : \n");

        foreach(KeyValuePair<string,int> roomFixedCharges in _FixedBookingPrice)
        {
            Console.WriteLine($"{roomFixedCharges.Key} => {roomFixedCharges.Value} INR\n");

            foreach (Dictionary<string, Dictionary<string, int>> roomData in _rooms_speciality_data)
            {
                Console.WriteLine("Extra Prices for Specialiteis : ");

                foreach (KeyValuePair<string, Dictionary<string, int>> specialites_data in roomData)
                {
                    foreach(KeyValuePair<string,int> SpecialtiyPrice in specialites_data.Value)
                    {
                        Console.WriteLine($"{SpecialtiyPrice.Key} = {SpecialtiyPrice.Value} INR");
                    }
                    
                }
            }

        }

        
    }
                                              

    public static void Main(string[] args)
    {


    
    }
}