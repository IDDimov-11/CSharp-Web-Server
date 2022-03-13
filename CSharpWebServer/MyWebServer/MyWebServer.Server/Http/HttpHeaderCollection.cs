
using System.Collections.Generic;

namespace MyWebServer.Server.Http
{
    public class HttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection() => this.headers = new Dictionary<string, HttpHeader>();
    }
}
