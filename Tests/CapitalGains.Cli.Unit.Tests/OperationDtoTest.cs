using CapitalGains.Cli.Domain.Enums;
using CapitalGains.Cli.Domain.Models;
using FluentAssertions;

namespace CapitalGains.Unit.Tests
{
    public class OperationDtoTest
    {
        [Fact]
        public void Should_Be_Operation_Type_Enum_Buy()
        {
            OperationDto operationDto = new("buy", 15.5m, 200);

            operationDto.Type.Should().Be(OperationTypeEnum.Buy);
        }

        [Fact]
        public void Should_Be_Operation_Type_Enum_Sell()
        {
            OperationDto operationDto = new("sell", 15.5m, 200);

            operationDto.Type.Should().Be(OperationTypeEnum.Sell);
        }

        [Fact]
        public void Should_Be_Create_OperationDto_Correctly()
        {
            OperationDto operationDto = new("buy", 15.5m, 200);

            operationDto.Type.Should().Be(OperationTypeEnum.Buy);
            operationDto.UnitCost.Should().Be(15.5m);
            operationDto.Quantity.Should().Be(200);
        }

        [Fact]
        public void Should_Be_Argument_Exception()
        {
            Action act = () =>
                new OperationDto("selll", 15.5m, 200);

            act.Should().Throw<ArgumentException>();
        }
    }
}
