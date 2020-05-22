using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Domain.Adapters
{
    public interface IDescriptionDao
    {
        string AddDescription(Description description);
    }
}
