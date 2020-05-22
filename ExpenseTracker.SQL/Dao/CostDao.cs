using ExpenseTracker.Domain.Adapters;
using ExpenseTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.SQL.Dao
{
    public class CostDao : ICostDao
    {
        private ExpenseTrackerContext _context;

        public CostDao(ExpenseTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<Cost> GetAll()
        {
            return _context.Costs;
        }

        public void AddCost(Cost cost)
        {
            try
            {
                if (cost.Name?.Value == null)
                    throw new Exception("Nie wpisano nazwy kosztu.");

                if (cost.Category?.Name == null)
                    throw new Exception("Nie wybrano kategorii.");

                if (cost.Amount < 1)
                    throw new Exception("Nie podano wartości kosztu.");

                _context.Costs.Add(cost);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsCostExit(Cost cost)
        {
            if (cost.Category?.Name == null || cost.Name?.Value == null)
                return false;

            return _context.Costs.Any(x => 
                x.Name.Value.ToLower() == cost.Name.Value.ToLower() && 
                x.Category.Name.ToLower() == cost.Category.Name.ToLower());
        }
    }
}
