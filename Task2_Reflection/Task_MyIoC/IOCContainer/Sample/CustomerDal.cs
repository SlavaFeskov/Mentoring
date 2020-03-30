using System;
using IOCContainer.Attributes;

namespace IOCContainer.Sample
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