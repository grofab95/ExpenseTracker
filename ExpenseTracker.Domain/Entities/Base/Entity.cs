using System;

namespace ExpenseTracker.Domain.Entities.Base
{
    public abstract class Entity
    {
        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Entity()
        {
            CreatedAt = DateTime.Now;           
        }
    }
}
