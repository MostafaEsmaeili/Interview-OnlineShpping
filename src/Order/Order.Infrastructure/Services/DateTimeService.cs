﻿using Order.Application.Common.interfaces;
using System;
namespace Order.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
