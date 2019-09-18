using DddExample.Common;

namespace DddExample.Domain
{
    public class DomainValidationException : ValidationException
    {
        public DomainValidationException(string message) : base(message)
        {
        }
    }
}