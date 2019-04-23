namespace KocFinansCC.Common.Services.Abstract
{
    public interface ISMSService
    {
        bool SendSMS(string phoneNumber);
    }
}
