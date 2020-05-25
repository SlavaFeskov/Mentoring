namespace Task.Serialization.DataContractSurrogate.Abstractions
{
    public abstract class DataContractSurrogateDecorator : DataContractSurrogate
    {
        protected readonly DataContractSurrogate Surrogate;

        protected DataContractSurrogateDecorator(DataContractSurrogate surrogate)
            : base(surrogate.DbContext)
        {
            Surrogate = surrogate;
        }
    }
}