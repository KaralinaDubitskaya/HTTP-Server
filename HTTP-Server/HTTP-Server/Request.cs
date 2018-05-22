using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTP_Server
{
    public enum RequestMethod
    {
        GET,
        POST,
        HEAD
    }

    public class Request
    {
        public RequestMethod Method { get; }
        public string URL { get; }

        public Request(string msg)
        {
            if (String.IsNullOrEmpty(msg))
            {
                throw new ArgumentException();
            }

            char[] separators = { ' ', '\n' };
            string[] tokens = msg.Split(separators);
            Method = (RequestMethod)Enum.Parse(typeof(RequestMethod), tokens[0]);
            URL = tokens[1].Replace("/", "\\");
        }
    }
}
