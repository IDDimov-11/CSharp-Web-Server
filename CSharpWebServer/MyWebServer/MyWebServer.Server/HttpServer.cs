using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener listener;

        public HttpServer(string ipAddress, int port)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            listener = new TcpListener(this.ipAddress, port);
        }

        public async Task Start()
        {
            //var serverListener = new TcpListener(ipAddress, port);

            this.listener.Start();

            Console.WriteLine($"Server started on port {port}...");
            Console.WriteLine("Listening for requests...");

            while (true)
            {

                var connection = await this.listener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();

                var request = await this.ReadRequest(networkStream);
                Console.WriteLine(request);

                await WriteResponse(networkStream);

                connection.Close();
            }
        }
        private async Task<string> ReadRequest(NetworkStream networkStream)
        {

            var bufferLenght = 1024;
            var buffer = new byte[bufferLenght];

            //var totalBytesRead = 0;

            var requestBuilder = new StringBuilder();

            while (networkStream.DataAvailable)
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLenght);

                //totalBytesRead += bytesRead;

                //if (totalBytesRead > 10 * 1024)
                //{
                //    connection.Close();
                //}

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }

            return requestBuilder.ToString();
        }

        private async Task WriteResponse(NetworkStream networkStream)
        {

            //var content = "Hi";
            // to prevent fav icon error
            var content = @"
<html>
    <head>        
        <link rel=""icon"" href=""data:,"">
    </head>
    <body>
        Hello from IDD
    </body>
</html>";

            var contentLength = Encoding.UTF8.GetByteCount(content);

            var response = $@"HTTP/1.1 200 OK 
Server: My Web Server
Date: {DateTime.UtcNow:r}
Content-Length: {contentLength}
Content-Type: text/html; charset=UTF-8

{content}";

            var responseBytes = Encoding.UTF8.GetBytes(response);

            await networkStream.WriteAsync(responseBytes);
        }
    }
}
