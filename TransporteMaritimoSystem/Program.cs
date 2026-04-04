using Microsoft.AspNetCore.Authentication.Cookies;

namespace TransporteMaritimoSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controladores MVC + Views
            builder.Services.AddControllersWithViews();

            // Soporte de sesión (Almacena Token JWT inmediatamente después del login)
            builder.Services.AddSession();

            // Authentication (COOKIE)
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Login";
                });

            var app = builder.Build();

            // Manejo de Errores
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            // Habilitar sesiones
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            // Ruta Default → LOGIN PAGE
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{id?}");

            app.Run();
        }
    }
}