using CitationNeeded.Domain.Interfaces;
using CitationNeeded.Domain.ValueTypes;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CitationNeeded.WebApp.Services
{
    public class ConsoleEmailService : IEmailService
    {
        private readonly ILogger<IEmailService> _logger;

        public ConsoleEmailService(ILogger<IEmailService> logger)
        {
            _logger = logger;
        }

        public Task SendAsync(Email email)
        {
            _logger.LogInformation(FormatEmail(email));
            return Task.CompletedTask;
        }

        private string FormatEmail(Email email)
        {
            return $"From: {email.From}, To: {email.To}, Subject: {email.Subject}, Content: {email.Content}";
        }
    }
}
