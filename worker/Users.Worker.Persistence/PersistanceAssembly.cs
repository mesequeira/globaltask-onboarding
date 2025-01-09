using System.Reflection;

namespace Users.Worker.Persistence;

public static class PersistanceAssembly
{
    public static Assembly Assembly => typeof(PersistanceAssembly).Assembly;
}
