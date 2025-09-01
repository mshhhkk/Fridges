using System.Diagnostics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
namespace Fridges.Api.Exeptions;

public class GlobalExeptionHandler: IExceptionHandler
{
    private readonly ILogger<GlobalExeptionHandler> _logger;

    public GlobalExeptionHandler(ILogger<GlobalExeptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled exeption occured : {Message}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/json";

        var response = new
        {
            error = "Произошла внутренняя ошибка",
            traceId = httpContext.TraceIdentifier
        };

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
