using CapitalGains.Cli.Domain.Models;
using FluentAssertions;

namespace CapitalGains.Unit.Tests
{
    public class TaxDtoTest
    {
        [Fact]
        public void Should_Be_Create_TaxDto_Correctly()
        {
            TaxDto taxDto = new(10.5m);
            taxDto.Tax.Should().Be(10.5m);
        }

        [Fact]
        public void Should_Be_Tax_Away_From_Zero()
        {
            TaxDto taxDto = new(0.005m);
            taxDto.Tax.Should().Be(0.01m);
        }
    }
}
