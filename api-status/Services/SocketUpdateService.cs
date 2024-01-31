
using System.Diagnostics;

public class SocketUpdateService
{
    private readonly ISocketService _socketService;
    private readonly IApiService _apiService;
    private readonly IApiStatusHub _apiStatusHub;

    public SocketUpdateService(ISocketService socketService, IApiService apiService, IApiStatusHub apiStatusHub)
    {
        _socketService = socketService;
        _apiService = apiService;
        _apiStatusHub = apiStatusHub;
    }

    public async Task UpdateData(int socketIndex)
    {
        SocketConfig socket = await _socketService.getBySocketIndex(socketIndex);
        IList<ApiConfig> socketApis = await _socketService.GetApisBySocketIndex(socketIndex);

        foreach(ApiConfig  api in socketApis){
            ApiStatus status = await CheckApiStatus(api);
            SendUpdatedDataInSocket(status);
        }

    }

    private async Task<ApiStatus> CheckApiStatus(ApiConfig api)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(api.ApiUrl);

                stopwatch.Stop(); //Para saber o ms real da requisição

                if (response.IsSuccessStatusCode)
                {
                    // A API está respondendo com sucesso
                    return new ApiStatus
                    {
                        ReturnStatus = (int)response.StatusCode,
                        Ms = (float)stopwatch.Elapsed.TotalMilliseconds, 
                        Timestamp = DateTime.Now,
                        StatusDescription = "Success"
                    };
                }
                else
                {
                    // A API retornou um status de erro
                    return new ApiStatus
                    {
                        ReturnStatus = (int)response.StatusCode,
                        Ms = (float)stopwatch.Elapsed.TotalMilliseconds,
                        Timestamp = DateTime.Now,
                        StatusDescription = "Error"
                    };
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                // Lidar com exceções, por exemplo, se a requisição falhar
                Console.WriteLine($"Erro ao verificar status da API {api.ApiName}: {ex.Message}"); // Log Temporario

                // Retorna um status de erro
                return new ApiStatus
                {
                    ReturnStatus = 500,
                    Ms = -1,
                    Timestamp = DateTime.Now,
                    StatusDescription = "Error"
                };
            }
        }
    }

    private void SendUpdatedDataInSocket(ApiStatus status)
    {
        try
        {
            _apiStatusHub.SendStatusUpdate(status);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar dados atualizados para o hub: {ex.Message}"); //Log temporario!
        }
    }
}
