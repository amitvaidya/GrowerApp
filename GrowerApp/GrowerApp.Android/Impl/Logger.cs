using GrowerApp.Interfaces;

namespace GrowerApp.Droid.Impl
{
    public class Logger : ILogger
    {
        public void LogError(string tag, string message) => Android.Util.Log.Error(tag, message);

        public void LogInfo(string tag, string message) => Android.Util.Log.Info(tag, message);
    }
}