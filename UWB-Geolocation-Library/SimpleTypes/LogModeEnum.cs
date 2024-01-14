namespace UWB_Geolocation_Library.SimpleTypes
{
    [Flags]
    public enum LogModeEnum
    {
        None = 0,
        Input = 1,
        Output = 2,
        Both = LogModeEnum.Input | LogModeEnum.Output
    }
}