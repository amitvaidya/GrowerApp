namespace GrowerApp.Interfaces
{
    public interface ILogger
    {
        void LogError (string tag, string message);

        void LogInfo(string tag, string message);
    }
}
