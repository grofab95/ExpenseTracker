using ExpenseTracker.Domain.Entities;
using System.Collections.Generic;

namespace ExpenseTracker.Domain.Adapters
{
    public interface ICategoryDao
    {
        IEnumerable<Category> GetAll();
        string AddCategory(Category category);
        string AddCategories(List<Category> categories);
    }
}
