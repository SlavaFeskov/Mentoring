using System.Reflection;

namespace IOCContainer.Sample
{
    public class Program
    {
        public void Sample()
        {
            var container = new Container();
            container.AddAssembly(Assembly.GetExecutingAssembly());

            var customerBLL = (CustomerBll) container.CreateInstance(typeof(CustomerBll));
            var customerBLL2 = container.CreateInstance<CustomerBll>();

            container.RegisterTypeAsSelf(typeof(CustomerBll));
            container.RegisterTypeAsSelf(typeof(Logger));
            container.RegisterType(typeof(CustomerDal), typeof(ICustomerDal));
        }
    }
}