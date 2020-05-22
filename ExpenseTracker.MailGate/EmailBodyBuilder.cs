using ExpenseTracker.Common;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Enums;
using System;
using System.IO;
using System.Linq;

namespace ExpenseTracker.MailGate
{
    public class EmailBodyBuilder
    {
        private string _templatesPath;

        public EmailBodyBuilder()
        {
            _templatesPath = Config.Get().MailConfig.TemplatesPath;
        }

        private string GetTemplate(ReportType name)
        {
            var path = $"{_templatesPath}\\{name}Report.html";

            if (!File.Exists(path))
                return null;
            
            return File.ReadAllText(path);
        }

        public string BuildReportEmailBody(ReportDto reportDto)
        {
            var emailBody = GetTemplate(ReportType.Daily);

            var costsList = "<ol>";

            reportDto.Costs.OrderBy(o => o.Category.Name).ToList().ForEach(x =>
            {
                costsList += $"<li> ({x.Category.Name}) -> {x.Name.Value} -> {x.Amount}zł </li>";
            });

            costsList += "</ol>";

            emailBody = emailBody.Replace("[DATE]", reportDto.BeginDate.ToString("dd/MM/yyyy"));
            emailBody = emailBody.Replace("[TOTAL_COST]", $"{reportDto.TotalCost}zł");
            emailBody = emailBody.Replace("[COSTS]", costsList);
            emailBody = emailBody.Replace("[CREATED_AT]", reportDto.CreatedAt.ToString("dd/MM/yyyy HH:mm"));

            return emailBody;
        }
    }
}
