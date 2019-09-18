using System;

namespace DddExample.Common.Times
{
    public class StandardSystemClockService : ISystemClockService
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}