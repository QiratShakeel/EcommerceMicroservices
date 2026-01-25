using Serilog;

namespace BuildingBlocks.Shared.Behaviors.Logging
{
    public class SerilogLoggerService : ILoggerService
    {
        private readonly ILogger _logger;

        public SerilogLoggerService(ILogger logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message, params object[] args)
            => _logger.Information(message, args);

        public void LogWarning(string message, params object[] args)
            => _logger.Warning(message, args);

        public void LogError(Exception ex, string message, params object[] args)
            => _logger.Error(ex, message, args);

        public void LogDebug(string message, params object[] args)
            => _logger.Debug(message, args);
    }
}
