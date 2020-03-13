using System;
using ConvertProject;

namespace ExceptionProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Converter.ConvertString("248gdfg28sdg5"));
            while (true)
            {
                var line = Console.ReadLine();
                try
                {
                    Console.WriteLine(line[0]);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Exception occured! Empty string was entered.");
                }
            }
        }
    }
}