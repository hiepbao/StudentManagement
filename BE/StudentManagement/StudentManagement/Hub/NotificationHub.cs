namespace StudentManagement.Hub;
using Microsoft.AspNetCore.SignalR;

public class NotificationHub : Hub
{
    public async Task NotifyUsersUpdated()
    {
        await Clients.All.SendAsync("UsersUpdated");
    }

    public async Task NotifyDataChanged(string dataType)
    {
        await Clients.All.SendAsync("DataChanged", dataType);
    }
}

