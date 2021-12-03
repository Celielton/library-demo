using System;

namespace Shared.Core.Domain.Events
{
    public abstract class BaseEvent
    {
        public TimeSpan Time { get; private set; } = DateTime.UtcNow.TimeOfDay;
    }
}
