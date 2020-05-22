using ExpenseTracker.Domain.Adapters;
using ExpenseTracker.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ExpenseTracker.SQL.Dao
{
    public class CategoryDao : ICategoryDao
    {
        private ExpenseTrackerContext _context;

        public CategoryDao(ExpenseTrackerContext context)
        {
            _context = context;
        }   

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories;
        }

        public string AddCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string AddCategories(List<Category> categories)
        {
            try
            {
                _context.Categories.AddRange(categories);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
