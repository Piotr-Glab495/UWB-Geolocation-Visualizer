using UWB_Geolocation_Library.SimpleTypes;

namespace UWB_Geolocation_Library.Communication.DataLogger
{
    internal class ConsoleLogger : ILogger
    {
        private readonly LogModeEnum currentLogMode;
        public LogModeEnum CurrentLogMode => currentLogMode;

        public ConsoleLogger(LogModeEnum logMode)
        {
            currentLogMode = logMode;
        }

        public void Dispose()
        {
            Console.WriteLine("Disposed properly...");
        }

        public void LogInData(string line)
        {
            Console.WriteLine("Input data: " + line);
        }

        public void LogOutData(string line)
        {
            Console.WriteLine("Output data: " + line);
        }
    }
}
