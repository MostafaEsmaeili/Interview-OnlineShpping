using System;

namespace Catalog.Application.Common.exceptions
{
    class CatalogApiExceptions : Exception
    {
        public CatalogApiExceptions(string message) : base(message)
        {
        }

        public CatalogApiExceptions() : base()
        {
        }

        public CatalogApiExceptions(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}