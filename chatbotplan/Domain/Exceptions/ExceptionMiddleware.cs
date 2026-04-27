using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ChatBotPlan.Domain.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _rqDelegate;

    public ExceptionMiddleware(RequestDelegate rqd)
    {
        _rqDelegate = rqd;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _rqDelegate(context);
        }
        catch (Exception excpt)
        {
            await HandleExcpetion(context, excpt);
        }
    }

    private static Task HandleExcpetion(HttpContext context, Exception excpt)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "Erro interno";

        if (excpt is ArgumentException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = excpt.Message;
        }
        if (excpt is DomainException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = excpt.Message;
        }
        var response = new
        {
            error = message,
            status = (int)statusCode
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));

    }
}