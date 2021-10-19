using Shared.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace library_api.Models
{
    public class Author : Entity
    {
        public Author()
        {
            Books = new List<Book>();
        }

        public Author(string firstName, string lastName, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public string FirstName { get; set; }   
        public string LastName { get; set; }        
        public DateTime BirthDate { get; set; } 
        public virtual ICollection<Book> Books { get; set; }
    }
}