using System.Collections.Generic;
using System.Security.Claims;
using CoreBox.Extensions;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Extensions
{
    public class ClaimsPrincipalExtensionUnitTests
    {
        [Fact]
        public void Deve_Encontrar_A_Role_Dentro_Das_Claims()
        {
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>() 
            {
                new Claim(ClaimTypes.Role, "Role1"),
                new Claim(ClaimTypes.Role, "Role2")
            }));

            var hasRole = claimsPrincipal.HasRole(new string[] { "Role1" });
            hasRole.Should().Be(true);
        }

        [Fact]
        public void Nao_Deve_Encontrar_A_Role_Dentro_Das_Claims()
        {
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>() 
            {
                new Claim(ClaimTypes.Role, "Role1"),
                new Claim(ClaimTypes.Role, "Role2")
            }));

            var hasRole = claimsPrincipal.HasRole(new string[] { "Role3" });
            hasRole.Should().Be(false);
        }
    }
}