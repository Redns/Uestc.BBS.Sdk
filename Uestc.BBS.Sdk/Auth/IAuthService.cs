namespace Uestc.BBS.Sdk.Auth
{
    public interface IAuthService
    {
        Task LoginAsync(AuthCredential credential);
    }
}
