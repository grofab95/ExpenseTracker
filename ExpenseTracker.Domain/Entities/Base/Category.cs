using ExpenseTracker.Domain.Validators;

namespace ExpenseTracker.Domain.Entities.Base
{
    public class Category
    {
        public string Name { get; private set; }

        public Category(string name)
        {
            CategoryValidators.ValidateName(name);

            Name = name;
        }
    }
}
