using System;
using System.Linq;

namespace ExpenseTracker.Domain.Entities
{
    public class Cost
    {
        public int Id { get; set; }
        public virtual Name Name { get; set; }
        public decimal Amount { get; set; }
        public int NumberOfUses { get; set; }
        public virtual Category Category { get; set; }
        public virtual Description Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public string FullName
        {
            get
            {
                return GetFullName();
            }
        }

        public string GetFullName()
        {
            var fullName = $"({Category?.Name}) {Name?.Value}";
            fullName = fullName.Count() > 27
                ? fullName.Substring(0, 24) + "..."
                : fullName;

            return fullName;
        }

        public Cost(Cost cost)
        {
            Name = cost.Name;
            Amount = cost.Amount;
            Category = cost.Category;
            Description = cost.Description;
        }

        public Cost() { }
    }
}
