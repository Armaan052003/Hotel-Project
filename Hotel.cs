using System;
using System.Collections.Generic;

public class dataBase
{

    public static void Main(string[] args)
    {
        Dictionary<int, int> a = new Dictionary<int, int>();

        a.Add(1, 1);
        a.Add(1, 2);

        foreach(KeyValuePair<int,int> kvp in a)
        {
            Console.WriteLine($"{kvp.Key},{kvp.Value}");
        }
    }
}