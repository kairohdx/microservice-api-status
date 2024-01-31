using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("ApiStatus")]
public class ApiStatusController : ControllerBase
{
    private readonly SocketUpdateService _socketUpdateService;
    private readonly ApiService _apiService;

    public ApiStatusController(SocketUpdateService socketUpdateService, ApiService apiService)
    {
        _socketUpdateService = socketUpdateService;
        _apiService = apiService;
    }

    public IActionResult status()
    {
        return Ok(new { mensagem = "Ok!" });
    }

    [HttpPost("refresh/{socketIndex}")]
    public IActionResult UpdateApis(int socketIndex)
    {
        _ = Task.Run(() => _socketUpdateService.UpdateData(socketIndex));

        return Ok(new { mensagem = $"Atualização iniciada para o Socket Index {socketIndex}" });
    }
}
