using Microsoft.AspNetCore.Mvc;
using Token.HttpClientHelper;

namespace Token.Inject.Test;
[Route("[controller]")]
[ApiController]
public class HttpController : ControllerBase
{
    private readonly TokenHttp _http;
    public HttpController(TokenHttp http)
    {
        _http = http;
    }

    [HttpGet]
    public async Task Get()
    {
        var file = System.IO.File.OpenRead(@"C:\Users\Administrator\Downloads\XunLeiWebSetup_ext.exe");
        var s = await _http.UploadingFile<string>("http://124.222.27.83:9000/api/Oss/uploading?uploadingType=0", file, "XunLeiWebSetup_ext.exe");
    }
}
