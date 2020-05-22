using ExpenseTracker.Common;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Enums;
using ExpenseTracker.SQL;
using ExpenseTracker.Statistics.Common;

namespace ExpenseTracker.Statistics.Managers
{
    public class DailyReportManager : ReportManager, IReportManager
    {
        private StatsDataCollector _statsDataCollector;

        public DailyReportManager(StatsDataCollector dataCollector) : base(ReportType.Daily)
        {
            _statsDataCollector = dataCollector;
        }

        public void PocessReport()
        {
            var data = GetReportData();
            if (data == null)
                return;

            var email = BuildReportEmail(data);

            SendReportEmail(email);
        }

        protected override ReportDto GetReportData()
        {
            Logger.Log("Report is building ...");

            var dailyData = _statsDataCollector.CollectDailyStats();

            if (dailyData == null)
            {
                Logger.Log("No current costs.");
                return null;
            }

            return dailyData;
        }
    }
}
