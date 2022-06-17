using Microsoft.AspNetCore.Mvc;
using Token.Application;
using Token.Application.Contracts;
using Token.HttpClientHelper;

namespace Token.Inject.Test;
[Route("[controller]")]
[ApiController]
public class HttpController : ControllerBase
{
    private readonly TokenHttp _http;
    private readonly IDemoAppService _demoAppService;
    public HttpController(TokenHttp http, IDemoAppService demoAppService)
    {
        _http = http;
        _demoAppService = demoAppService;
    }

    [HttpGet]
    public async Task<string> Get()
    {
        var data= await _demoAppService.GetDemoDataAsync();
        return data;
    }
}
