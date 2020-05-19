using System;
using Task.DB;
using Task.Serialization.DataContractSurrogate.Abstractions;

namespace Task.Serialization.DataContractSurrogate
{
    public class EmployeeDataContractSurrogateDecorator : DataContractSurrogateDecorator
    {
        public EmployeeDataContractSurrogateDecorator(Abstractions.DataContractSurrogate surrogate)
            : base(surrogate)
        {
        }

        public override Type GetDataContractType(Type type)
        {
            if (typeof(Employee).IsAssignableFrom(type))
            {
                return typeof(Employee);
            }

            return Surrogate.GetDataContractType(type);
        }

        public override object GetObjectToSerialize(object obj, Type targetType)
        {
            if (obj is Employee employee)
            {
                return new Employee
                {
                    EmployeeID = employee.EmployeeID,
                    LastName = employee.LastName,
                    FirstName = employee.FirstName,
                    Title = employee.Title,
                    TitleOfCourtesy = employee.TitleOfCourtesy,
                    BirthDate = employee.BirthDate,
                    HireDate = employee.HireDate,
                    Address = employee.Address,
                    City = employee.City,
                    Region = employee.Region,
                    PostalCode = employee.PostalCode,
                    Country = employee.Country,
                    HomePhone = employee.HomePhone,
                    Extension = employee.Extension,
                    Photo = employee.Photo,
                    Notes = employee.Notes,
                    ReportsTo = employee.ReportsTo,
                    PhotoPath = employee.PhotoPath
                };
            }

            return Surrogate.GetObjectToSerialize(obj, targetType);
        }
    }
}