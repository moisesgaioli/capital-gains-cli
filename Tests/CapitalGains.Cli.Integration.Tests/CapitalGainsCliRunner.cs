using System.Diagnostics;

namespace CapitalGains.Integration.Tests
{
    public static class CapitalGainsCliRunner
    {
        public static string RunCli(string input)
        {
            // The project must be compiled at least once.
            var projectDir = Path.GetFullPath(
                Path.Combine(
                    AppContext.BaseDirectory, "..", "..", "..", "..", "..", "Source", "bin", "Debug", "net10.0")
                );

            var projectFile = Path.Combine(projectDir, "CapitalGains.Cli.dll");

            ProcessStartInfo startInfo = new()
            {
                FileName = "dotnet",
                Arguments = $"\"{projectFile}\"",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using var process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            process.StandardInput.WriteLine(input);
            process.StandardInput.Close();

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (!string.IsNullOrWhiteSpace(error))
                throw new Exception(error);

            return output.Trim();
        }
    }
}
