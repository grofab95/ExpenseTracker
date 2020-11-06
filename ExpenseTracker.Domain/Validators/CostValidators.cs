using ExpenseTracker.Common.Exceptions;
using ExpenseTracker.Domain.Entities;
using System;

namespace ExpenseTracker.Domain.Validators
{
    public class CostValidators
    {
        public static void ValidCostName(CostName costName)
        {
            if (costName == null)
                throw new ArgumentException("Cost name is required.");
        }

        public static void ValidCostValue(decimal costValue)
        {
            if (costValue < 0)
                throw new NegativeCostValue();
        }
    }
}
