using System.Reflection;
using MyIoC;
using MyIoC.Attributes;
using MyIoC.Sample;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            var t = typeof(CustomerDal).GetCustomAttribute<ExportAttribute>();
            container.AddAssembly(Assembly.LoadFile(@"D:\Mentoring\Task2_Reflection\Task_MyIoC\MyIoC\bin\Debug\MyIoC.dll"));

            var customerBLL = (CustomerBll)container.CreateInstance(typeof(CustomerBll));
            var customerBLL2 = container.CreateInstance<CustomerBll>();

            container.RegisterTypeAsSelf(typeof(CustomerBll));
            container.RegisterTypeAsSelf(typeof(Logger));
            container.RegisterType(typeof(CustomerDal), typeof(ICustomerDal));
        }
    }
}