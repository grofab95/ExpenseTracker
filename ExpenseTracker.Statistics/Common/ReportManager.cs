using ExpenseTracker.Common;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Enums;
using ExpenseTracker.MailGate;
using ExpenseTracker.MailGate.Dto;
using System;

namespace ExpenseTracker.Statistics.Common
{
    public abstract class ReportManager : IReportManager
    {
        private EmailBodyBuilder _emailBodyBuilder;
        private ReportType _reportType;

        protected ReportManager(ReportType reportType)
        {
            _reportType = reportType;
            _emailBodyBuilder = new EmailBodyBuilder();
        }

        public void PocessReport()
        {
            if (!IsTimeToRun())
                return;

            var data = GetReportData();
            if (data == null)
                return;

            var email = BuildReportEmail(data);

            SendReportEmail(email);
        }

        protected abstract ReportDto GetReportData();     

        private bool IsTimeToRun()
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

        private EmailFactors BuildReportEmail(ReportDto reportDto)
        {
            var reportName = Translator.ReportTypeToPolish(_reportType);
            var emailBody = _emailBodyBuilder.BuildReportEmailBody(reportDto, _reportType);

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

        private void SendReportEmail(EmailFactors email)
        {
            Logger.Log("Report is sending ...");
            var emailService = new EmailService(email);
            emailService.SendEmail();
        }
    }
}
