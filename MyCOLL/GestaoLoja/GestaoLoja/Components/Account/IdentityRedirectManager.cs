using Microsoft.AspNetCore.Components;

namespace GestaoLoja.Components.Account
{
    internal sealed class IdentityRedirectManager
    {
        private readonly NavigationManager _navigationManager;

        public IdentityRedirectManager(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void RedirectTo(string? uri)
        {
            uri ??= "";

            if (uri.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Cannot redirect to an external URL: \"{uri}\".");
            }

            _navigationManager.NavigateTo(uri);
        }

        public void RedirectToWithStatus(string uri, string message, HttpContext context)
        {
            context.Response.StatusCode = 401;
            _navigationManager.NavigateTo(uri);
        }
    }
}