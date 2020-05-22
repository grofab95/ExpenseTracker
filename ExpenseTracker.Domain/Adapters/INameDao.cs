using ExpenseTracker.Domain.Entities;
using System.Collections.Generic;

namespace ExpenseTracker.Domain.Adapters
{
    public interface INameDao
    {
        string AddNames(List<Name> names);
        IEnumerable<Name> GetAll();
    }
}
