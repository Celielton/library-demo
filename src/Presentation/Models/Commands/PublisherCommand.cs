using Shared.Core.API.Commands;

namespace library_api.Models.Commands
{
    public class PublisherCommand : ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
