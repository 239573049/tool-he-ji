using ApiTest;
using Token.HttpClientHelper;
using Token.Inject;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoInject(typeof(ApiTestTag));
builder.Services.AddControllers();
builder.Services.AddSingleton(new TokenHttp(new HttpClient()));
var app = builder.Build();


app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
