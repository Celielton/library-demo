using System;

namespace Shared.Core.Domain.Interfaces
{
    public interface IMutableEntity : IEntity
    {
        public int Version { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
