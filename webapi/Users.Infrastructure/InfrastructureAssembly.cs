using System.Reflection;

namespace Users.Infrastructure;

public static class InfrastructureAssembly
{
    public static Assembly CurrentAssembly = typeof(InfrastructureAssembly).Assembly;
}
