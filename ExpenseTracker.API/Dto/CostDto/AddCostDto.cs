using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.API.Dto.CostDto
{
    public class AddCostDto
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public virtual Category Category { get; set; }
        public virtual Description Description { get; set; }
    }
}
