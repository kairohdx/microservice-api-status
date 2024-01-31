using Microsoft.AspNetCore.SignalR;

public interface IApiStatusHub
{
    void SendStatusUpdate(ApiStatus status);
}

public interface IApiService
{
}

public interface ISocketService
{
    Task<IList<ApiConfig>> GetApisBySocketIndex(int socketIndex);
    Task<SocketConfig> getBySocketIndex(int socketIndex);
}