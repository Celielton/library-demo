using library_api.Models.Enums;
using Shared.Core.API.Commands;
using System;

namespace library_api.Models.Commands
{
    public class BookCommand : ICommand
    {
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        public Guid PublisherId { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ISNB { get; set; }
        public Language Language { get; set; }
    }
}
