using System.Reflection;
using Architecture.Core;
using Architecture.Core.Abstract.Repositories;
using Architecture.Core.Abstract.Services;
using Architecture.Core.Abstract.UnitOfWork;
using Architecture.Repository;
using Architecture.Repository.Concrete.Repositories;
using Architecture.Repository.Concrete.UnitOfWork;
using Architecture.Service.Abstract.Services;
using Autofac;
using Module = Autofac.Module;

namespace Architecture.API.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();


            var apiAssembly = Assembly.GetExecutingAssembly();
            var repositoryAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAseembly = Assembly.GetAssembly(typeof(Service<>));
            var coreAssembly = Assembly.GetAssembly(typeof(BaseDto));
            //var cachingAssembly = Assembly.GetAssembly(typeof());

            builder.RegisterAssemblyTypes(apiAssembly, repositoryAssembly, serviceAseembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repositoryAssembly, serviceAseembly)
                .Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
