using ExpenseTracker.Common;
using ExpenseTracker.Statistics.Common;
using ExpenseTracker.Statistics.Managers;
using System;
using System.Collections.Generic;

namespace ExpenseTracker.Statistics
{
    public class Program
    {
        static void Main()
        {
            try
            {
                Logger.Log("Statistic module is starting ...");

                var dataCollector = new StatsDataCollector();

                var managers = GetReportManagers(dataCollector);
                StartManagers(managers);
            }
            catch (Exception ex)
            {
                Logger.Log($"[STATISTICS] - {ex.Message}", LogLevel.ERROR);
            }
        }

        private static void StartManagers(List<IReportManager> managers)
        {
            foreach (var manager in managers)
            {
                manager.PocessReport();
            }
        }

        private static List<IReportManager> GetReportManagers(StatsDataCollector dataCollector)
        {
            return new List<IReportManager>
            {
                new DailyReportManager(dataCollector),
                new WeeklyReportManager(dataCollector),
                new MonthlyReportManager(dataCollector)
            };
        }
    }
}
