using System;
using System.Net;
using CoreBox.Exceptions;
using CoreBox.Extensions;
using CoreBox.Test;
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
            httpStatus.Should().Be(HttpStatusCode.Conflict);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Retornar_HttpStatus_Unauthorized(UnauthorizedAccessException exception)
        {
            var httpStatus = exception.ToHttpStatus();
            httpStatus.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Retornar_HttpStatus_Forbidden(ForbiddenException exception)
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
        public void Deve_Retornar_HttpStatus_Gone(GoneException exception)
        {
            var httpStatus = exception.ToHttpStatus();
            httpStatus.Should().Be(HttpStatusCode.Gone);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Retornar_HttpStatus_BadRequest_Por_AggregateException(ValidationException validationException)
        {
            var aggregateException = new AggregateException(validationException);
            var httpStatus = aggregateException.ToHttpStatus();
            httpStatus.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public void Deve_Retornar_HttpStatus_InternalServerError()
        {
            int one = 1;
            int zero = 0;
            AggregateException exception = null;

            try
            {
                var bug = one / zero;
            }
            catch (Exception ex)
            {
                exception = new AggregateException(ex);
            }

            var httpStatus = exception.ToHttpStatus();
            httpStatus.Should().Be(HttpStatusCode.InternalServerError);
        }
    }
}