namespace CitationNeeded.Domain.ValueTypes
{
    public class Account
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
    }
}
