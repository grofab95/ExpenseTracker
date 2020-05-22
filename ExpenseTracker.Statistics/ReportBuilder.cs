using ExpenseTracker.Common;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.MailGate;
using ExpenseTracker.MailGate.Dto;

namespace ExpenseTracker.Statistics
{
    public class ReportBuilder
    {
        private ReportDto _reportDto;
        private EmailBodyBuilder _emailBodyBuilder;

        public ReportBuilder(ReportDto statsDto)
        {
            _reportDto = statsDto;
            _emailBodyBuilder = new EmailBodyBuilder();
        }

        public EmailFactors BuildReport(string subject)
        {
            Logger.Log("Report is building ...");

            var emailBody = _emailBodyBuilder.BuildReportEmailBody(_reportDto);
            return new EmailFactors
            {
                Recipient = "grochla95@gmail.com",
                Subject = subject,
                Body = emailBody
            };
        }

        public void SendReport(EmailFactors emailContent)
        {
            Logger.Log("Report is sending ...");
            var emailService = new EmailService(emailContent);
            var result = emailService.SendEmail();
        }
    }
}
