using Chat;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddSignalR();

var app = builder.Build();
app.MapHub<ChatHub>("/Chat");
app.Run();
