using System.Reflection;
using Users.Application.Users.Commands.Create;
using Users.Domain.Users.Models;
using Users.Infrastructure;
using Users.Persistence;
using Users.WebApi.Users.Controllers;

namespace Users.Architecture.Tests;

public class ArchitectureTests
{
    private readonly Assembly domainAssembly = typeof(User).Assembly;
    private readonly Assembly applicationAssembly = typeof(CreateUserCommand).Assembly;
    private readonly Assembly persistanceAssembly = typeof(ApplicationDbContext).Assembly;
    private readonly Assembly presentationAssembly = typeof(UsersController).Assembly;
    private readonly Assembly infrastructureAssembly = typeof(InfrastructureAssembly).Assembly;

    [Fact]
    public void Domain_ShouldNotHaveDependenciesOnOtherLayers()
    {

        var references = domainAssembly.GetReferencedAssemblies();

        var invalidReferences = references
            .Where(r =>
                    r.Name == applicationAssembly.GetName().Name
                    || r.Name == persistanceAssembly.GetName().Name
                    || r.Name == presentationAssembly.GetName().Name
                    || r.Name == infrastructureAssembly.GetName().Name
            )
            .ToList();

        Assert.Empty(invalidReferences);
    }


    [Fact]
    public void Application_ShouldNotDependentOnPresentation()
    {
        var references = applicationAssembly.GetReferencedAssemblies();

        var presentation = references.FirstOrDefault(r => r.Name == presentationAssembly.GetName().Name);

        Assert.Null(presentation);
    }

    [Fact]
    public void Persistance_ShouldNotDependentOnPresentation()
    {
        var references = persistanceAssembly.GetReferencedAssemblies();

        var presentation = references.FirstOrDefault(r => r.Name == presentationAssembly.GetName().Name);

        Assert.Null(presentation);
    }

    [Fact]
    public void Infrastructure_ShouldNotDependentOnPresentation()
    {
        var references = infrastructureAssembly.GetReferencedAssemblies();

        var presentation = references.FirstOrDefault(r => r.Name == presentationAssembly.GetName().Name);

        Assert.Null(presentation);
    }

    [Fact]
    public void Application_ShouldOnlyDependentOnDomain()
    {
        var references = applicationAssembly.GetReferencedAssemblies();

        var invalidReferences = references
            .Where(r =>
                    r.Name == persistanceAssembly.GetName().Name
                    || r.Name == presentationAssembly.GetName().Name
                    || r.Name == infrastructureAssembly.GetName().Name
            )
            .ToList();

        Assert.Empty(invalidReferences);
    }
}
