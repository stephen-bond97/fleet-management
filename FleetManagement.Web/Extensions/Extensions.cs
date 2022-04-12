using Microsoft.AspNetCore.Authentication.Cookies;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FleetManagement.Web.Extensions
{
    public static class Extensions
    {
        public static string GetEnumDisplayName(this Enum enumType)
        {
            return enumType.GetType().GetMember(enumType.ToString())
                           .First()
                           .GetCustomAttribute<DisplayAttribute>()
                           .Name;
        }

        public static void AddCookieAuthentication(
            this IServiceCollection services,
            string notAuthorised = "/User/ErrorNotAuthorised", 
            string notAuthenticated = "/User/ErrorNotAuthenticated")
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options => {
                        options.AccessDeniedPath = notAuthorised;
                        options.LoginPath = notAuthenticated;
                    });
        }
    }
}
