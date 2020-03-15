using System;
using MyIoC.Attributes;

namespace MyIoC.Sample
{
    [Export(typeof(ICustomerDal))]
    public class CustomerDal : ICustomerDal
    {
        public void Print()
        {
            Console.WriteLine("CUSTOMER DAL INTERFACE!");
        }
    }
}