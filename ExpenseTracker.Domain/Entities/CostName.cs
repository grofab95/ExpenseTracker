using ExpenseTracker.Domain.Entities.Base;
using ExpenseTracker.Domain.Validators;

namespace ExpenseTracker.Domain.Entities
{
    public class CostName : Entity
    {
        public string Value { get; private set; }

        public CostName(string value) 
        {
            CostNameValidators.ValidValue(value);

            Value = value;
        }
    }
}
