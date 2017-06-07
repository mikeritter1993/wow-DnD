using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace sql_and_interactive_window
{
    class MultiplayerConnectionHelper
    {
        private TcpClient client;
        private NetworkStream stream;
        private String ipAddress;
        private int portNum;
        private byte[] message;
        private bool connected;

      

        public MultiplayerConnectionHelper(String ipAddress, int portNum)
        {
            this.connected = false;
            this.ipAddress = ipAddress;
            this.portNum = portNum;

            client = new TcpClient();
            client.SendBufferSize = Program.NETWORK_MESSAGE_SIZE;
            client.ReceiveBufferSize = Program.NETWORK_MESSAGE_SIZE;

            stream = null;
            message = null;

        }

        public MultiplayerConnectionHelper()
        {
            connected = false;
            ipAddress = "";
            portNum = 0;

            client = new TcpClient();
            client.SendBufferSize = Program.NETWORK_MESSAGE_SIZE;
            client.ReceiveBufferSize = Program.NETWORK_MESSAGE_SIZE;
            
            stream = null;
            message = null;
        }

        public bool TryConnection()
        {
            client.Connect(ipAddress, portNum);
            if (client.Connected)
            {
                stream = client.GetStream();
                connected = true;
                return true;
            }
            else
            {
                connected = false;
                return false;
            }
        }

        public void SendMessage(String userMessage)
        {
            if (CheckMessage(userMessage))
            {
                message = System.Text.Encoding.ASCII.GetBytes(userMessage);
                stream.Write(message, 0, message.Length);
            }
            else
            {
                //add error
            }
            

        }

        private bool CheckMessage(String userMessage)
        {
            if (userMessage.Length >= Program.NETWORK_MESSAGE_SIZE)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string IpAddress
        {
            get
            {
                return ipAddress;
            }

            set
            {
                ipAddress = value;
            }
        }

        public int PortNum
        {
            get
            {
                return portNum;
            }

            set
            {
                portNum = value;
            }
        }

        public bool Connected
        {
            get
            {
                return connected;
            }

            set
            {
                connected = value;
            }
        }
    }
}
