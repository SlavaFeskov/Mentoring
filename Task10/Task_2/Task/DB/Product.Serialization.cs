using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Task.Services;

namespace Task.DB
{
    public partial class Product : ISerializable
    {
        private Product(SerializationInfo info, StreamingContext context)
        {
            ProductID = info.GetInt32(nameof(ProductID));
            ProductName = info.GetString(nameof(ProductName));
            SupplierID = info.GetInt32(nameof(SupplierID));
            CategoryID = info.GetInt32(nameof(CategoryID));
            QuantityPerUnit = info.GetString(nameof(QuantityPerUnit));
            UnitPrice = info.GetDecimal(nameof(UnitPrice));
            UnitsInStock = info.GetInt16(nameof(UnitsInStock));
            UnitsOnOrder = info.GetInt16(nameof(UnitsOnOrder));
            ReorderLevel = info.GetInt16(nameof(ReorderLevel));
            Discontinued = info.GetBoolean(nameof(Discontinued));
            Category = (Category) info.GetValue(nameof(Category), typeof(Category));
            Order_Details =
                (ICollection<Order_Detail>) info.GetValue(nameof(Order_Details), typeof(ICollection<Order_Detail>));
            Supplier = (Supplier) info.GetValue(nameof(Supplier), typeof(Supplier));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            var dbContext = NorthwindDbContextProvider.Get();

            info.AddValue(nameof(ProductID), ProductID);
            info.AddValue(nameof(ProductName), ProductName);
            info.AddValue(nameof(SupplierID), SupplierID);
            info.AddValue(nameof(CategoryID), CategoryID);
            info.AddValue(nameof(QuantityPerUnit), QuantityPerUnit);
            info.AddValue(nameof(UnitPrice), UnitPrice);
            info.AddValue(nameof(UnitsInStock), UnitsInStock);
            info.AddValue(nameof(UnitsOnOrder), UnitsOnOrder);
            info.AddValue(nameof(ReorderLevel), ReorderLevel);
            info.AddValue(nameof(Discontinued), Discontinued);
            info.AddValue(nameof(Category), dbContext.Categories.Single(c => c.CategoryID == CategoryID));
            info.AddValue(nameof(Order_Details), dbContext.Order_Details.Where(od => od.ProductID == ProductID).ToList());
            info.AddValue(nameof(Supplier), dbContext.Suppliers.Single(s => s.SupplierID == SupplierID));
        }
    }
}