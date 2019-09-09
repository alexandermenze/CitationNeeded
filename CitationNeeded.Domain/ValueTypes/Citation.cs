namespace CitationNeeded.Domain.ValueTypes
{
    public class Citation
    {
        public string Id { get; set; }
        public User Speaker { get; set; }
        public string Text { get; set; }
    }
}
