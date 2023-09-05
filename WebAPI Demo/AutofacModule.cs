using Autofac;
using WebAPI_Demo.Services;

namespace WebAPI_Demo
{
    public sealed class AutofacModule:Module
    {
        protected void Load(ContainerBuilder builder)
        {
            //Add transient
            builder.RegisterType<service>().As<Iservice>()
                .InstancePerDependency();
        }
    }
}
