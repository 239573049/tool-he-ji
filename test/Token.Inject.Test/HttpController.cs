using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Token.HttpClientHelper;

namespace Token.Inject.Test;
[Route("[controller]")]
[ApiController]
public class HttpController : ControllerBase
{
    private readonly TokenHttp _http;
    public HttpController(TokenHttp http){
    _http = http;
    }

    [HttpGet]
    public async Task Get(){
        var s=await _http.PostAsync("baidu","{\"json\":1}");
    }
}
