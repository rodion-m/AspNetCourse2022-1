using System.Net;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

#pragma warning disable CS8618

namespace Lesson20.Auth.BlazorWasm.Shared;

public class CustomErrorBoundary : ErrorBoundary
{
    private string? _errorUri;
    [Inject] private NavigationManager NavigationManager { get; set; }

    protected override Task OnErrorAsync(Exception exception)
    {
        _errorUri = NavigationManager.Uri;
        return base.OnErrorAsync(exception);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        switch (CurrentException)
        {
            case null:
                base.BuildRenderTree(builder);
                break;
            case HttpRequestException { StatusCode: HttpStatusCode.Unauthorized }:
                base.Recover();
                var uri = NavigationManager.Uri;
                NavigationManager.NavigateTo($"/LogIn?redirect_to={uri}");
                break;
            default:
            {
                if (NavigationManager.Uri != _errorUri)
                {
                    base.Recover();
                }
                else
                {
                    base.BuildRenderTree(builder);
                    if (CurrentException is HttpRequestException e)
                    {
                        var html = $"<div>Error Status Code: {e.StatusCode}</div>";
                        builder.AddContent(0, new MarkupString(html));
                    }
                }
                break;
            }
        }
    }
}