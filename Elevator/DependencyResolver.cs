using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    public class DependencyResolver
    {
        private static IWindsorContainer _container;

        //Initialize the container
        public static void Initialize()
        {
            _container = new WindsorContainer();
            _container.Register(new ComponentRegistration());
        }

        //Resolve types
        public static T For<T>()
        {
            return _container.Resolve<T>();
        }
    }

    //public class RepositoriesInstaller : IWindsorInstaller
    //{
    //    public void Install(IWindsorContainer container, IXElevator elevator)
    //    {
    //        container.Register(Classes.FromThisAssembly()
    //            .Where(Component.IsInSameNamespaceAs<EventRepository>())
    //            .WithService.DefaultInterfaces()
    //            .LifestyleTransient()
    //            .Configure(c => c.DependsOn(new { id = 0 }))
    //        );
    //    }
    //}

}
