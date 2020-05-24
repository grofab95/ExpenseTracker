using ExpenseTracker.Common;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Enums;
using ExpenseTracker.Statistics.Common;

namespace ExpenseTracker.Statistics.Managers
{
    public class WeeklyReportManager : ReportManager
    {
        private StatsDataCollector _statsDataCollector;

        public WeeklyReportManager(StatsDataCollector dataCollector) : base(ReportType.Weekly)
        {
            _statsDataCollector = dataCollector;
        }

        protected override ReportDto GetReportData()
        {
            Logger.Log("Weekly report is building ...");

            var dailyData = _statsDataCollector.CollectWeeklyStats();

            if (dailyData == null)
            {
                Logger.Log("No weekly costs.");
                return null;
            }

            return dailyData;
        }
    }
}
