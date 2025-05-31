namespace AlquileresApp.Core.Interfaces;

public interface IServicioCookies
{
    public Task<string> GetCookie(string key);
    public Task SetCookie(string key, string value, int? expireTime);
    public Task DeleteCookie(string key);
}
