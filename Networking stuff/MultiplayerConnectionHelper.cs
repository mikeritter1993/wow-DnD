/*********************************************************************
Class: MultiplayerConnectionHelper
Description: Handels client connection to servers and communication with the server
Status: pretty much complete
TO DO: add some try catches and some timeout debugging stuff, comments
**********************************************************************/
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;

namespace DnD
{
    class MultiplayerConnectionHelper
    {
        public delegate void Del(string message);
        private Del mesgFunction;
        private TcpClient client;
        private NetworkStream stream;
        private String ipAddress;
        private String userName;
        private int portNum;
        private bool connected;

      

        public MultiplayerConnectionHelper(String ipAddress, int portNum)
        {
            this.connected = false;
            this.ipAddress = ipAddress;
            this.portNum = portNum;
            this.UserName = "";

            client = new TcpClient();
            client.SendBufferSize = Program.NETWORK_MESSAGE_SIZE;
            client.ReceiveBufferSize = Program.NETWORK_MESSAGE_SIZE;

            stream = null;
            

        }

        public MultiplayerConnectionHelper()
        {
            connected = false;
            ipAddress = "";
            portNum = 0;
            this.UserName = "";

            client = new TcpClient();
            client.SendBufferSize = Program.NETWORK_MESSAGE_SIZE;
            client.ReceiveBufferSize = Program.NETWORK_MESSAGE_SIZE;

            stream = null;
            
        }

        private void ListenToServer()   //listens to the server for messages, called on its own thread
        {
            byte[] mesgFromServerByte = new byte[Program.NETWORK_MESSAGE_SIZE];
            String mesgFromServer = "";
            while(true)
            {
                if (stream.DataAvailable)
                {
                    stream.Read(mesgFromServerByte, 0, Program.NETWORK_MESSAGE_SIZE);
                    mesgFromServer = System.Text.Encoding.ASCII.GetString(mesgFromServerByte);
                    mesgFromServer = CleanString(mesgFromServer);
                    MesgFunction(mesgFromServer);
                }
            }
        }

        public void StartListening()
        {
            Thread listen = new Thread(ListenToServer);
            listen.Start();
        }
        

        public bool TryConnection() //connects to the server givin whats inside data members ipAddress and portNum
        {
            try
            {
                client.Connect(ipAddress, portNum);
                stream = client.GetStream();
                connected = true;
                return true;
            }
            catch (Exception)
            {
                connected = false;
                MessageBox.Show("Connection Failed, make sure the IP address is correct and the host has the port 8888 open.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
           
        }

        public void SendMessage(String userMessage) //sends a message to the connected server
        {
            userMessage = PrepMessage(userMessage);
            byte[] userMessageByte = System.Text.Encoding.ASCII.GetBytes(userMessage);
            stream.Write(userMessageByte, 0, userMessageByte.Length);
        }

        private String PrepMessage(String message)  //preps a message with clients user name at the front and a NETWORK_MESSAGE_DELIMITER at the end
        {
            String rValue = UserName + ": " + message + Program.NETWORK_MESSAGE_DELIMITER;
            return rValue;
        }

        private String CleanString(String toClean)  //takes out a NETWORK_MESSAGE_DELIMITER at the end of a message from the server
        {
            int endPoint = toClean.IndexOf(Program.NETWORK_MESSAGE_DELIMITER);
            String rValue = toClean.Substring(0, endPoint);
            return rValue;
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

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public Del MesgFunction
        {
            get
            {
                return mesgFunction;
            }

            set
            {
                mesgFunction = value;
            }
        }
    }
}
