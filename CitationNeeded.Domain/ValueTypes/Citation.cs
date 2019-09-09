using System;

namespace CitationNeeded.Domain.ValueTypes
{
    public class Citation
    {
        public User Author { get; set; }
        public User Speaker { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
    }
}
