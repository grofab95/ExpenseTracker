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

            var now = DateTime.Now;
            var costs = _context.Costs
                .Where(x => x.CreatedAt.Day == now.Day - 1 &&
                            x.CreatedAt.Month == now.Month &&
                            x.CreatedAt.Year == now.Year)
                .ToList();

            if (costs.Count == 0)
                return null;

            now = now.AddDays(-1);

            return new ReportDto
            {
                Costs = costs,
                BeginDate = new DateTime(now.Year, now.Month, now.Day),
                EndDate = new DateTime(now.Year, now.Month, now.Day + 1),
                ReportType = ReportType.Daily
            };
        }

        public ReportDto CollectWeeklyStats()
        {
            Logger.Log("- weekly costs are collecting");

            var now = DateTime.Now;

            var until = new DateTime(now.Year, now.Month, now.Day);
            var since = until.AddDays(-7);

            var costs = _context.Costs.Where(x => x.CreatedAt >= since && x.CreatedAt < until).OrderByDescending(y => y.Amount).ToList();

            if (costs.Count == 0)
                return null;

            return new ReportDto
            {
                Costs = costs,
                BeginDate = since,
                EndDate = until,
                ReportType = ReportType.Daily
            };
        }

        public ReportDto CollectMonthlyStats()
        {
            Logger.Log("- monthly costs are collecting");

            var now = DateTime.Now;

            var until = new DateTime(now.Year, now.Month, now.Day);
            var since = until.AddMonths(-1);

            var costs = _context.Costs.Where(x => x.CreatedAt >= since && x.CreatedAt < until).ToList();

            if (costs.Count == 0)
                return null;

            return new ReportDto
            {
                Costs = costs,
                BeginDate = since,
                EndDate = until,
                ReportType = ReportType.Daily
            };
        }
    }
}
