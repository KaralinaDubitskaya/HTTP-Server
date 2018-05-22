using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTP_Server
{
    public enum StatusCode
    {
        OK = 200,
        InternalServerError = 500,
        NotFound = 404,
        BadRequest = 400
    }

    public class Response
    {
        private StatusCode _statusCode;
        private string _contentType;
        private byte[] _content;
        private RequestMethod _method;

        public Response(Request request)
        {
            if (request == null)
            {
                _statusCode = StatusCode.BadRequest;
                _contentType = "text/html";
                _content = GetFileContent(Configuration.BadRequestDefaultPageName);
            }

            if (request.Method == RequestMethod.GET || request.Method == RequestMethod.HEAD)
            {
                _method = request.Method;
                _content = GetFileContent(request.URL);

                if (_content != null)
                {
                    _statusCode = StatusCode.OK;
                    _contentType = "text/html";
                }
                else
                {
                    _statusCode = StatusCode.NotFound;
                    _contentType = "text/html";
                    _content = GetFileContent(Configuration.NotFoundDefaultPageName);
                }
            }
        }

        public Response(StatusCode statusCode, string contentType, byte[] content)
        {
            _statusCode = statusCode;
            _contentType = contentType;
            _content = content;
        }

        public override string ToString()
        {
            string response = "";
            // Status line
            response += string.Format("HTTP 1.0 {0} {1}\r\n", ((int)_statusCode).ToString(), _statusCode.ToString());

            // Headers
            response += "Content-Type: " + _contentType + "\r\n";
            response += "Content-Length: " + _content?.Length + "\r\n";
            response += "Date: " + DateTime.Now + "\r\n\r\n";

            if (_method == RequestMethod.GET)
            {
                try
                {
                    response += "\r\n" + System.Text.Encoding.UTF8.GetString(_content);
                }
                catch (Exception ex) { }
            }

            return response;
        }

        private byte[] GetFileContent(string request)
        {
            string filePath = Environment.CurrentDirectory + Configuration.WebDir + request;
            FileInfo file = new FileInfo(filePath);

            if (file.Exists && file.Extension.Contains("."))
            {
                FileStream fileStream = file.OpenRead();
                BinaryReader reader = new BinaryReader(fileStream);
                Byte[] data = new Byte[file.Length];
                reader.Read(data, 0, data.Length);
                fileStream.Close();
                return data;
            }
            else
            {
                return null;
            }
        }
    }
}
