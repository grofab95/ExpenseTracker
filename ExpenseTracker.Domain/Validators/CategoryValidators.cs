using BookStore.Common.Extensions;
using System;

namespace ExpenseTracker.Domain.Validators
{
    public class CategoryValidators
    {
        public static void ValidateName(string name)
        {
            if (name.IsNotExist())
                throw new ArgumentException("Name is required.");
        }
    }
}
