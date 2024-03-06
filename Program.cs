using System.Text;
using Blog;
using Blog.Data;
using Blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // autenticacao padrao Bearer
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // token/autenticacao gerados neste servidor
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
builder.Services.AddDbContext<BlogDataContext>();

builder.Services.AddTransient<TokenService>();

var app = builder.Build();

Configuration.JwtKey = app.Configuration.GetValue<string>("JwtKey");
Configuration.ApiKey = app.Configuration.GetValue<string>("ApiKey");
Configuration.ApiKeyName = app.Configuration.GetValue<string>("ApiKeyName");

var smtp = new Configuration.SmtpConfiguration();
app.Configuration.GetSection("Smtp").Bind(smtp); // ira pegar a section (objeto smtp no json) e converter para a classe
Configuration.Smtp = smtp;

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
