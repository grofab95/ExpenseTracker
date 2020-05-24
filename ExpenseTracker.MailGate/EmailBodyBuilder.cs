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
            _templatesPath = Config.Get.MailConfig.TemplatesPath;
        }

        private string GetCostEmailTemplate()
        {
            var path = $"{_templatesPath}\\CostReport.html";

            if (!File.Exists(path))
                throw new Exception("Cost email template not found");
            
            return File.ReadAllText(path);
        }

        public string BuildReportEmailBody(ReportDto reportDto, ReportType reportType)
        {
            var emailBody = GetCostEmailTemplate();
            var period = reportType == ReportType.Daily
                ? $"Wydatki wygenerowane w dniu {reportDto.BeginDate.ToString("dd/MM/yyyy")}"
                : $"Wydatki wygenerowane w okresie <br /> od {reportDto.BeginDate.ToString("dd/MM/yyyy")} do {reportDto.EndDate.ToString("dd/MM/yyyy")}";

            var positions = reportDto.Costs
                .OrderByDescending(o => o.Amount)
                .GroupBy(x => new { x.FullName, x.Amount })
                .Select(y => new
                {
                    Category = y.Select(q => q.Category).First(),
                    Name = y.Select(q => q.Name).First(),
                    FullName = y.Select(q => q.FullName).First(),
                    Cost = y.Key.Amount,
                    Quantity = y.Count(),
                    Total = y.Sum(q => q.Amount)
                })
                .OrderByDescending(o => o.Total)
                .ToList();

            var costsTable = "<table style=\"border: 1px solid black; width:100%\">";
            costsTable += "<tr>";
            costsTable += "<th style=\"border: 1px solid black\">LP.</th>";
            costsTable += "<th style=\"border: 1px solid black\">NAZWA</th>";
            costsTable += "<th style=\"border: 1px solid black\">KATEGORIA</th>";
            costsTable += "<th style=\"border: 1px solid black\">WARTOŚĆ</th>";
            costsTable += "<th style=\"border: 1px solid black\">ILOŚĆ</th>";
            costsTable += "<th style=\"border: 1px solid black\">SUMA</th>";
            costsTable += "</tr>";
            foreach (var pos in positions)
            {
                costsTable += "<tr>";
                costsTable += $"<th style=\"border: 1px solid black\">{positions.IndexOf(pos)+1}.</th>";
                costsTable += $"<th style=\"border: 1px solid black\">{pos.Name.Value}</th>";
                costsTable += $"<th style=\"border: 1px solid black\">{pos.Category.Name}</th>";
                costsTable += $"<th style=\"border: 1px solid black\">{pos.Cost}zł</th>";
                costsTable += $"<th style=\"border: 1px solid black\">{pos.Quantity}</th>";
                costsTable += $"<th style=\"border: 1px solid black\">{pos.Total}zł</th>";
                costsTable += "</tr>";
            }
            costsTable += "</table>";


            emailBody = emailBody.Replace("[REPORT_TYPE]", Translator.ReportTypeToPolish(reportType).ToLower() + ("ch"));
            emailBody = emailBody.Replace("[PERIOD]", period);
            emailBody = emailBody.Replace("[TOTAL_COST]", $"{reportDto.TotalCost}zł");
            emailBody = emailBody.Replace("[COSTS]", costsTable);
            emailBody = emailBody.Replace("[CREATED_AT]", reportDto.CreatedOn.ToString("dd/MM/yyyy HH:mm"));

            return emailBody;
        }
    }
}
