using CitationNeeded.Domain.Interfaces;
using CitationNeeded.Domain.ValueTypes;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace CitationNeeded.Infrastructure.Mail
{
    public class SendGridEmailService : IEmailService
    {
        private readonly IOptions<AppSettings> _appSettings;
        private ISendGridClient _sendGridClient;

        public SendGridEmailService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task SendAsync(Email email)
        {
            InitializeClient();
            var msg = MakeSendGridMessage(email);
            await _sendGridClient.SendEmailAsync(msg);
        }

        private void InitializeClient()
        {
            if (_sendGridClient != null)
                return;

            var apiKey = _appSettings.Value.SendGridApiKey;

            if (apiKey == null)
                throw new InvalidOperationException("No send grid api key set in configuration!");

            _sendGridClient = new SendGridClient(apiKey);
        }

        private static SendGridMessage MakeSendGridMessage(Email email)
        {
            var from = new EmailAddress(email.From);
            var to = new EmailAddress(email.To);

            var sendGridMessage = 
                MailHelper.CreateSingleEmail(from, to, email.Subject, email.Content, email.Content);

            return sendGridMessage;
        }
    }
}
