using MyWebServer.Server;
using System.Threading.Tasks;

namespace BeautyCode
{
    class StartUp
    {
        public static async Task Main()
        {
            // http://localhost:9090

            var server = new HttpServer("127.0.0.1", 9090);

            await server.Start();
        }
    }
}
