using CitationNeeded.Domain.Interfaces;
using CitationNeeded.Domain.ValueTypes;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace CitationNeeded.Infrastructure.Mail
{
    public class SendGridEmailService : IEmailService
    {
        private const string SendGridApiKeyEnvironmentVariable = "SENDGRID_APIKEY";

        private ISendGridClient _sendGridClient;

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

            var apiKey = Environment.GetEnvironmentVariable(
                SendGridApiKeyEnvironmentVariable, EnvironmentVariableTarget.Machine);

            if (apiKey == null)
                throw new InvalidOperationException("No send grid api key set in environment variables!");

            _sendGridClient = new SendGridClient(apiKey);
        }

        private static SendGridMessage MakeSendGridMessage(Email email)
        {
            var from = new EmailAddress(email.From);
            var to = new EmailAddress(email.To);

            var sendGridMessage = 
                MailHelper.CreateSingleEmail(from, to, email.Subject, email.Text, email.Html);

            return sendGridMessage;
        }
    }
}
