using System.Text.Json;
using CapitalGains.Integration.Tests;

namespace CapitalGains.Cli.Integration.Tests
{
    public class CapitalGainsServiceTest
    {
        private static string NormalizeJsonLines(string text)
        {
            var lines = text
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var normalized = lines
                .Select(NormalizeJson);

            return string.Join(Environment.NewLine, normalized);
        }

        private static string NormalizeJson(string json)
        {
            using var doc = JsonDocument.Parse(json);
            return JsonSerializer.Serialize(doc);
        }

        [Fact]
        public void Case_1()
        {
            var input = """
            [{"operation":"buy", "unit-cost":10.00, "quantity": 100},{"operation":"sell", "unit-cost":15.00, "quantity": 50},{ "operation":"sell", "unit-cost":15.00, "quantity": 50}]
            """;

            var expected = """
            [{"tax":0.00},{"tax":0.00},{"tax":0.00}]
            """;

            var output = CapitalGainsCliRunner.RunCli(input);
            Assert.Equal(NormalizeJsonLines(expected), NormalizeJsonLines(output));
        }

        [Fact]
        public void Case_2()
        {
            var input = """[{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{ "operation":"sell", "unit-cost":20.00, "quantity": 5000},{ "operation":"sell", "unit-cost":5.00, "quantity": 5000}]""";

            var expected = """
            [{"tax":0.00},{"tax":10000.00},{"tax":0.00}]
            """;

            var output = CapitalGainsCliRunner.RunCli(input);
            Assert.Equal(NormalizeJsonLines(expected), NormalizeJsonLines(output));
        }

        [Fact]
        public void Case_3()
        {
            var input = """
            [{"operation":"buy", "unit-cost":10.00, "quantity": 100},{ "operation":"sell", "unit-cost":15.00, "quantity": 50},{ "operation":"sell", "unit-cost":15.00, "quantity": 50}]
            [{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{ "operation":"sell", "unit-cost":20.00, "quantity": 5000},{ "operation":"sell", "unit-cost":5.00, "quantity": 5000}]
            """;

            var expected = """
            [{"tax":0.00},{"tax":0.00},{"tax":0.00}]
            [{"tax":0.00},{"tax":10000.00},{"tax":0.00}]
            """;

            var output = CapitalGainsCliRunner.RunCli(input);
            Assert.Equal(NormalizeJsonLines(expected), NormalizeJsonLines(output));
        }

        [Fact]
        public void Case_4()
        {
            var input = """
            [{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{ "operation":"sell", "unit-cost":5.00, "quantity": 5000},{ "operation":"sell", "unit-cost":20.00, "quantity": 3000}]
            """;

            var expected = """
            [{"tax":0.00},{"tax":0.00},{"tax":1000.00}]
            """;

            var output = CapitalGainsCliRunner.RunCli(input);
            Assert.Equal(NormalizeJsonLines(expected), NormalizeJsonLines(output));
        }

        [Fact]
        public void Case_5()
        {
            var input = """
            [{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{ "operation":"buy", "unit-cost":25.00, "quantity": 5000},{ "operation":"sell", "unit-cost":15.00, "quantity": 10000}]
            """;

            var expected = """
            [{"tax":0.00},{"tax":0.00},{"tax":0.00}]
            """;

            var output = CapitalGainsCliRunner.RunCli(input);
            Assert.Equal(NormalizeJsonLines(expected), NormalizeJsonLines(output));
        }

        [Fact]
        public void Case_6()
        {
            var input = """
            [{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{ "operation":"buy", "unit-cost":25.00, "quantity": 5000},{ "operation":"sell", "unit-cost":15.00, "quantity": 10000},{ "operation":"sell", "unit-cost":25.00, "quantity": 5000}]
            """;

            var expected = """
            [{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":10000.00}]
            """;

            var output = CapitalGainsCliRunner.RunCli(input);
            Assert.Equal(NormalizeJsonLines(expected), NormalizeJsonLines(output));
        }

