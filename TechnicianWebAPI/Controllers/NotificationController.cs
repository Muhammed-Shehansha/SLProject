using Microsoft.AspNetCore.Mvc;
using TmsWebApi.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TmsWebApi.Models;

namespace TmsWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(INotificationService notificationService, ILogger<NotificationController> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        [HttpGet("GetNotifications")]
        [Authorize(Roles = "Technician")]
        public async Task<IActionResult> GetNotifications()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { message = "User ID not found in token." });
            }

            var notifications = await _notificationService.GetNotificationsByUserIdAsync(int.Parse(userId));
            return Ok(notifications);
        }

        [HttpPatch("MarkAsRead/{id}")]
        [Authorize(Roles = "Technician")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _notificationService.MarkNotificationAsReadAsync(id);
            return Ok(new { message = "Notification marked as read." });
        }

        [HttpPost("AddNotification")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNotification(Notification notification)
        {
            await _notificationService.AddNotificationAsync(notification);
            return CreatedAtAction(nameof(GetNotifications), new { id = notification.Id }, notification);
        }

        [HttpDelete("DeleteNotification/{id}")]
        [Authorize(Roles = "Technician")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            await _notificationService.DeleteNotificationAsync(id);
            return Ok(new { message = "Notification deleted successfully!" });
        }
    }
}
