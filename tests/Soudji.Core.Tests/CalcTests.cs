using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Suodji.Core;
using Xunit;

namespace Soudji.Core.Tests
{
    public class CalcTests
    {
        /// <summary>
        /// Naming convention MethodName_StateUnderTest_ExpectedBehaviour
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Sum_StateUnderTest_ExpectedBehaviour()
        {
            // Arrange
            var fixture = new Fixture();
            var a = fixture.Create<int>();
            var b = fixture.Create<int>();
            var calc = fixture.Create<Calc>();

            // Act
            var sum = calc.Sum(a,b);

            // Assert
            sum.Should().Be(a + b);
        }
    }
}
