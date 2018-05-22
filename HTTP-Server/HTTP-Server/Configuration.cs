using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTP_Server
{
    public static class Configuration
    {
        public static string ServerHTTPVersion = "HTTP/1.1";
        public static int Port = 8081;
        public static string WebDir = "\\web";
        public static string PageNotFound = "\\NotFound.html";
    }
}
