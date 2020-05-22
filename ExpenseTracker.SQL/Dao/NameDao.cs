using ExpenseTracker.Domain.Adapters;
using ExpenseTracker.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ExpenseTracker.SQL.Dao
{
    public class NameDao : INameDao
    {
        private ExpenseTrackerContext _context;

        public NameDao(ExpenseTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<Name> GetAll()
        {
            return _context.Names;
        }

        public string AddNames(List<Name> names)
        {
            try
            {
                _context.Names.AddRange(names);
                _context.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
