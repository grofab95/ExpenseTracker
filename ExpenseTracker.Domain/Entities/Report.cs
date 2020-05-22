using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Enums;
using System;
using System.Collections.Generic;

namespace ExpenseTracker.Domain.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public ReportType ReportType { get; set; }
        public List<Cost> Costs { get; set; }
        public DateTime SendAt { get; set; }
        public string Recipient { get; set; }
        public DateTime? CreatedOn { get; set; }

        public Report(ReportDto reportDto)
        {
            ReportType = reportDto.ReportType;
            Costs = reportDto.Costs;
            CreatedOn = reportDto.CreatedOn;
        }

        public Report() { }
    }
}
