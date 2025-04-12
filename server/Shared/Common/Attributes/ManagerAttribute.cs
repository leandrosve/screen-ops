using Microsoft.AspNetCore.Authorization;

namespace Common.Attributes
{
    public class ManagerAttribute : AuthorizeAttribute
    {
        public ManagerAttribute()
        {
            Roles = "ADMIN,MANAGER";
        }
    }
}
