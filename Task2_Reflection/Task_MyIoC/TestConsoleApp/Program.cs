using System;
using System.Linq;
using IOCContainer;
using IOCContainer.Exceptions;
using IOCContainer.Sample;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            var assembly = AppDomain.CurrentDomain.GetAssemblies().Single(a => a.FullName.Contains("IOCContainer"));
            //container.AddAssembly(assembly);
            container.RegisterTypeAsSelf(typeof(CustomerBll));
            //container.RegisterTypeAsSelf(typeof(Logger));
            container.RegisterType(typeof(CustomerDal), typeof(ICustomerDal));
            try
            {
                var customerBLL = (CustomerBll)container.CreateInstance(typeof(CustomerBll));
                var customerBLL2 = container.CreateInstance<CustomerBll>();
                var customerDal = container.CreateInstance<ICustomerDal>();

                customerDal.Print();
                customerBLL.Dal.Print();
                customerBLL2.Logger.Log();
            }
            catch (DependenciesNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (TypeNotRegisteredException e)
            {
                Console.WriteLine(e.Message);
            }
            

            Console.ReadKey();
        }
    }
}