using ExpenseTracker.Domain.Entities;
using System.Collections.Generic;

namespace ExpenseTracker.Domain.Adapters
{
    public interface ICostDao
    {
        IEnumerable<Cost> GetAll();
        bool IsCostExit(Cost cost);
        void AddCost(Cost cost);
    }
}
