using Blazored.LocalStorage;
using Lesson14.HttpClient;
using Microsoft.AspNetCore.Components;

#pragma warning disable CS8618

namespace Lesson20.Auth.BlazorWasm.Pages;

public abstract class AppComponentBase : ComponentBase
{
    [Inject] protected ShopClient _shopClient { get; private set; }
    [Inject] protected ILocalStorageService _localStorage { get; private set; }

    protected bool IsTokenChecked { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (!IsTokenChecked)
        {
            IsTokenChecked = true;
            var token = await _localStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token)) _shopClient.SetAuthorizationToken(token);
        }
    }
}