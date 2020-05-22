using System;
using System.Collections.Generic;

namespace ExpenseTracker.Domain.Entities
{
    public class Description
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
