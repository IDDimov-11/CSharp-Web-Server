using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BeautyCode
{
    class Program
    {
        public static async Task Main()
        {
            // http://localhost:9090

            var address = IPAddress.Parse("127.0.0.1");
            var port = 9090;

            var serverListener = new TcpListener(address, port);

            serverListener.Start();

            Console.WriteLine($"Server started on port {port}...");
            Console.WriteLine("Listening for requests...");

            while (true)
            {

                var connection = await serverListener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();

                var content = "Hi";
                var contentLength = Encoding.UTF8.GetByteCount(content);

                var response = $@"HTTP/1.1 200 OK 
Server: My Web Server
Date: {DateTime.UtcNow:r}
Content-Length: {contentLength}
Content-Type: text/html; charset=UTF-8

{content}";

                var responseBytes = Encoding.UTF8.GetBytes(response);

                await networkStream.WriteAsync(responseBytes);

                connection.Close();
            }
        }
    }
}
