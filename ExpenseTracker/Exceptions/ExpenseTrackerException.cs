using System;

namespace ExpenseTracker.Common.Exceptions
{
    public abstract class ExpenseTrackerException : Exception
    {
        public ExpenseTrackerException(string message) : base(message)
        { }
    }
}
