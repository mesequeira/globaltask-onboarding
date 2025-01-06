using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Users.Infrastructure
{
    public static class InfrastructureAssembly
    {
        public static Assembly CurrentAssembly = typeof(InfrastructureAssembly).Assembly;
    }
}
