using ApiTest;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Token.Application;
using Token.HttpClientHelper;
using Token.Inject;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoInject(typeof(ApiTestTag),typeof(TokenApplicationModule));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(delegate (SwaggerGenOptions option)
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "api",
        Description = "Token Test api",
        Contact = new OpenApiContact
        {
            Name = "Token",
            Email = "239573049@qq.com",
            Url = new Uri("https://github.com/239573049")
        }
    });
    string[] files = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");//获取api文档
    string[] array = files;
    foreach (string filePath in array)
    {
        option.IncludeXmlComments(filePath, includeControllerXmlComments: true);
    }

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "输入你的Token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
});

builder.Services.AddSingleton(new TokenHttp(new HttpClient()));
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Management.WebApi");
        c.DocExpansion(DocExpansion.None);
        c.DefaultModelsExpandDepth(-1);
        c.RoutePrefix = string.Empty;
    });
}
app.MapControllers();


app.Run();
