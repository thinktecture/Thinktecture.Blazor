using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.Blazor.PwaUpdate.Services;

namespace Thinktecture.Blazor.PwaUpdate
{
    public static class PwaUpdateServiceCollectionExtensions
    {
        public static IServiceCollection AddUpdateService(this IServiceCollection services)

        {
            return services.AddScoped<IUpdateService, UpdateService>();
        }
    }
}
