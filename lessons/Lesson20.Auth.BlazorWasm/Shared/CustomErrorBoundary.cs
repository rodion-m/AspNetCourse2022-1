using System.Net;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

#pragma warning disable CS8618

namespace Lesson20.Auth.BlazorWasm.Shared;

public class CustomErrorBoundary : ErrorBoundary
{
    private string? _errorUri;
    [Inject] private NavigationManager _navigationManager { get; set; }

    protected override Task OnErrorAsync(Exception exception)
    {
        _errorUri = _navigationManager.Uri;
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
                Recover();
                var uri = _navigationManager.Uri;
                _navigationManager.NavigateTo($"/LogIn?redirect_to={uri}");
                break;
            default:
            {
                if (_navigationManager.Uri != _errorUri)
                {
                    Recover();
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