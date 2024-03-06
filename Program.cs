using System.Text;
using Blog;
using Blog.Data;
using Blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
ConfigureAuthentication(builder);
ConfigureMvc(builder);
ConfigureServices(builder);


var app = builder.Build();

LoadConfiguration(app);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void LoadConfiguration(WebApplication webApp)
{
    Configuration.JwtKey = webApp.Configuration.GetValue<string>("JwtKey");
    Configuration.ApiKey = webApp.Configuration.GetValue<string>("ApiKey");
    Configuration.ApiKeyName = webApp.Configuration.GetValue<string>("ApiKeyName");

    var smtp = new Configuration.SmtpConfiguration();
    webApp.Configuration.GetSection("Smtp").Bind(smtp); // ira pegar a section (objeto smtp no json) e converter para a classe
    Configuration.Smtp = smtp;
}

void ConfigureAuthentication(WebApplicationBuilder webAppBuilder)
{
    var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
    webAppBuilder.Services.AddAuthentication(x =>
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
}

void ConfigureMvc(WebApplicationBuilder webAppBuilder)
{
    webAppBuilder.Services
        .AddControllers()
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
}

void ConfigureServices(WebApplicationBuilder webAppBuilder)
{
    webAppBuilder.Services.AddDbContext<BlogDataContext>();
    webAppBuilder.Services.AddTransient<TokenService>();
}
