using LMS.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly LMSDbContext dbContext;

        public NotificationsController(LMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Get all notifications
        // GET: api/notifications
        [HttpGet]
        public IActionResult GetAll()
        {
            var notifications = dbContext.Notifications.ToList();
            return Ok(notifications);
        }
    }
}
