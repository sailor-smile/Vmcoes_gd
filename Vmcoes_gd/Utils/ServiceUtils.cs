using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vmcoes_gd.Utils
{
    public class ServiceUtils
    {
        public static IServiceCollection services;
        public static T GetService<T>()
        {
            return services.BuildServiceProvider().GetService<T>();
        }
    }
}
