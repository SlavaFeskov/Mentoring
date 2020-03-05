using System;

namespace NetStandardCL
{
    public static class Shared
    {
        public static string GetMessage(string name) => $"{DateTime.Now.ToLongTimeString()} Hello, {name}!";
    }
}
