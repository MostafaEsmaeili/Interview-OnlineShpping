using System;

namespace Order.Application.Common.exceptions
{
    public class OrderApiExceptions : Exception
    {
        public OrderApiExceptions(string message) : base(message)
        {
        }

        public OrderApiExceptions() : base()
        {
        }

        public OrderApiExceptions(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}