using CapitalGains.Cli.Domain.Entities;
using FluentAssertions;

namespace CapitalGains.Unit.Tests
{
    public class OperationProcessTest
    {
        private readonly OperationProcess _operationProcess;

        public OperationProcessTest()
        {
            this._operationProcess = new OperationProcess();
        }

        [Fact]
        public void Should_Be_Correct_Initial_Values()
        {
            _operationProcess.CurrentQuantity.Should().Be(0);
            _operationProcess.WeightedAveragePrice.Should().Be(0m);
            _operationProcess.AccumulatedLoss.Should().Be(0m);
        }

        [Fact]
        public void Should_Be_Sum_Current_Quantity()
        {
            _operationProcess.SumCurrentQuantity(100);
            _operationProcess.CurrentQuantity.Should().Be(100);
        }

        [Fact]
        public void Should_Be_Deduct_Current_Quantity()
        {
            _operationProcess.SumCurrentQuantity(100);
            _operationProcess.DeductCurrentQuantity(50);
            _operationProcess.CurrentQuantity.Should().Be(50);
        }

        [Fact]
        public void Should_Be_Accumulated_Loss_Greater_Than_Zero()
        {
            _operationProcess.SumAccumulatedLoss(-100);
            _operationProcess.AccumulatedLoss.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Should_Be_Sum_Accumulated_Loss()
        {
            _operationProcess.SumAccumulatedLoss(-100);
            _operationProcess.AccumulatedLoss.Should().Be(100);
        }

        [Fact]
        public void Should_Be_Deduct_Accumulated_Loss()
        {
            _operationProcess.SumAccumulatedLoss(-100);

            decimal grossValue = 20m;
            decimal profit = grossValue - _operationProcess.AccumulatedLoss;

            _operationProcess.DeductAccumulatedLoss(profit);
            _operationProcess.AccumulatedLoss.Should().Be(80m);
        }

        [Fact]
        public void Should_Be_Update_Weighted_Average_Price()
        {
            _operationProcess.UpdateWeightedAveragePrice(10, 2.99m);
            _operationProcess.WeightedAveragePrice.Should().Be(2.99m);
        }

        [Fact]
        public void Should_Be_Update_By_Buy()
        {
            _operationProcess.UpdateByBuy(10, 3m);
            _operationProcess.UpdateByBuy(10, 1m);
            _operationProcess.WeightedAveragePrice.Should().Be(2m);
            _operationProcess.CurrentQuantity.Should().Be(20);
        }
    }
}
