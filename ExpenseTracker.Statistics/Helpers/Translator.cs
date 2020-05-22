using ExpenseTracker.Domain.Enums;
using System;

namespace ExpenseTracker.Statistics.Helpers
{
    public class Translator
    {
        public static string ReportTypeToPolish(ReportType reportType)
        {
            switch (reportType)
            {
                case ReportType.Daily:
                    return "Dzienny";

                case ReportType.Weekly:
                    return "Tygodniowy";

                case ReportType.Monthly:
                    return "Miesięczny";

                default:
                    break;
            }

            throw new Exception("Report translate not implemented.");
        }
    }
}
