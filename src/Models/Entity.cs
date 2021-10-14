using System;

namespace library_api.Models 
{
    public abstract class Entity 
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; } 
    }
}