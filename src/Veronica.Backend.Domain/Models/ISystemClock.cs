using System;

namespace Veronica.Backend.Domain.Models
{
    public interface ISystemClock
    {
        DateTimeOffset UtcNow { get; }
    }

    public class SystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}