using CapitalGains.Cli.Application.Services;
using CapitalGains.Cli.Domain.Models;
using CapitalGains.Cli.Domain.Policies;
using FluentAssertions;

namespace CapitalGains.Unit.Tests
{
    public class CapitalGainsServiceTest
    {
        private readonly CapitalGainsService _service;

        public CapitalGainsServiceTest()
        {
            _service = new CapitalGainsService();
        }

        [Fact]
        public void Should_Not_Tax_When_Operation_Is_Buy()
        {
            List<OperationDto> operations = new()
            {
                new("buy", 10m, 100)
            };

            List<TaxDto> result = _service.Process(operations);

            result[0].Tax.Should().Be(0m);
        }

        [Fact]
        public void Should_Not_Tax_When_Value_Operation_Is_Less_Than_Operation_Limit_Value()
        {
            List<OperationDto> operations = new()
            {
                new("sell", 10m, 100)
            };

            List<TaxDto> result = _service.Process(operations);

            result[0].Tax.Should().Be(0m);
        }

        [Fact]
        public void Should_Not_Tax_When_Value_Operation_Is_Equal_To_Operation_Limit_Value()
        {
            List<OperationDto> operations = new()
            {
                new("sell", 20m, 1000)
            };

            List<TaxDto> result = _service.Process(operations);

            result[0].Tax.Should().Be(0m);
        }

        [Fact]
        public void Should_Not_Tax_When_Value_Operation_Results_In_A_Loss()
        {
            List<OperationDto> operations = new()
            {
                new("buy", 20m, 1000),
                new("sell", 10m, 1000)
            };

            List<TaxDto> result = _service.Process(operations);

            result[0].Tax.Should().Be(0m);
            result[1].Tax.Should().Be(0m);
        }

        [Fact]
        public void Should_Be_Tax_When_Value_Operation_Results_In_A_Profit()
        {
            List<OperationDto> operations = new()
            {
                new("sell", 100m, 1000)
            };

            List<TaxDto> result = _service.Process(operations);

            result[0].Tax.Should().Be(20000m);
        }

        [Fact]
        public void Should_Be_Tax_Correct_According_To_Tax_Rate()
        {
            List<OperationDto> operations = new()
            {
                new("sell", 100m, 1000)
            };

            var operationValue = operations[0].UnitCost * operations[0].Quantity;
            var tax = operationValue * TaxRules.TaxRate;

            List<TaxDto> result = _service.Process(operations);

            result[0].Tax.Should().Be(tax);
        }

        [Fact]
        public void Should_Be_Length_Of_Response_Equal_Length_Operation()
        {
            List<OperationDto> operations = new()
            {
                new("sell", 100m, 1000),
                new("sell", 100m, 1000),
                new("sell", 100m, 1000),
                new("sell", 100m, 1000),
            };

            List<TaxDto> result = _service.Process(operations);

            result.Should().HaveSameCount(operations);
        }
    }
}
