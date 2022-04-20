using Microsoft.AspNetCore.Authentication.Cookies;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FleetManagement.Web.Extensions
{
    public static class Extensions
    {
        public static string GetEnumDisplayName(this Enum enumType)
        {
            // get the attribute for the enum value so that we can show a human readable display text instead of just the enum value
            return enumType.GetType().GetMember(enumType.ToString())
                           .First()
                           .GetCustomAttribute<DisplayAttribute>()
                           .Name;
        }

        public static void AddCookieAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options => {
                        options.AccessDeniedPath = "/Users/ErrorNotAuthorised";
                        options.LoginPath = "/Users/Login";
                    });
        }
    }
}
