using System;
using System.Collections.Generic;
using System.Security.Claims;
using CoreBox.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace CoreBox.Tests.Extensions
{
    public class HttpContextExtensionsUnitTests
    {
        [Fact]
        public void Deve_Retornar_Falso_Ao_Buscar_Se_Usuario_Esta_Autenticado_Sem_Sessao()
        {
            HttpContext httpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal()
            };

            httpContext.IsAuthenticated().Should().BeFalse();
        }

        [Fact]
        public void Deve_Retornar_Verdadeiro_Ao_Buscar_Se_Usuario_Esta_Autenticado()
        {
            HttpContext httpContext = new DefaultHttpContext();

            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Role, "Role1"),
                new Claim(ClaimTypes.Role, "Role2")
            }, "CustomAuth"));

            httpContext.IsAuthenticated().Should().BeTrue();
        }

        [Fact]
        public void Deve_Retornar_Falso_Ao_Buscar_Se_Usuario_Esta_Autenticado()
        {
            HttpContext httpContext = new DefaultHttpContext();

            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Role, "Role1"),
                new Claim(ClaimTypes.Role, "Role2")
            }));

            httpContext.IsAuthenticated().Should().BeFalse();
        }

        [Fact]
        public void Deve_Retornar_O_Id_Do_Usuario_Autenticado()
        {
            var id = Guid.NewGuid().ToString();
            HttpContext httpContext = new DefaultHttpContext();

            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Role, "Role2")
            }, "CustomAuth"));

            httpContext.GetUserIdAsString().Should().Be(id);
        }

        [Fact]
        public void Nao_Deve_Retornar_O_Id_Do_Usuario_Pois_Nao_Existe_Autenticacao()
        {
            var id = Guid.NewGuid().ToString();
            HttpContext httpContext = new DefaultHttpContext();

            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Role, "Role2")
            }));

            Action act = () => httpContext.GetUserIdAsString();
            act.Should().ThrowExactly<UnauthorizedAccessException>();
        }

        [Fact]
        public void Nao_Deve_Retornar_O_Id_Do_Usuario_Pois_Nao_Foi_Setado_Na_Autenticacao()
        {
            var id = Guid.NewGuid().ToString();
            HttpContext httpContext = new DefaultHttpContext();

            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Role, "Role2")
            }, "CustomAuth"));

            Action act = () => httpContext.GetUserIdAsString();
            act.Should().ThrowExactly<UnauthorizedAccessException>();
        }
    }
}