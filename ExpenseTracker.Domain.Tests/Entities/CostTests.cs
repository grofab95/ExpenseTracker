using ExpenseTracker.Common.Exceptions;
using ExpenseTracker.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace ExpenseTracker.Domain.Tests.Entities
{
    public class CostTests
    {
        private Cost _cost;
        private CostName _costName;

        public CostTests()
        {
            _costName = new CostName("food");
            _cost = new Cost(_costName, 1);
        }

        [Fact]
        public void CreatedCost_Should_HasCostName()
        {
            _cost.CostName.Should().NotBe(null);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void CreatedCost_Should_HasCostValue(decimal costValue)
        {
            var cost = new Cost(_costName, costValue);
            cost.CostValue.Should().Be(costValue);
        }

        [Fact]
        public void ValidCostName_For_MissingCostName_Throw_ArgumentException()
        {
            FluentActions.Invoking(() => new Cost(null, 1))
              .Should()
              .Throw<ArgumentException>()
              .WithMessage("Cost name is required.");
        }

        [Fact]
        public void ValidCostValue_For_NegativeNumber_Throw_NegativeCostValue()
        {
            FluentActions.Invoking(() => new Cost(_costName, -1))
              .Should()
              .Throw<NegativeCostValue>();
        }
    }
}
