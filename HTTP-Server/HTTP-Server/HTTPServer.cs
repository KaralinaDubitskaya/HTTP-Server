using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HTTP_Server
{
    public class HTTPServer
    {
        private Socket _socket;
        public delegate void LogMessageDelegate(string msg);
        public LogMessageDelegate LogMessage { get; set; }
        private byte[] _buffer = new byte[1024];

        public HTTPServer(int port, LogMessageDelegate LogMsg)
        {
            LogMessage = LogMsg;

            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                EndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
                _socket.Bind(endPoint);
                _socket.Listen(5);
                _socket.BeginAccept(new AsyncCallback(OnAccept), null);
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
            }
        }

        public void OnAccept(IAsyncResult ar)
        {
            // Create client socket
            Socket clientSocket = _socket.EndAccept(ar);

            // Infinite time-out period
            clientSocket.ReceiveTimeout = 0;

            //Start listening for more clients
            _socket.BeginAccept(new AsyncCallback(OnAccept), null);

            //Once the client connects then start receiving the commands from her
            clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None,
                new AsyncCallback(OnReceive), clientSocket);
        }

        public void OnReceive(IAsyncResult ar)
        {
            Socket clientSocket = (Socket)ar.AsyncState;
            clientSocket.EndReceive(ar);

            // Create a request object
            string msg = Encoding.UTF8.GetString(_buffer);
            Request request = new Request(msg);
            LogMessage(msg);
            
            // Create response
            Response response;
            try
            {
                response = new Response(request);
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
                response = new Response(null);
            }

            // Send response back to client
            msg = response.ToString();
            byte[] message = Encoding.UTF8.GetBytes(msg);
            clientSocket.BeginSend(message, 0, message.Length, SocketFlags.None,
                                new AsyncCallback(OnSend), clientSocket);
            LogMessage(msg);

        }

        public void OnSend(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndSend(ar);

                // Close connection
                client.Close();
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
            }
        }
    }
}
