using Microsoft.AspNetCore.SignalR;

namespace WEB_API.Helpers
{
    public class NotHub : Hub
    {
        public async Task SendNotification(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}
