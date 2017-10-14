using Nancy.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using Nancy;

namespace nancyDemo
{
    /// <inheritdoc />
    /// <summary>
    /// Default implementation of the <see cref="T:Nancy.IAssemblyCatalog" /> interface, based on
    /// retrieving <see cref="T:System.Reflection.Assembly" /> information from <see cref="T:Microsoft.Extensions.DependencyModel.DependencyContext" />.
    /// </summary>
    public class LancyDependencyContextAssemblyCatalog : IAssemblyCatalog
    {
        private static readonly Assembly NancyAssembly = typeof(INancyEngine).GetTypeInfo().Assembly;
        private readonly DependencyContext dependencyContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LancyDependencyContextAssemblyCatalog"/> class,
        /// using <paramref name="entryAssembly"/>.
        /// </summary>
        public LancyDependencyContextAssemblyCatalog()
        {
            this.dependencyContext =
                DependencyContext.Load(typeof(LancyDependencyContextAssemblyCatalog).GetAssembly());

            // dependencyContext may be null depending on hosting model. it's null in 'dotnet xunit' and resharper test runner, but not null in ncrunch.
            if (dependencyContext == null)
            {
                this.dependencyContext = DependencyContext.Load(Assembly.GetEntryAssembly());
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets all <see cref="T:System.Reflection.Assembly" /> instances in the catalog.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IReadOnlyCollection`1" /> of <see cref="T:System.Reflection.Assembly" /> instances.</returns>
        public IReadOnlyCollection<Assembly> GetAssemblies()
        {
            var results = new HashSet<Assembly>
            {
                typeof(DependencyContextAssemblyCatalog).GetTypeInfo().Assembly
            };

            foreach (var library in this.dependencyContext.RuntimeLibraries)
            {
                if (IsReferencingNancy(library))
                {
                    foreach (var assemblyName in library.GetDefaultAssemblyNames(this.dependencyContext))
                    {
                        results.Add(SafeLoadAssembly(assemblyName));
                    }
                }
            }

            return results.ToArray();
        }

        private static Assembly SafeLoadAssembly(AssemblyName assemblyName)
        {
            try
            {
                return Assembly.Load(assemblyName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static bool IsReferencingNancy(Library library)
        {
            return library.Dependencies.Any(dependency => dependency.Name.Equals(NancyAssembly.GetName().Name));
        }
    }
}