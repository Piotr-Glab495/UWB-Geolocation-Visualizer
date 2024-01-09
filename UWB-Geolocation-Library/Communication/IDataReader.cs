namespace UWB_Geolocation_Library.Communication
{
    internal interface IDataReader
    {
        //TODO: add methods responsible for communication with localising device
        abstract void OpenPort();
        abstract void ClosePort();
        abstract Task<double[]> ReadDataAsync();
    }
}
