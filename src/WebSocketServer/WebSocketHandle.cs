using System.Net.WebSockets;
using System.Text;

namespace WebSocketServer
{
    public class WebSocketHandle
    {
        private static List<WebSocket> s_sockets = new List<WebSocket>();

        public async Task Execute(WebSocket webSocket)
        {
            s_sockets.Add(webSocket);
            while (webSocket.State == WebSocketState.Open)
            {
                var message = await ReceiveMessage(webSocket);
                await BroadcastMessage(message);
            }
        }

        private static async Task<string> ReceiveMessage(WebSocket socket)
        {
            byte[] buffer = new byte[1024 * 4];

            var result = await socket.ReceiveAsync(
                new ArraySegment<byte>(buffer),
                CancellationToken.None);

            return Encoding.UTF8.GetString(buffer, 0, result.Count);
        }

        private static async Task BroadcastMessage(string message)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(buffer);

            foreach (var socket in s_sockets)
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(
                        segment, WebSocketMessageType.Text,
                        true,
                        CancellationToken.None);
                }
            }
        }
    }
}
