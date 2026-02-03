using KASHOPE.BLL.Services.Classes;
using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.Repository.Classes;
using KASHOPE.DAL.Repository.Interfaces;
using KASHOPE.DAL.Utils;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace KASHOPE.PL
{
    public static class AppConfiguration
    {
        public static void Configuration(IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            var assemblyService = typeof(CategoryService).Assembly;
            var assemblyRepository = typeof(CategoryRepository).Assembly;
            services.Scan(s => s.FromAssemblies(assemblyService).AddClasses(c => c.AssignableTo<IScopedService>()).AsImplementedInterfaces().WithScopedLifetime());
            services.Scan(s => s.FromAssemblies(assemblyRepository).AddClasses(c => c.AssignableTo<IScopedRepository > ()).AsImplementedInterfaces().WithScopedLifetime());
            services.AddProblemDetails();
        }
    }
}
