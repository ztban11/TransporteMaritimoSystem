using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TransporteMaritimoSystem.Helpers
{
    public static class PermissionHelper
    {
        public static bool TieneRol(HttpContext context, string rol)
        {
            var rolesJson = context.Session.GetString("Roles");

            if (string.IsNullOrEmpty(rolesJson))
                return false;

            var roles = JsonSerializer.Deserialize<List<string>>(rolesJson);

            return roles != null && roles.Contains(rol);
        }
    }
}
