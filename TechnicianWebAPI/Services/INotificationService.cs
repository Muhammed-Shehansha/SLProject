using TmsWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TmsWebApi.Services
{
    public interface INotificationService
    {
        Task<List<Notification>> GetNotificationsByUserIdAsync(int userId);
        Task AddNotificationAsync(Notification notification);
        Task MarkNotificationAsReadAsync(int notificationId);
        Task DeleteNotificationAsync(int notificationId);
    }
}
