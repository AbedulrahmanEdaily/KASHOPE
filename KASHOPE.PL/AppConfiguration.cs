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
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ISeedData, RoleSeedData>();
            services.AddScoped<ISeedData, UserSeedData>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<ICheckoutService, CheckoutService>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
        }
    }
}
