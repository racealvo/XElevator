using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    public class ComponentRegistration : IRegistration
    {
        public void Register(IKernelInternal kernel)
        {
            kernel.Register(
                Component.For<LoggingInterceptor>()
                    .ImplementedBy<LoggingInterceptor>());

            kernel.Register(
                Component.For<IXElevator>()
                         .ImplementedBy<XElevator>()
                         .Interceptors(InterceptorReference.ForType<LoggingInterceptor>()).Anywhere);
        }
    }
}
