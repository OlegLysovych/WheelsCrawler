using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WheelsCrawler.API.Controllers
{
    public class AdminController : BaseApiController
    {
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-roles")]
        public ActionResult GetUsersWithRoles()
        {
            return Ok("Only admins can see this");
        }

        [Authorize(Policy = "ModerateUrlsRole")]
        [HttpGet("urls-to-moderate")]
        public ActionResult GetUrlsForModeration()
        {
            return Ok("Admins or moderators can see this");
        }

    }
}