using System.Net;
using CoreBox.Exceptions;
using FluentValidation;

namespace CoreBox.Extensions;

public static class ExceptionExtensions
{
    public static HttpStatusCode ToHttpStatus(this Exception ex)
        => ex switch
        {
            Exception _ex when _ex is ValidationException || _ex is BusinessRuleException => HttpStatusCode.BadRequest,
            Exception _ex when _ex is UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            Exception _ex when _ex is ForbiddenAccessException => HttpStatusCode.Forbidden,
            Exception _ex when _ex is NotFoundException => HttpStatusCode.NotFound,
            Exception _ex when _ex is ConflictException => HttpStatusCode.Conflict,
            Exception _ex when _ex is UnavailableException => HttpStatusCode.Gone,
            Exception _ex when _ex is AggregateException => ToHttpStatus(((AggregateException)_ex).InnerException),
            _ => HttpStatusCode.InternalServerError
        };
}