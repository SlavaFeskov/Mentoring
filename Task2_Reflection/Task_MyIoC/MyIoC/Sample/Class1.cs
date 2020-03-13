using MyIoC.Attributes;

namespace MyIoC.Sample
{
	[ImportConstructor]
	public class CustomerBll
	{
		public CustomerBll(ICustomerDal dal, Logger logger)
		{ }
	}

	public class CustomerBll2
	{
		[Import]
		public ICustomerDal CustomerDal { get; set; }
		[Import]
		public Logger Logger { get; set; }
	}
}
