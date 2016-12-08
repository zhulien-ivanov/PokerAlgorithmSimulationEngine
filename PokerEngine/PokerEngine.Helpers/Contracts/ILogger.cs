namespace PokerEngine.Helpers.Contracts
{
    public interface ILogger
    {
        void Log(string message);

        void AddSeparator();
    }
}
