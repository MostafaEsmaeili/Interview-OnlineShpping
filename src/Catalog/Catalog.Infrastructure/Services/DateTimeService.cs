using Catalog.Application.Common.interfaces;
using System;
namespace Catalog.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
