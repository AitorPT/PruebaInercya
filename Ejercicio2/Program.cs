using System;
using System.Collections.Generic;
using System.Diagnostics;  
using System.IO;

class Program
{
    static void Main()
    {
        int count = 5000000;
        HashSet<int> numbers = new HashSet<int>();
        Random random = new Random();
        string filePath = "numeros_random2.txt";

        Stopwatch stopwatch = new Stopwatch(); 
        stopwatch.Start(); 

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            while (numbers.Count < count)
            {
                int num = random.Next(int.MinValue, int.MaxValue);
                if (numbers.Add(num))
                {
                    writer.WriteLine(num);
                }
            }
        }

        stopwatch.Stop();
        Console.WriteLine($"Tiempo de ejecución: {stopwatch.ElapsedMilliseconds / 1000.0} segundos");
        Console.WriteLine("Bien.");
    }
}
