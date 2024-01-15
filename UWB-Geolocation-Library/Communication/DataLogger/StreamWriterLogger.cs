using UWB_Geolocation_Library.SimpleTypes;

namespace UWB_Geolocation_Library.Communication.DataLogger
{
    internal class StreamWriterLogger : ILogger
    {
        private StreamWriter? inStreamWriter;
        private StreamWriter? outStreamWriter;
        private LogModeEnum currentLogMode;
        private readonly string IN_PATH = Path.Combine(GetLogsDirectory(), "InData.txt");
        private readonly string OUT_PATH = Path.Combine(GetLogsDirectory(), "OutData.txt");

        public LogModeEnum CurrentLogMode { get => currentLogMode; set => currentLogMode = value; }

        public StreamWriterLogger(LogModeEnum logMode)
        {
            EnsureLogsDirectoryExistsEmpty();
            if (logMode.HasFlag(LogModeEnum.Input))
            {
                inStreamWriter = new StreamWriter(IN_PATH);
            }
            if(logMode.HasFlag(LogModeEnum.Output))
            {
                outStreamWriter = new StreamWriter(OUT_PATH);
            }
            currentLogMode = logMode;
        }

        public void LogInData(string line)
        {
            if(currentLogMode.HasFlag(LogModeEnum.Input))
            {
                inStreamWriter ??= new StreamWriter(IN_PATH, true);
                inStreamWriter.WriteLine(line);
            }
        }

        public void LogOutData(string line)
        {
            if (currentLogMode.HasFlag(LogModeEnum.Output))
            {
                outStreamWriter ??= new StreamWriter(OUT_PATH, true);
                outStreamWriter.WriteLine(line);
            }
        }

        public void Dispose()
        {
            inStreamWriter?.Dispose();
            inStreamWriter = null;
            outStreamWriter?.Dispose();
            outStreamWriter = null;
        }

        private static string GetLogsDirectory()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        }

        private static void EnsureLogsDirectoryExistsEmpty()
        {
            string logsDirectory = GetLogsDirectory();

            if (!Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory);
            }
        }
    }
}
