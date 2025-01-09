using System.Reflection;
using Users.Worker.Application.Users.Consumers;
using Users.Worker.Domain.Users.Models;
using Users.Worker.Infrastructure.Emails.Services;
using Users.Worker.Persistence;

namespace Users.Worker.Architecture.Tests;

public class ArchitectureTests
{
    private readonly Assembly domainAssembly = typeof(User).Assembly;
    private readonly Assembly applicationAssembly = typeof(UserRegisteredEventConsumer).Assembly;
    private readonly Assembly persistanceAssembly = PersistanceAssembly.Assembly;
    private readonly Assembly presentationAssembly = typeof(Worker).Assembly;
    private readonly Assembly infrastructureAssembly = typeof(EmailService).Assembly;

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
