using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Domain.Dto
{
    public class ReportDto
    {
        public List<Cost> Costs { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public ReportType ReportType { get; set; }
        public DateTime CreatedAt { get; set; }

        public ReportDto()
        {
            CreatedAt = DateTime.Now;
        }

        public decimal TotalCost
        {
            get
            {
                return Costs.Sum(x => x.Amount);
            }
        }
    }
}
