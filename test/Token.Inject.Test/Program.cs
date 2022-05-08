using ApiTest;
using Token.Inject;
using Token.HttpClientHelper;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoInject(typeof(ApiTestTag));
var app = builder.Build();

var http = new TokenHttp(new HttpClient(){ BaseAddress= new Uri("http://baidu.com") });
var result=await http.PostAsync("baidu", "{\"1\":1}");
app.MapGet("/", () => "Hello World!");

app.Run();
