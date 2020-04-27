using System.Collections.Generic;
using System.Web;
using NorthwindHandler.Models;

namespace NorthwindHandler.Services.Abstractions
{
    public interface IResponseGenerator
    {
        void GenerateResponse(HttpContext context, IEnumerable<OrderModel> orders);
    }
}