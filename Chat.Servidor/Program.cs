using Chat.Servidor.Data;
using Chat.Servidor.Endpoints;
using Chat.Servidor.Hubs;
using Chat.Servidor.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = new SqliteConnection("Data Source=:memory:");
connection.Open();

builder.Services.AddSingleton(connection);
builder.Services.AddScoped<SalaService>();
builder.Services.AddScoped<MensajeService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddDbContext<ChatDbContext>((services, options) =>
{
    var dbConnection = services.GetRequiredService<SqliteConnection>();
    options.UseSqlite(dbConnection);
});
builder.Services.AddSignalR();

var app = builder.Build();
// Crear las tablas si no existen.
await using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ChatDbContext>();
    await db.Database.EnsureCreatedAsync();
}
app.MapHub<ChatHub>("/hubs/chat");
app.MapUsuarioEndpoint();
app.MapSalaEndpoint();
app.Run();
