namespace CitationNeeded.Domain.ValueTypes
{
    public class AccountVerification
    {
        public string Id { get; set; }
        public Account Account { get; set; }
        public string VerificationToken { get; set; }
        public bool IsVerified { get; set; }
    }
}
