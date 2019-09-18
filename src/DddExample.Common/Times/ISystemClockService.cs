using System;

namespace DddExample.Common.Times
{
    public interface ISystemClockService
    {
        DateTimeOffset UtcNow { get; }
    }
}