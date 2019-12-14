using FluentAssertions;
using KeepCoding.Web.Business;
using Xunit;

namespace KeepCoding.Web.Tests.Business
{
    public class CameraCategorizerTest
    {
        [Fact]
        public void OnlyDivisibleBy3_Category0()
        {
            var category = CameraCategorizer.Categorize(3);
            category.Should().Be(0);
        }

        [Fact]
        public void OnlyDivisibleBy5_Category1()
        {
            var category = CameraCategorizer.Categorize(5);
            category.Should().Be(1);
        }

        [Fact]
        public void DivisibleBy3And5_Category2()
        {
            var category = CameraCategorizer.Categorize(15);
            category.Should().Be(2);
        }

        [Fact]
        public void NotDivisibleBy3And5_Category3()
        {
            var category = CameraCategorizer.Categorize(2);
            category.Should().Be(3);
        }
    }
}
