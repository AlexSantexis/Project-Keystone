using Serilog;

namespace Project_Keystone.Tests
{
    public class TestBase : IDisposable
    {
        public TestBase() 
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("Logging/testlog.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
            Log.Information("Test Started");
        }
        public void Dispose()
        {
            Log.Information("Test finished");
            Log.CloseAndFlush();
        }
    }
}
