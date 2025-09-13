
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
