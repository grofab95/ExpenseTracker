using ExpenseTracker.Common;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Enums;
using ExpenseTracker.Statistics.Common;

namespace ExpenseTracker.Statistics.Managers
{
    public class MonthlyReportManager : ReportManager
    {
        private StatsDataCollector _statsDataCollector;

        public MonthlyReportManager(StatsDataCollector dataCollector) : base(ReportType.Monthly)
        {
            _statsDataCollector = dataCollector;
        }

        protected override ReportDto GetReportData()
        {
            Logger.Log("Monthly report is building ...");

            var dailyData = _statsDataCollector.CollectMonthlyStats();

            if (dailyData == null)
            {
                Logger.Log("No monthly costs.");
                return null;
            }

            return dailyData;
        }
    }
}
