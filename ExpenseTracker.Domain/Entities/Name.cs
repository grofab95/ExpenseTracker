using System;
using System.Collections.Generic;

namespace ExpenseTracker.Domain.Entities
{
    public class Name
    {
        public int Id { get; set; }
        public string Value { get; set; }        
        public virtual ICollection<Cost> Costs { get; set; }
        public DateTime CreatedAt { get; set; }

        public static string Trim()
        {
            throw new NotImplementedException();
        }
    }
}
