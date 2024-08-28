using System;
using System.Collections.Generic;
using System.Diagnostics;  
using System.IO;

class Program
{
    static void Main()
    {
        int count = 5000000;
        
        //Crea un conjunto para almacenar numeros únicos
        HashSet<int> numbers = new HashSet<int>();
        
        Random random = new Random();
        
        //Ruta del archivo donde se guardarán los números
        string filePath = "numeros_random2.txt";

        //Crea una instancia de Stopwatch para medir el tiempo de ejecución
        Stopwatch stopwatch = new Stopwatch(); 
        stopwatch.Start(); 

        //Abrir un StreamWriter para escribir en el archivo
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            //Genera numeros
            while (numbers.Count < count)
            {
                int num = random.Next(int.MinValue, int.MaxValue);
                
                //Añade el número al conjunto y verificar si se añadió 
                if (numbers.Add(num))
                {
                    //Escribe el número en el archivo
                    writer.WriteLine(num);
                }
            }
        }

        stopwatch.Stop();
        
        Console.WriteLine($"Tiempo de ejecución: {stopwatch.ElapsedMilliseconds / 1000.0} segundos");
        Console.WriteLine("Bien.");
    }
}
