using ExpenseTracker.Domain.Entities.Base;
using FluentAssertions;
using System;
using Xunit;

namespace ExpenseTracker.Domain.Tests.Entities
{
    public class CategoryTests
    {
        private Category _category;

        public CategoryTests()
        {
            _category = new Category("Car");
        }

        [Fact]
        public void CreatedCategory_Should_Has_Name()
        {
            _category.Name.Should().NotBeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidCategory_For_MissingName_Throw_ArgumentException(string name)
        {
            FluentActions.Invoking(() => new Category(name))
             .Should()
             .Throw<ArgumentException>()
             .WithMessage("Name is required.");
        }
    }
}
