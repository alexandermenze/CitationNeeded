using System;
using System.Collections.Generic;

namespace CitationNeeded.Domain.ValueTypes
{
    public class CitationGroup
    {
        public string Id { get; set; }
        public Account Author { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<Citation> Citations { get; set; }
    }
}
