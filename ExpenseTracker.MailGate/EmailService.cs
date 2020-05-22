using ExpenseTracker.Common;
using ExpenseTracker.MailGate.Dto;
using System;
using System.Net;
using System.Net.Mail;

namespace ExpenseTracker.MailGate
{
    public class EmailService
    {
        private EmailFactors _emailFactors;

        public EmailService(EmailFactors emailFactors)
        {
            _emailFactors = emailFactors;
        }

        public EmailResult SendEmail()
        {
            try
            {
                var config = Config.Get;
                var fromAddress = new MailAddress(config.MailConfig.Email, "Expense Tracker");
                var toAddress = new MailAddress(_emailFactors.Recipient, "To Name");

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(config.MailConfig.Email, config.MailConfig.Password)
                };

                var message = new MailMessage(fromAddress, toAddress);

                message.Subject = _emailFactors.Subject;
                message.Body = _emailFactors.Body;
                message.IsBodyHtml = true;

                Logger.Log($"Sending e-mail to {toAddress.Address} ...");

                smtp.Send(message);
                message.Dispose();

                Logger.Log("...Done");

                return new EmailResult
                {
                    IsError = false,
                    Message = "Ok"
                };
            }
            catch (Exception ex)
            {
                Logger.Log<EmailService>(ex);

                return new EmailResult
                {
                    IsError = true,
                    Message = "Błąd podczas wysyłania wiadomości."
                };
            }
        }
    }
}
