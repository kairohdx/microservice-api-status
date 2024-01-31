using Microsoft.AspNetCore.SignalR;

public class ApiStatusHub : Hub, IApiStatusHub
{
    public void SendStatusUpdate(ApiStatus status)
    {
        throw new NotImplementedException();
    }

    public async Task SendUpdate(string message)
    {
        await Clients.All.SendAsync("ReceiveUpdate", message);
    }
}
