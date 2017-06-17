/*********************************************************************
Class: MultiplayerConnectionHelper
Description: Handels client connection to servers and communication with the server
Status: pretty much complete
TO DO: add some try catches and some timeout debugging stuff
**********************************************************************/
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace DnD
{
    class MultiplayerConnectionHelper
    {
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
            this.userName = "";

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
            this.userName = "";

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
                    PrintMessage(mesgFromServer);
                }
            }
        }

        public void PrintMessage(String message)    //change once message box is made
        {
            Console.WriteLine(message);
        }

        public bool TryConnection() //connects to the server givin whats inside data members ipAddress and portNum
        {
            client.Connect(ipAddress, portNum);
            if (client.Connected)
            {
                stream = client.GetStream();
                connected = true;
                Thread listen = new Thread(ListenToServer);
                listen.Start();
                return true;
            }
            else
            {
                //$$prolly need some more error stuff here
                connected = false;
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
            String rValue = userName + ": " + message + Program.NETWORK_MESSAGE_DELIMITER;
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
    }
}
