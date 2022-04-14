using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Lesson23.Filters;

public class ResponseDefaultFormatterService
{
    private readonly IHttpResponseStreamWriterFactory _streamWriterFactory;
    private readonly OutputFormatterSelector _formatterSelector;

    public ResponseDefaultFormatterService(
        IHttpResponseStreamWriterFactory streamWriterFactory,
        OutputFormatterSelector formatterSelector)
    {
        _streamWriterFactory = streamWriterFactory;
        _formatterSelector = formatterSelector;
    }
    
    public async Task WriteAsync(HttpContext httpContext, ObjectResult result)
    {
        var formatterContext = new OutputFormatterWriteContext(
            httpContext,
            _streamWriterFactory.CreateWriter,
            result.DeclaredType ?? result.Value?.GetType(),
            result.Value);

        var selectedFormatter = _formatterSelector.SelectFormatter(
            formatterContext,
            (IList<IOutputFormatter>)result.Formatters ?? Array.Empty<IOutputFormatter>(),
            result.ContentTypes);
        if (selectedFormatter == null)
        {
            httpContext.Response.StatusCode = StatusCodes.Status406NotAcceptable;
            return;
        }

        await selectedFormatter.WriteAsync(formatterContext);
    }
}
