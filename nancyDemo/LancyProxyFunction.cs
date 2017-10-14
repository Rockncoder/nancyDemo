using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AwsLambdaOwin;
using Nancy;
using Nancy.Owin;

namespace nancyDemo
{
    public class LancyProxyFunction : APIGatewayOwinProxyFunction
    {
        protected override Func<IDictionary<string, object>, Task> Init()
        {
            return NancyMiddleware
                .UseNancy(opt => { opt.Bootstrapper = new LancyBootstrapper(); })(_ => Task.CompletedTask);
        }
    }

    public class LancyBootstrapper : DefaultNancyBootstrapper
    {
        // Remove when https://github.com/NancyFx/Nancy/pull/2694 is shipped.
        protected override IAssemblyCatalog AssemblyCatalog => new LancyDependencyContextAssemblyCatalog();
    }
}