﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TCPClient
{
    class Client
    {
        private Socket socket;
        private IPAddress IP;
        private int Port;
        private int buffer;
        private byte[] buffers;

        public Client(IPAddress ip, int port, int size = 5000)
        {
            IP = ip;
            Port = port;
            buffer = size;
            buffers = new byte[buffer];
        }

        public bool Connect()
        {           
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                socket.Connect(new IPEndPoint(IP,Port));
                MessageBox.Show("Kết nối thành công với sever", "THÔNG BÁO", MessageBoxButtons.OK);
                return true;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối với server!", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        //////////
        public bool Send(string message)
        {
            try
            {
                socket.Send(Encoding.UTF8.GetBytes(message));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public byte[] Recieve()
        {
            
            int bytes = socket.Receive(buffers, buffer, SocketFlags.None);
            byte[] req = new byte[bytes];
            Array.Copy(buffers, req, bytes);

            return req;
        }
    }
}
 