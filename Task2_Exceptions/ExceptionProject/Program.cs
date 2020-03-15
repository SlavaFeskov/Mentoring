using System;
using ConvertProject;

namespace ExceptionProject
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(Converter.ConvertString("248gdfg28sdg5"));
            }
            catch (StringConverterException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}