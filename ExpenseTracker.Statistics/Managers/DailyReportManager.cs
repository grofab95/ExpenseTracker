using ExpenseTracker.Common;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Enums;
using ExpenseTracker.Statistics.Common;

namespace ExpenseTracker.Statistics.Managers
{
    public class DailyReportManager : ReportManager
    {
        private StatsDataCollector _statsDataCollector;

        public DailyReportManager(StatsDataCollector dataCollector) : base(ReportType.Daily)
        {
            _statsDataCollector = dataCollector;
        }

        protected override ReportDto GetReportData()
        {
            Logger.Log("Daily report is building ...");

            var dailyData = _statsDataCollector.CollectDailyStats();

            if (dailyData == null)
            {
                Logger.Log("No daily costs.");
                return null;
            }

            return dailyData;
        }
    }
}
