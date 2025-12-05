using System.Text.Json;
using CapitalGains.Cli.Application.Services;
using CapitalGains.Cli.Domain.Models;

namespace CapitalGains.Cli
{
    class Program
    {
        static void Main()
        {
            string? line;
            while ((line = Console.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    break;

                string? jsonOutput = JsonProcess(line);
                Console.WriteLine(jsonOutput);
            }
        }

        private static string? JsonProcess(string input)
        {
            List<OperationDto>? operations = JsonSerializer.Deserialize<List<OperationDto>>(input);

            if (operations is null)
                return string.Empty;

            List<TaxDto> result = new CapitalGainsService().Process(operations);
            return JsonSerializer.Serialize(result);
        }
            
    }
}

