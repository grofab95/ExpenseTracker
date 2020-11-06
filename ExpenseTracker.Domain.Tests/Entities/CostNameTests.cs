using ExpenseTracker.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace ExpenseTracker.Domain.Tests.Entities
{
    public class CostNameTests
    {
        private CostName _costName;
        public CostNameTests()
        {
            _costName = new CostName("fuel");
        }

        [Fact]
        public void CreatedCostName_Should_Has_Value()
        {
            _costName.Value.Should().NotBeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidValue_For_MissingValue_Throw_ArgumentException(string value)
        {
            FluentActions.Invoking(() => new CostName(value))
               .Should()
               .Throw<ArgumentException>();
        }
    }
}
