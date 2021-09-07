using System;
using System.Net;
using CoreBox.Exceptions;
using CoreBox.Extensions;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CoreBox.Tests.Extensions
{
    public class ExceptionExtensionsUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Retornar_HttpStatus_BadRequest(ValidationException validationException, BusinessRuleException businessRuleException)
        {
            var httpStatus = validationException.ToHttpStatus();
            httpStatus.Should().Be(HttpStatusCode.BadRequest);

            httpStatus = businessRuleException.ToHttpStatus();
            httpStatus.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Retornar_HttpStatus_Unauthorized(UnauthorizedAccessException exception)
        {
            var httpStatus = exception.ToHttpStatus();
            httpStatus.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Retornar_HttpStatus_Forbidden(ForbiddenAccessException exception)
        {
            var httpStatus = exception.ToHttpStatus();
            httpStatus.Should().Be(HttpStatusCode.Forbidden);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Retornar_HttpStatus_NotFound(NotFoundException exception)
        {
            var httpStatus = exception.ToHttpStatus();
            httpStatus.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Retornar_HttpStatus_Conflict(ConflictException exception)
        {
            var httpStatus = exception.ToHttpStatus();
            httpStatus.Should().Be(HttpStatusCode.Conflict);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Retornar_HttpStatus_Gone(UnavailableException exception)
        {
            var httpStatus = exception.ToHttpStatus();
            httpStatus.Should().Be(HttpStatusCode.Gone);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Retornar_HttpStatus_Gone2(AggregateException exception)
        {
            var teste = new AggregateException("teste inner");

            var httpStatus = exception.ToHttpStatus();
            httpStatus.Should().Be(HttpStatusCode.Gone);
        }
    }
}