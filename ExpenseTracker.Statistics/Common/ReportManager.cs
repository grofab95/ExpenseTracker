using ExpenseTracker.Common;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Enums;
using ExpenseTracker.MailGate;
using ExpenseTracker.MailGate.Dto;
using ExpenseTracker.Statistics.Helpers;
using System;

namespace ExpenseTracker.Statistics.Common
{
    public abstract class ReportManager
    {
        private EmailBodyBuilder _emailBodyBuilder;
        private ReportType _reportType;

        protected ReportManager(ReportType reportType)
        {
            _reportType = reportType;
            _emailBodyBuilder = new EmailBodyBuilder();
        }

        protected abstract ReportDto GetReportData();     

        protected bool IsTimeToRun()
        {
            var actualDate = DateTime.Now;

            switch (_reportType)
            {
                case ReportType.Daily:
                    return true;

                case ReportType.Weekly:
                    return DateTime.Now.DayOfWeek == DayOfWeek.Monday;

                case ReportType.Monthly:
                    return actualDate.Day == 1;
            }

            throw new Exception("Report type not implemented.");
        }

        protected EmailFactors BuildReportEmail(ReportDto reportDto)
        {
            var reportName = Translator.ReportTypeToPolish(_reportType);
            var emailBody = _emailBodyBuilder.BuildReportEmailBody(reportDto);

            var reportDate = _reportType == ReportType.Daily
                ? reportDto.BeginDate.ToString("dd/MM")
                : reportDto.BeginDate.ToString("dd/MM") + " - " + reportDto.EndDate.ToString("dd/MM");

            return new EmailFactors
            {
                Recipient = Config.Get.ReportEmailRecipient,
                Subject = $"{reportName} raport wydatków {reportDate}",
                Body = emailBody
            };
        }

        protected void SendReportEmail(EmailFactors email)
        {
            Logger.Log("Report is sending ...");
            var emailService = new EmailService(email);
            emailService.SendEmail();
        }
    }
}
