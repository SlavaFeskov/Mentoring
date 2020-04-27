using System.Web;
using NorthwindHandler.Models;

namespace NorthwindHandler.Services.Abstractions
{
    public interface IParamsRetriever
    {
        Filter GetParameters(HttpRequest request);
    }
}