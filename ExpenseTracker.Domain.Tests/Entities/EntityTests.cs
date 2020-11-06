using ExpenseTracker.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace ExpenseTracker.Domain.Tests.Entities
{
    public class EntityTests
    {
        private CostName _costName;

        public EntityTests()
        {
            _costName = new CostName("fuel");
        }

        [Fact]
        public void CreatedCost_Should_HasCreatedDate()
        {
            _costName.CreatedAt.Should().NotBe(default);
        }
    }
}
