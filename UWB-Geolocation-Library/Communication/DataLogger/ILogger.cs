using UWB_Geolocation_Library.SimpleTypes;

namespace UWB_Geolocation_Library.Communication.DataLogger
{
    internal interface ILogger
    {
        LogModeEnum CurrentLogMode { get; set; }
        void LogInData(string line);
        void LogOutData(string line);
        void Dispose();
    }
}
