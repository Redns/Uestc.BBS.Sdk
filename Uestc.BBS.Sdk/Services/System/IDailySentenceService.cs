namespace Uestc.BBS.Sdk.Services.System
{
    public interface IDailySentenceService
    {
        Task<string> GetDailySentenceAsync();
    }
}
