using Blog.Data;
using Blog.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
builder.Services.AddDbContext<BlogDataContext>();

builder.Services.AddTransient<TokenService>(); // sempre vai criar uma nova instancia do servico
// builder.Services.AddScoped(); // ira criar a instancia por requisicao/metodo/action
// builder.Services.AddSingleton(); // tera uma instancia para a App toda

var app = builder.Build();
app.MapControllers();

app.Run();
