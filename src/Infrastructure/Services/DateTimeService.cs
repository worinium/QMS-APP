using QMS_APP.Application.Common.Interfaces;
using System;

namespace QMS_APP.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
