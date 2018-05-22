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
        public string Content { get; }

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

            if (Method == RequestMethod.POST)
            {
                int length = 0;
                for (int i = 0; i < tokens.Length; i++)
                {
                    if (tokens[i] == "content-length:")
                    {
                        length = Int32.Parse(tokens[i + 1]);
                    }
                }

                string separator = "\r\n\r\n";
                Content = msg.Substring(msg.IndexOf(separator) + separator.Length, length);
            }
        }
    }
}
