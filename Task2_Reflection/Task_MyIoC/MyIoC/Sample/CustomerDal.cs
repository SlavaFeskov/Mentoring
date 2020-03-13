using MyIoC.Attributes;

namespace MyIoC.Sample
{
    [Export(typeof(ICustomerDal))]
    public class CustomerDal : ICustomerDal
    {
    }
}