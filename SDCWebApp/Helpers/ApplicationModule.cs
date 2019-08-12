using Autofac;
using System.Reflection;

namespace SDCWebApp.Helpers
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterTypes(assembly.GetTypes()).AsImplementedInterfaces();
        }
    }
}
