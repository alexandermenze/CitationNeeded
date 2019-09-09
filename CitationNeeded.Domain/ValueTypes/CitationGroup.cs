using System.Collections.Generic;

namespace CitationNeeded.Domain.ValueTypes
{
    public class CitationGroup
    {
        public IEnumerable<Citation> Citations { get; set; }
    }
}
