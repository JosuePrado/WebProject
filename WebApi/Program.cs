using WebProject.Infraestructure;
using WebProject.RequestPipeline;
using WebProject.Services.WebSocket.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddInfraestructure(builder.Configuration);

var app = builder.Build();
app.InitializeDatabase();
app.InitializeWebSocket();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();