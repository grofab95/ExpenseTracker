using ExpenseTracker.Domain.Entities.Base;
using ExpenseTracker.Domain.Validators;

namespace ExpenseTracker.Domain.Entities
{
    public class Cost : Entity
    {
        public CostName CostName { get; private set; }
        public decimal CostValue { get; private set; }        

        public Cost(CostName costName, decimal costValue) 
        {
            CostValidators.ValidCostName(costName);
            CostValidators.ValidCostValue(costValue);

            CostName = costName;
            CostValue = costValue;
        }
    }
}
