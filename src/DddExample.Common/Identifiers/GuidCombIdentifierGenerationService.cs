using System;

namespace DddExample.Common.Identifiers
{
    public class GuidCombIdentifierGenerationService : IIdentifierGenerationService
    {
        public Guid Generate()
        {
            return GuidComb.Generate();
        }
    }
}