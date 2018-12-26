using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using KC.FileMan.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using System.Linq;

namespace KC.FileMan.Web
{
    /// <summary>
    /// 依赖注入配置
    /// </summary>
    public class DependencyConfig
    {
        private static IContainer container;

        public static IContainer Configure(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            // intercepter
            builder.RegisterAssemblyTypes(typeof(IntercepterBase).Assembly)
                    .Where(x => x.IsSubclassOf(typeof(IntercepterBase)));

            // session factory
            builder.Register(x => ModelBuilder.GetSessionFactory("KC_FileMan"))
                    .As<ISessionFactory>()
                    .InstancePerLifetimeScope();

            // repository
            builder.RegisterAssemblyTypes(typeof(KC.FileMan.Repository.RepositoryBase<>).Assembly)
                        .AsImplementedInterfaces()
                        .InstancePerLifetimeScope();


            // application
            builder.RegisterAssemblyTypes(typeof(KC.FileMan.Application.AppBase).Assembly)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope()
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(DataSessionInterceptor))//数据库会话拦截
                    .InterceptedBy(typeof(TransactionInterceptor));//事物拦截

            builder.Populate(services);

            container = builder.Build();
            return container;
        }
    }
}