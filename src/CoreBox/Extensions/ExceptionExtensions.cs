using System.Net;
using CoreBox.Exceptions;
using FluentValidation;

namespace CoreBox.Extensions;

public static class ExceptionExtensions
{
    public static string GetMessage(this Exception ex) =>
        ex is AggregateException ? ex.InnerException.Message : ex?.Message;

    public static HttpStatusCode ToHttpStatus(this Exception ex)
        => ex switch
        {
            Exception _ex when _ex is ValidationException || _ex is BadRequestException => HttpStatusCode.BadRequest,
            Exception _ex when _ex is UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            Exception _ex when _ex is ForbiddenException => HttpStatusCode.Forbidden,
            Exception _ex when _ex is NotFoundException => HttpStatusCode.NotFound,
            Exception _ex when _ex is ConflictException || _ex is BusinessRuleException => HttpStatusCode.Conflict,
            Exception _ex when _ex is GoneException => HttpStatusCode.Gone,
            Exception _ex when _ex is AggregateException => ToHttpStatus(((AggregateException)_ex).InnerException),
            _ => HttpStatusCode.InternalServerError
        };
}