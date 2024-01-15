using System.Diagnostics;
using UWB_Geolocation_Library.SimpleTypes;

namespace UWB_Geolocation_Library.Communication.DataLogger
{
    /**
     * <summary>
     *  This is the class only for debugging the data during development. Not for release mode.
     * </summary>
     */
    internal class ConsoleLogger : ILogger
    {
        private LogModeEnum currentLogMode;
        public LogModeEnum CurrentLogMode { get => currentLogMode; set => currentLogMode = value; }

        public ConsoleLogger(LogModeEnum logMode)
        {
            currentLogMode = logMode;
        }

        public void Dispose()
        {
            Debug.WriteLine("Library console logger disposed properly...");
        }

        public void LogInData(string line)
        {
            if (currentLogMode.HasFlag(LogModeEnum.Input))
            {
                Debug.WriteLine("Input data: " + line);
            }
        }

        public void LogOutData(string line)
        {
            if (currentLogMode.HasFlag(LogModeEnum.Output))
            {
                Debug.WriteLine("Output data: " + line);
            }
        }
    }
}
