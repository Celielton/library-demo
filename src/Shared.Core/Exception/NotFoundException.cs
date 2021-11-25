using Shared.Core.Exception.Interfaces;

namespace Shared.Core.Exception
{
    public class NotFoundException : System.Exception, IApplicationException
    {
        public NotFoundException(string message = "Entity was not found") : base(message) { }
    }
}
