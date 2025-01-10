using Xunit;
using System.IO;

namespace Users.Architecture.Tests
{
    public class ProjectReferencesTests
    {
        [Fact]
        public void Application_Should_Not_Reference_Infrastructure()
        {
            var appCsprojPath = Path.Combine("..", "..", "Users.Application", "Users.Application.csproj");
            var appCsprojContent = File.ReadAllText(appCsprojPath);

            Assert.DoesNotContain("<ProjectReference Include=\"../Infrastructure/Users.Infrastructure/Users.Infrastructure.csproj\" />", appCsprojContent);
        }

        [Fact]
        public void WebApi_Should_Not_Reference_Domain_Directly()
        {
            var webApiCsprojPath = Path.Combine("..", "..", "Users.WebApi", "Users.WebApi.csproj");
            var webApiCsprojContent = File.ReadAllText(webApiCsprojPath);

            Assert.DoesNotContain("<ProjectReference Include=\"../Core/Users.Domain/Users.Domain.csproj\" />", webApiCsprojContent);
        }
    }
}
