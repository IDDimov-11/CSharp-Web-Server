using System.Collections.Generic;

namespace MyWebServer.Server.Http
{
    public class HtttpRequest
    {
        public HttpMethod Method { get; private set; }

        public string Url { get; private set; }

        public HttpHeaderCollection Headers { get; } = new HttpHeaderCollection();

        public string Body { get; private set; }
    }
}
