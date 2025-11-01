
using Microsoft.EntityFrameworkCore;
using RegistroDeJugadoresTicTacToe.Components;
using RegistroDeJugadoresTicTacToe.DAL;
using RegistroDeJugadoresTicTacToe.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var ConnectionStrings = builder.Configuration.GetConnectionString("SqlConStr");
builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(ConnectionStrings));

builder.Services.AddScoped<JugadoresService>();
builder.Services.AddScoped<PartidasService>();
builder.Services.AddScoped<MovimientosService>();
builder.Services.AddHttpClient<IJugadoresApiService, JugadoresApiService>(client =>
{
    client.BaseAddress = new Uri("https://gestionhuacalesapi.azurewebsites.net/");
});

builder.Services.AddHttpClient<IPartidaApiService, PartidaApiService>(client =>
{
    client.BaseAddress = new Uri("https://gestionhuacalesapi.azurewebsites.net/");
});

builder.Services.AddHttpClient<IMovimientosApiService, MovimientosApiService>(client =>
{
    client.BaseAddress = new Uri("https://gestionhuacalesapi.azurewebsites.net/");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
   
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
