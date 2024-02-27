using Blog.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true; // ira desabilitar a validacao automatica do ASP.NET, assim permitindo que usemos o nosso retorno padrao
    });
builder.Services.AddDbContext<BlogDataContext>();

var app = builder.Build();
app.MapControllers();

app.Run();
