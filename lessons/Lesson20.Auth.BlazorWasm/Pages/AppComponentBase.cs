using Blazored.LocalStorage;
using Lesson14.HttpApiClient;
using Microsoft.AspNetCore.Components;

#pragma warning disable CS8618

namespace Lesson20.Auth.BlazorWasm.Pages;

public abstract class AppComponentBase : ComponentBase
{
    [Inject] protected ShopClient ShopClient { get; private set; }
    [Inject] protected ILocalStorageService LocalStorage { get; private set; }

    protected bool IsTokenChecked { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (!IsTokenChecked)
        {
            IsTokenChecked = true;
            var token = await LocalStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                ShopClient.SetAuthorizationToken(token);
            }
        }
    }
}