        [Fact]
        public void Case_7()
        {
            var input = """
            [{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{ "operation":"sell", "unit-cost":2.00, "quantity": 5000},{ "operation":"sell", "unit-cost":20.00, "quantity": 2000},{ "operation":"sell", "unit-cost":20.00, "quantity": 2000},{ "operation":"sell", "unit-cost":25.00, "quantity": 1000}]
            """;

            var expected = """
            [{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":3000.00}]
            """;

            var output = CapitalGainsCliRunner.RunCli(input);
            Assert.Equal(NormalizeJsonLines(expected), NormalizeJsonLines(output));
        }

        [Fact]
        public void Case_8()
        {
            var input = """
            [{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{ "operation":"sell", "unit-cost":2.00, "quantity": 5000},{ "operation":"sell", "unit-cost":20.00, "quantity": 2000},{ "operation":"sell", "unit-cost":20.00, "quantity": 2000},{ "operation":"sell", "unit-cost":25.00, "quantity": 1000},{ "operation":"buy", "unit-cost":20.00, "quantity": 10000},{ "operation":"sell", "unit-cost":15.00, "quantity": 5000},{ "operation":"sell", "unit-cost":30.00, "quantity": 4350},{ "operation":"sell", "unit-cost":30.00, "quantity": 650}]
            """;

            var expected = """
            [{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":3000.00},{"tax":0.00},{"tax":0.00},{"tax":3700.00},{"tax":0.00}]
            """;

            var output = CapitalGainsCliRunner.RunCli(input);
            Assert.Equal(NormalizeJsonLines(expected), NormalizeJsonLines(output));
        }

        [Fact]
        public void Case_9()
        {
            var input = """
            [{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{ "operation":"sell", "unit-cost":50.00, "quantity": 10000},{ "operation":"buy", "unit-cost":20.00, "quantity": 10000},{ "operation":"sell", "unit-cost":50.00, "quantity": 10000}]
            """;

            var expected = """
            [{"tax":0.00},{"tax":80000.00},{"tax":0.00},{"tax":60000.00}]
            """;

            var output = CapitalGainsCliRunner.RunCli(input);
            Assert.Equal(NormalizeJsonLines(expected), NormalizeJsonLines(output));
        }

        [Fact]
        public void Case_10()
        {
            var input = """
            [{"operation": "buy", "unit-cost": 5000.00, "quantity": 10},{ "operation": "sell", "unit-cost": 4000.00, "quantity": 5},{ "operation": "buy", "unit-cost": 15000.00, "quantity": 5},{ "operation": "buy", "unit-cost": 4000.00, "quantity": 2},{ "operation": "buy", "unit-cost": 23000.00, "quantity": 2},{ "operation": "sell", "unit-cost": 20000.00, "quantity": 1},{ "operation": "sell", "unit-cost": 12000.00, "quantity": 10},{ "operation": "sell", "unit-cost": 15000.00, "quantity": 3}]
            """;

            var expected = """
            [{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":1000.00},{"tax":2400.00}]
            """;

            var output = CapitalGainsCliRunner.RunCli(input);
            Assert.Equal(NormalizeJsonLines(expected), NormalizeJsonLines(output));
        }

