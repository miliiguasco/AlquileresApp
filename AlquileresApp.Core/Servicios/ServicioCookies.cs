using Microsoft.JSInterop;
using AlquileresApp.Core.Interfaces;
namespace AlquileresApp.Core.Servicios;

public class ServicioCookies(IJSRuntime jsRuntime) : IServicioCookies
{
    public async Task SetCookie(string name, string value, int? expireTime)
    {
        await jsRuntime.InvokeVoidAsync("setCookie", name, value, expireTime);
    }

    public async Task DeleteCookie(string name)
    {   
        await jsRuntime.InvokeVoidAsync("deleteCookie", name);
    }

    public async Task<string> GetCookie(string name)
    {
        return await jsRuntime.InvokeAsync<string>("getCookie", name);
    }
}
