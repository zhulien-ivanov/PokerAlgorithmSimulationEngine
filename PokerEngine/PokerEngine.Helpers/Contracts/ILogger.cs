namespace PokerEngine.Helpers.Contracts
{
    public interface ILogger
    {
        void Log(string message);

        void LogError(string message);

        void LogInfo(string message);

        void LogWarning(string message);

        void AddSeparator();
    }
}
