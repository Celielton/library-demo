using Shared.Core.Domain.Interfaces;
using System;

namespace Shared.Core.Domain.Entities
{
    public class MutableEntity : Entity, IMutableEntity
    {
        public int Version { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
