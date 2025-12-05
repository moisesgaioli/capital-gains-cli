using System.Text.Json.Serialization;
using CapitalGains.Cli.Domain.Enums;

namespace CapitalGains.Cli.Domain.Models
{
    public class OperationDto
    {
        private string _operation = string.Empty;

        [JsonPropertyName("operation")]
        public string Operation { 
            get => _operation;

            set
            {
                _operation = value.ToLower();
                Type = _operation switch
                {
                    "buy" => OperationTypeEnum.Buy,
                    "sell" => OperationTypeEnum.Sell,
                    _ => throw new ArgumentException("Invalid operation type")
                };
            }
        }

        [JsonPropertyName("unit-cost")]
        public decimal UnitCost { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonIgnore]
        public OperationTypeEnum Type { get; private set; }

        public OperationDto(string operation, decimal unitCost, int quantity)
        {
            Operation = operation;
            UnitCost = unitCost;
            Quantity = quantity;
        }
    }
}
