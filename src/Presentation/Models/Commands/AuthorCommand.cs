using Shared.Core.API.Commands;
using System;

namespace library_api.Models.Commands
{
    public class AuthorCommand : ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
