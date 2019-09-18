using System;

namespace DddExample.Common.Identifiers
{
    public interface IIdentifierGenerationService
    {
        Guid Generate();
    }
}
