using System.Net.WebSockets;
using WebSocketServer;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseWebSockets();

app.Use(async (context, next) =>
{
    if (!context.WebSockets.IsWebSocketRequest)
    {
        // WebSocket���N�G�X�g�łȂ��ꍇ�́A�ʏ��HTTP���N�G�X�g����������
        await next();
    }
    if (context.Request.Path == "/ws")
    {
        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
        WebSocketHandle handler = new();
        await handler.Execute(webSocket);
    }
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Started WebScokertServer");
    });
});

app.Run();
