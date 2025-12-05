using CapitalGains.Cli.Domain.Entities;
using CapitalGains.Cli.Domain.Enums;
using CapitalGains.Cli.Domain.Models;
using CapitalGains.Cli.Domain.Policies;

namespace CapitalGains.Cli.Application.Services
{
    public class CapitalGainsService
    {
        public List<TaxDto> Process(List<OperationDto> operations)
        {
            List<TaxDto> responses = new();
            OperationProcess operationProcess = new();

            foreach (OperationDto operation in operations)
            {
                if (operation.Type == OperationTypeEnum.Sell)
                {
                    SellProcess(operationProcess, operation, responses);
                    continue;
                }

                BuyProcess(operationProcess, operation, responses);
            }

            return responses;
        }

        private static void BuyProcess(OperationProcess operationProcess, OperationDto operation, List<TaxDto> response)
        {
            operationProcess.UpdateByBuy(operation.Quantity, operation.UnitCost);
            response.Add(new TaxDto(0.00m));
        }

        private static void SellProcess(OperationProcess operationProcess, OperationDto operation, List<TaxDto> responses)
        {
            decimal tax = 0.00m;
            decimal operationValue = operation.Quantity * operation.UnitCost;
            decimal grossValue = (operation.UnitCost - operationProcess.WeightedAveragePrice) * operation.Quantity;
            operationProcess.DeductCurrentQuantity(operation.Quantity);

            if (grossValue < 0)
            {
                operationProcess.SumAccumulatedLoss(grossValue);
                responses.Add(new TaxDto(tax));
                return;
            }

            decimal profitValue = grossValue - operationProcess.AccumulatedLoss;

            if (profitValue <= 0)
            {
                operationProcess.DeductAccumulatedLoss(profitValue);
                responses.Add(new TaxDto(tax));
                return;
            }

            if (operationValue <= TaxRules.OperationLimitValue)
            {
                responses.Add(new TaxDto(tax));
                return;
            }

            tax = profitValue * TaxRules.TaxRate;
            operationProcess.DeductAccumulatedLoss(0m);
            responses.Add(new TaxDto(tax)); 
        }
    }
}
