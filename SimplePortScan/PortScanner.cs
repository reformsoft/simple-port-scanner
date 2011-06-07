using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace SimplePortScan
{
    public class PortScanner
    {
        private IPAddress _target;
        public int Port { get; private set; }
        public bool IsOpen { get; private set; }

        public PortScanner(IPAddress target, int port)
        {
            _target = target;
            Port = port;
        }

        public void ScanPort()
        {
            using (TcpClient TcpClient = new TcpClient())
            {
                try
                {
                    TcpClient.Connect(_target, Port);
                }
                catch (SocketException)
                {
                    IsOpen = false;
                }

                using (NetworkStream ClientStream = TcpClient.GetStream())
                {
                    IsOpen = true;
                }
            }
        }
    }
}
