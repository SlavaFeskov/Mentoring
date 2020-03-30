using IOCContainer.Attributes;

namespace IOCContainer.Sample
{
    [ImportConstructor]
    public class CustomerBll
    {
        public ICustomerDal Dal { get; }
        public Logger Logger { get; }

        public CustomerBll(ICustomerDal dal, Logger logger)
        {
            Dal = dal;
            Logger = logger;
        }
    }

    public class CustomerBll2
    {
        [Import] public ICustomerDal CustomerDal { get; set; }
        [Import] public Logger Logger { get; set; }
    }
}