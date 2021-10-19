using System;

namespace Shared.Core.Domain.Interfaces
{
    public interface IEntity
    {
        public Guid Id { get; }
        public DateTime CreatedDate { get; }
    }
}
