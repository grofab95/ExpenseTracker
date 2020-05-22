using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Enums;
using ExpenseTracker.Statistics.Common;
using System;

namespace ExpenseTracker.Statistics.Managers
{
    public class WeeklyReportManager : ReportManager, IReportManager
    {
        public WeeklyReportManager(StatsDataCollector dataCollector) : base(ReportType.Monthly)
        {
        }

        public void PocessReport()
        {
            throw new NotImplementedException();
        }

        protected override ReportDto GetReportData()
        {
            throw new NotImplementedException();
        }
    }
}
