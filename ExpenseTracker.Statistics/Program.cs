using ExpenseTracker.Common;
using System;

namespace ExpenseTracker.Statistics
{
    public class Program
    {
        static void Main()
        {
            try
            {
                Logger.Log("Statistics module is running ...");
                var dataCollector = new StatsDataCollector();
                var statsData = dataCollector.CollectDailyStats();

                if (statsData == null)
                {
                    Logger.Log("No current costs.");
                    return;
                }

                var reportBuilder = new ReportBuilder(statsData);
                var emailSubject = $"Dzienny raport wydatków - {statsData.BeginDate.ToString("dd/MM")}";
                var report = reportBuilder.BuildReport(emailSubject);
                reportBuilder.SendReport(report);
            }
            catch (Exception ex)
            {
                Logger.Log($"[STATISTICS] - {ex.Message}", LogLevel.ERROR);
                Logger.Log(ex, true);
            }
        }
    }
}
