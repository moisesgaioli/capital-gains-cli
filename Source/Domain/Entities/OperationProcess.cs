namespace CapitalGains.Cli.Domain.Entities
{
    public class OperationProcess
    {
        public int CurrentQuantity { get; private set; }
        public decimal WeightedAveragePrice { get; private set; }
        public decimal AccumulatedLoss { get; private set; }

        public OperationProcess() 
        {
            CurrentQuantity = 0;
            WeightedAveragePrice = 0m;
            AccumulatedLoss = 0m;
        }

        public void UpdateByBuy(int quantity, decimal unitCost)
        {
            UpdateWeightedAveragePrice(quantity, unitCost);
            SumCurrentQuantity(quantity);
        }

        public void UpdateWeightedAveragePrice(int quantity, decimal unitCost)
        {
            decimal newWeightedAveragePrice = ((CurrentQuantity * WeightedAveragePrice) + (quantity * unitCost)) / (CurrentQuantity + quantity);
            WeightedAveragePrice = newWeightedAveragePrice;
        }

        public void SumCurrentQuantity(int quantity) =>
            CurrentQuantity += quantity;

        public void DeductCurrentQuantity(int quantity) =>
            CurrentQuantity -= quantity;

        public void SumAccumulatedLoss(decimal quantity) =>
            AccumulatedLoss += Math.Abs(quantity);

        public void DeductAccumulatedLoss(decimal quantity) =>
            AccumulatedLoss = Math.Abs(quantity);
    }
}