        [Fact]
        public void All_Cases()
        {
            var input = """
            [{"operation":"buy", "unit-cost":10.00, "quantity": 100},{"operation":"sell", "unit-cost":15.00, "quantity": 50},{"operation":"sell", "unit-cost":15.00, "quantity": 50}]
            [{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{"operation":"sell", "unit-cost":20.00, "quantity": 5000},{"operation":"sell", "unit-cost":5.00, "quantity": 5000}]
            [{"operation":"buy", "unit-cost":10.00, "quantity": 100},{"operation":"sell", "unit-cost":15.00, "quantity": 50},{"operation":"sell", "unit-cost":15.00, "quantity": 50}]
            [{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{"operation":"sell", "unit-cost":20.00, "quantity": 5000},{"operation":"sell", "unit-cost":5.00, "quantity": 5000}]
            [{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{"operation":"sell", "unit-cost":5.00, "quantity": 5000},{"operation":"sell", "unit-cost":20.00, "quantity": 3000}]
            [{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{"operation":"buy", "unit-cost":25.00, "quantity": 5000},{"operation":"sell", "unit-cost":15.00, "quantity": 10000}]
            [{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{"operation":"buy", "unit-cost":25.00, "quantity": 5000},{"operation":"sell", "unit-cost":15.00, "quantity": 10000},{"operation":"sell", "unit-cost":25.00, "quantity": 5000}]
            [{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{"operation":"sell", "unit-cost":2.00, "quantity": 5000},{"operation":"sell", "unit-cost":20.00, "quantity": 2000},{"operation":"sell", "unit-cost":20.00, "quantity": 2000},{"operation":"sell", "unit-cost":25.00, "quantity": 1000}]
            [{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{"operation":"sell", "unit-cost":2.00, "quantity": 5000},{"operation":"sell", "unit-cost":20.00, "quantity": 2000},{"operation":"sell", "unit-cost":20.00, "quantity": 2000},{"operation":"sell", "unit-cost":25.00, "quantity": 1000},{"operation":"buy", "unit-cost":20.00, "quantity": 10000},{"operation":"sell", "unit-cost":15.00, "quantity": 5000},{"operation":"sell", "unit-cost":30.00, "quantity": 4350},{"operation":"sell", "unit-cost":30.00, "quantity": 650}]
            [{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{"operation":"sell", "unit-cost":50.00, "quantity": 10000},{"operation":"buy", "unit-cost":20.00, "quantity": 10000},{"operation":"sell", "unit-cost":50.00, "quantity": 10000}]
            [{"operation": "buy", "unit-cost": 5000.00, "quantity": 10},{"operation": "sell", "unit-cost": 4000.00, "quantity": 5},{"operation": "buy", "unit-cost": 15000.00, "quantity": 5},{"operation": "buy", "unit-cost": 4000.00, "quantity": 2},{"operation": "buy", "unit-cost": 23000.00, "quantity": 2},{"operation": "sell", "unit-cost": 20000.00, "quantity": 1},{"operation": "sell", "unit-cost": 12000.00, "quantity": 10},{"operation": "sell", "unit-cost": 15000.00, "quantity": 3}]
            """;

            var expected = """
            [{"tax":0.00},{"tax":0.00},{"tax":0.00}]
            [{"tax":0.00},{"tax":10000.00},{"tax":0.00}]
            [{"tax":0.00},{"tax":0.00},{"tax":0.00}]
            [{"tax":0.00},{"tax":10000.00},{"tax":0.00}]
            [{"tax":0.00},{"tax":0.00},{"tax":1000.00}]
            [{"tax":0.00},{"tax":0.00},{"tax":0.00}]
            [{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":10000.00}]
            [{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":3000.00}]
            [{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":3000.00},{"tax":0.00},{"tax":0.00},{"tax":3700.00},{"tax":0.00}]
            [{"tax":0.00},{"tax":80000.00},{"tax":0.00},{"tax":60000.00}]
            [{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":1000.00},{"tax":2400.00}]
            """;

            var output = CapitalGainsCliRunner.RunCli(input);
            Assert.Equal(NormalizeJsonLines(expected), NormalizeJsonLines(output));
        }

        [Fact]
        public void Should_Process_Input_File_Like_Dotnet_Run_Redirect()
        {
            var filePath = Path.GetFullPath(
                Path.Combine(
                    AppContext.BaseDirectory, "..", "..", "..", "..", "..", "Source", "Input")
                );

            var fileTxt = Path.Combine(filePath, "input.txt");
            var input = File.ReadAllText(fileTxt);

            var expected = """
            [{"tax":0.00},{"tax":0.00},{"tax":0.00}]
            [{"tax":0.00},{"tax":10000.00},{"tax":0.00}]
            [{"tax":0.00},{"tax":0.00},{"tax":0.00}]
            [{"tax":0.00},{"tax":10000.00},{"tax":0.00}]
            [{"tax":0.00},{"tax":0.00},{"tax":1000.00}]
            [{"tax":0.00},{"tax":0.00},{"tax":0.00}]
            [{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":10000.00}]
            [{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":3000.00}]
            [{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":3000.00},{"tax":0.00},{"tax":0.00},{"tax":3700.00},{"tax":0.00}]
            [{"tax":0.00},{"tax":80000.00},{"tax":0.00},{"tax":60000.00}]
            [{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":1000.00},{"tax":2400.00}]
            """;

            var output = CapitalGainsCliRunner.RunCli(input);
            Assert.Equal(NormalizeJsonLines(expected), NormalizeJsonLines(output));
        }
    }
}
