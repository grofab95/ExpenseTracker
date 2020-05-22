using ExpenseTracker.MailGate;
using ExpenseTracker.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ExpenseTracker.Statistics
{
    public class ExpenseAnalyzer
    {
        private ExpenseTrackerContext _context;

        public ExpenseAnalyzer()
        {
            _context = new ExpenseTrackerContext();
        }
    }
}
