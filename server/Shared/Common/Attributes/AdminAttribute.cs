using Microsoft.AspNetCore.Authorization;

namespace Common.Attributes
{
    public class AdminAttribute : AuthorizeAttribute
    {
        public AdminAttribute()
        {
            Roles = "ADMIN";
        }
    }
}
