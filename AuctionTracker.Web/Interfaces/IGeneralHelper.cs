namespace AuctionTracker.Web.Interfaces
{
    public interface IGeneralHelper
    {
        string ConvertDateTime(DateTime val);
        bool ValidDecimalNumber(decimal val);
    }
}