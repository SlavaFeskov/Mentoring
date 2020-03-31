using System.Collections.Generic;

namespace NorthwindDal.Data
{
    public static class OrderQueryData
    {
        public static IEnumerable<string> GetQueryColumns = new List<string>
        {
            "OrderID",
            "OrderDate",
            "RequiredDate",
            "ShippedDate",
            "Freight",
            "ShipName",
            "ShipAddress",
            "ShipCity",
            "ShipRegion",
            "ShipPostalCode",
            "ShipCountry",
            "CustomerID",
            "EmployeeID",
            "ShipVia"
        };
    }
}