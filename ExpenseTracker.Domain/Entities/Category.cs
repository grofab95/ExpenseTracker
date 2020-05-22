using System;
using System.Collections.Generic;

namespace ExpenseTracker.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
