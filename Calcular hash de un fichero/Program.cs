﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Usamos esta libreria para poder crear el hash
using System.Security.Cryptography;
//Esta libreria permite leer y escribir en archivos
using System.IO;

namespace Calcular_hash_de_un_fichero
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variables
            int variable = -1;
            String fileName;
            String textoPlano;
            String fileHash;
            String textoHash;

            //Hacemos un blucle para que si no pulsamos el 0 no salgamos del programa
            while (variable != 0)
            {
                //Limpiamos la consola por cada vuelta
                Console.Clear();
                //Preguntamos al usuario que quiere hacer 
                Console.WriteLine("************************CALCULAR HASH*****************************\n");
                Console.WriteLine("Pulsa 1 para calcular el hash del documento de texto.");
                Console.WriteLine("Pulsa 2 para comparar los hash y comprobar la integridad.");
                Console.WriteLine("Pulsa 0 para cerrar el programa.\n");
                Console.WriteLine("******************************************************************");

                //Para leer un integer hacer una conversion al mismo tiempo que lo leemos 
                variable = Int32.Parse(Console.ReadLine());             

                if (variable == 1)
                {
                    //Pedimos el nombre del archivo, hay que poner la extension, en este caso es .txt
                    Console.WriteLine("Entra el nombre del archivo, para calcular el hash.");

                    //Hacemos un try por si no encontramos el archivo o lo hemos escrito mal
                    try
                    {
                        //Leemos la respuesta esperando que sea el nombre del fichero
                        fileName = Console.ReadLine();

                        //Leemos los datos del fichero y los ponemos en una variable
                        textoPlano = File.ReadAllText(fileName);
                        //Path.GetDirectoryName(fileName);

                        // Convertimos la variable string, en un array de bytes
                        // EL ERROR QUE TENIA ES QUE AQUI DONDE TEXTOPLANO PUSE FILENAME, POR ESO NO ME IVA
                        byte[] bytesIn = UTF8Encoding.UTF8.GetBytes(textoPlano);

                        // Instanciamos la classe para hacer el hash
                        SHA512Managed SHA512 = new SHA512Managed();

                        // Calculamos hash
                        byte[] hashResult = SHA512.ComputeHash(bytesIn);

                        // Si queremos mostrar el hash por pantalla o guardarlo en un 
                        //fichero de texto hay que convertirlo a un string
                        String textOut = BitConverter.ToString(hashResult, 0);

                        //Avisamos de que se ha creado el hash y preguntamos con que nombre lo quiere guardar
                        Console.WriteLine("\nHash calculado.\nIntroduce el nombre con el que se guarde el hash, recuerda poner '.txt' al final");

                        //Leemos el nombre del archivo
                        fileHash = Console.ReadLine();

                        //guardamos en un fichero el hash y le ponemos el nombre que nos diga el usuario
                        File.WriteAllText(fileHash, textOut);

                        //Mostramos un mensaje para decir que el hash se a guardao correctamente
                        Console.WriteLine("El hash se a guardado corectamente.");

                        // Eliminamos la clase instanciada para liberar memoria
                        SHA512.Dispose();

                        //Hasta que el usuario no pulse una tecla no sale del bucle
                        Console.ReadKey();
                    }
                    catch (Exception)
                    {
                        //Mostramos un mensaje para decir que se ha producido un error en el proceso
                        Console.WriteLine("No se ha encontrado el fichero.");

                        //Hasta que el usuario no pulse una tecla no sale del bucle
                        Console.ReadKey();
                    }
                }

                if (variable == 2)
                {
                    //Le pedimos al usuario que introduzca el nombre del fichero
                    Console.WriteLine("Introduce el nombre del archivo");

                    //Hacemos un try por si no encontramos el archivo o lo hemos escrito mal
                    try
                    {
                        //Leemos la respuesta esperando que sea el nombre del fichero
                        fileName = Console.ReadLine();

                        //Leemos los datos del fichero y los ponemos en una variable
                        textoPlano = File.ReadAllText(fileName);

                        // Convertimos la variable string, en un array de bytes
                        // EL ERROR QUE TENIA ES QUE AQUI DONDE TEXTOPLANO PUSE FILENAME, POR ESO NO ME IVA
                        byte[] bytesIn = UTF8Encoding.UTF8.GetBytes(textoPlano);

                        // Instanciamos la classe para hacer el hash
                        SHA512Managed SHA512 = new SHA512Managed();

                        // Calculamos hash
                        byte[] hashResult = SHA512.ComputeHash(bytesIn);

                        // Si queremos mostrar el hash por pantalla o guardarlo en un 
                        //fichero de texto hay que convertirlo a un string
                        String textOut = BitConverter.ToString(hashResult, 0);

                        //Ahora pedimos el hash
                        Console.WriteLine("\nIntroduce el hash");

                        //Leemos la respuesta esperando que sea el nombre del fichero
                        fileHash = Console.ReadLine();

                        //Leemos los datos del fichero y los ponemos en una variable
                        textoHash = File.ReadAllText(fileHash);

                        //Comparamos los dos hash para ver si el mensaje se a modificado
                        if (textoHash.Equals(textOut))
                        {
                            Console.WriteLine("\nLos hash coinciden, el archivo no a sido modificado");

                            //Hasta que el usuario no pulse una tecla no sale del bucle
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("\nLos hash no coinciden");

                            //Hasta que el usuario no pulse una tecla no sale del bucle
                            Console.ReadKey();

                        }
                        SHA512.Dispose();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("No se ha encontrado el fichero.");

                        //Hasta que el usuario no pulse una tecla no sale del bucle
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}



