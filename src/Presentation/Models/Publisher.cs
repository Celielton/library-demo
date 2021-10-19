using Shared.Core.Domain.Entities;
using System.Collections.Generic;

namespace library_api.Models
{
    public class Publisher : Entity
    {
        public Publisher()
        {
            Books = new List<Book>();
        }

        public Publisher(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }   
        public string Description { get; set; }
        public virtual ICollection<Book> Books { get; set; }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}