using Shared.Core.Domain.Interfaces;
using System;

namespace Shared.Core.Domain.Entities
{
    public class Entity : IEntity
    {
        public Entity()
        {
            CreatedDate = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }

        public DateTime CreatedDate { get; private set; }
    }
}
