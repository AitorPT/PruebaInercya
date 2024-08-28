using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace BulkCopyExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Cadena de conexión 
            string connectionString = "Server=localhost;Database=CustomerDB;Trusted_Connection=True;";

            
            string csvFilePath = "Customers.csv";

            //Crea una tabla de datos en memoria
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(string));  
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Address", typeof(string));
            dataTable.Columns.Add("City", typeof(string));
            dataTable.Columns.Add("Country", typeof(string));
            dataTable.Columns.Add("PostalCode", typeof(string));
            dataTable.Columns.Add("Phone", typeof(string));

            //Lee el archivo CSV y carga los datos en la tabla de datos
            using (var reader = new StreamReader(csvFilePath))
            {
                //Lee la primera línea 
                var headers = reader.ReadLine().Split(';');
                if (headers.Length != dataTable.Columns.Count)
                {
                    Console.WriteLine("El archivo CSV tiene un formato incorrecto.");
                    return;
                }

                //Lee el archivo CSV línea por línea
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    if (values.Length != dataTable.Columns.Count)
                    {
                        Console.WriteLine("La línea del archivo CSV tiene un formato incorrecto: " + line);
                        continue;
                    }

                    //Añade la fila a la tabla de datos
                    dataTable.Rows.Add(
                        values[0],  //Id
                        values[1],  //Name
                        values[2],  //Address
                        values[3],  //City
                        values[4],  //Country
                        values[5],  //PostalCode
                        values[6]   //Phone
                    );
                }
            }

            //Usa SqlBulkCopy para cargar los datos en la base de datos
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "Customers";
                    try
                    {
                        bulkCopy.WriteToServer(dataTable);
                        Console.WriteLine("Datos cargados correctamente en la tabla 'Customers'.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al cargar los datos: " + ex.Message);
                    }
                }
            }
        }
    }
}
