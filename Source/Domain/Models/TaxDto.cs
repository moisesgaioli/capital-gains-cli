using System.Text.Json.Serialization;

namespace CapitalGains.Cli.Domain.Models
{
    public class TaxDto
    {
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }

        public TaxDto(decimal tax) =>
            Tax = Math.Round(tax, 2, MidpointRounding.AwayFromZero);
    }
}
