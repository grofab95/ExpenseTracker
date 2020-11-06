using ExpenseTracker.Common.Exceptions;

namespace ExpenseTracker.Common.Exceptions
{
    public class NegativeCostValue : ExpenseTrackerException
    {
        private const string message =
            "Cost value must be positive.";

        public NegativeCostValue() : base(message)
        { }
    }
}
