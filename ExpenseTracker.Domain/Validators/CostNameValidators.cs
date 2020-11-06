using BookStore.Common.Extensions;
using System;

namespace ExpenseTracker.Domain.Validators
{
    public class CostNameValidators
    {
        public static void ValidValue(string value)
        {
            if (value.IsNotExist())
                throw new ArgumentException("Value is required.");
        }
    }
}
