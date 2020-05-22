using ExpenseTracker.Domain.Adapters;
using ExpenseTracker.Domain.Entities;
using System;

namespace ExpenseTracker.SQL.Dao
{
    public class DescriptionDao : IDescriptionDao
    {
        private ExpenseTrackerContext _context;

        public DescriptionDao(ExpenseTrackerContext context)
        {
            _context = context;
        }

        public string AddDescription(Description description)
        {
            try
            {
                _context.Descriptions.Add(description);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
