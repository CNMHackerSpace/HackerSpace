﻿using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;
//See: https://learn.microsoft.com/en-us/azure/active-directory/develop/tutorial-blazor-webassembly
namespace HackerSpace.Client
{
    public class GraphAPIAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public GraphAPIAuthorizationMessageHandler(IAccessTokenProvider provider,
            NavigationManager navigationManager)
            : base(provider, navigationManager)
        {
            ConfigureHandler(
                authorizedUrls: new[] { "https://graph.microsoft.com" },
                scopes: new[] { "https://graph.microsoft.com/User.Read", "https://graph.microsoft.com/Mail.Read" });
        }
    }
}
