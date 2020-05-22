using ExpenseTracker.Common;
using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Enums;
using ExpenseTracker.SQL;
using System;
using System.Linq;

namespace ExpenseTracker.Statistics
{
    public class StatsDataCollector
    {
        private ExpenseTrackerContext _context;

        public StatsDataCollector()
        {
            _context = new ExpenseTrackerContext();
        }

        public ReportDto CollectDailyStats()
        {
            Logger.Log("- daily costs are collecting");

            var actualDate = DateTime.Now;
            var costs = _context.Costs.Where(x => x.CreatedAt.Day == actualDate.Day - 1).ToList();

            if (costs.Count == 0)
                return null; 

            return new ReportDto
            {
                Costs = costs,
                BeginDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(-1),
                ReportType = ReportType.Daily
            };
        }

        public ReportDto CollectWeeklyStats()
        {
            throw new NotImplementedException();
        }

        public ReportDto CollectMonthlyStats()
        {
            throw new NotImplementedException();
        }
    }
}
