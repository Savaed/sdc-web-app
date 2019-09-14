using Autofac;
using SDCWebApp.Controllers;
using SDCWebApp.Services;
using System.Linq;
using System.Reflection;

namespace SDCWebApp.Helpers
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var assembly = Assembly.GetExecutingAssembly();

            // Register services that inherit directly from the ServiceBase class.
            RegisterServices(builder, assembly);

            // Register controllers that inherit directly from the CustomApiController class.
            RegisterControllers(builder, assembly);

            // Register other types.
            RegisterOtherTypes(builder, assembly);
        }


        #region Privates

        private void RegisterServices(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsClass && t.BaseType == typeof(ServiceBase))
                .AsSelf()
                .Keyed<IServiceBase>(t => $"I{t.Name}")
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }

        private void RegisterControllers(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
               .Where(t => t.IsClass && t.BaseType == typeof(CustomApiController))
               .AsSelf()
               .Keyed<CustomApiController>(t => $"I{t.Name}")
               .AsImplementedInterfaces()
               .InstancePerRequest();
        }

        private void RegisterOtherTypes(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
               .AsSelf()
               .AsImplementedInterfaces()
               .InstancePerDependency();
        }

        #endregion

    }
}
