namespace Dev.Business.Models.Base
{
    public interface IAppIdentityUser
    {
        string GetUserName();
        Guid GetUserId();
        bool IsAuthenticated();
        bool IsInRole(string role);
        string? GetRemoteIpAddress();
        string? GetLocalIpAddress();
    }
}
