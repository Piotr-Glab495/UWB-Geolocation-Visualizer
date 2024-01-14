namespace UWB_Geolocation_Library.Communication.DataReader
{
    internal interface IDataReader
    {
        abstract void OpenPort();
        abstract void ClosePort();
        abstract double[]? ReadData();
    }
}
