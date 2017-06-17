/*********************************************************************
Class: MultiplayerHostHelper
Description: starts a server and handels connection of clients and communicating with them and each other
Status: pretty much complete
TO DO: add some try catches and some timeout debugging stuff, clients ending connection protection
**********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace DnD
{
    class MultiplayerHostHelper
    {
        private List<TcpClient> clientList;     //list of all clients connected
        private TcpListener host;               //the server
        public int numOfClients;                //how many clients are on the server

        public MultiplayerHostHelper()
        {
            numOfClients = 0;
            clientList = new List<TcpClient>();
            host = new TcpListener (8888);
        }

        private void ListenToClient(TcpClient client)   //listens to a client for messages to then be broadcast to all other clients, called on its own thread per client
        {
            byte[] buffer = new byte[Program.NETWORK_MESSAGE_SIZE];
            NetworkStream stream = client.GetStream();
            String clientMesg;
            while (true)
            {
                if (stream.DataAvailable)
                {
                    stream.Read(buffer, 0, Program.NETWORK_MESSAGE_SIZE);
                    clientMesg = System.Text.Encoding.ASCII.GetString(buffer);
                    clientMesg = CleanString(clientMesg);
                    BroadCast(clientMesg);
                    stream.Flush();
                }
            }

        }
        
        //$$edit make another function to be called on its own thread and listen for connections, change this to call that function and set stuff up
        public void StartSever()    //start the server and start listening for connections 
        {
            TcpClient hold = null;
            host.Start();

            while (true)
            {
                if (host.Pending())
                { 
                    hold = host.AcceptTcpClient();
                    clientList.Add(hold);
                    Thread startClient = new Thread(() => ListenToClient(hold));
                    startClient.Start();
                    numOfClients++;
                }
            }
        }
        private void BroadCast(String clientMessage)    //sends a message to all clients on the server
        {
            byte[] clientMesgByte = System.Text.Encoding.ASCII.GetBytes(clientMessage);
            foreach (TcpClient item in clientList)
            {
                NetworkStream stream = item.GetStream();
                stream.Write(clientMesgByte, 0, clientMesgByte.Length);
            }
        }


        private String CleanString(String toClean)  //takes all blank spaces off the end of a message before sending it NOTE: keeps the NETWORK_MESSAGE_DELIMITER at the end
        {
            int endPoint = toClean.IndexOf(Program.NETWORK_MESSAGE_DELIMITER) + 1;
            String rValue = toClean.Substring(0, endPoint);
            return rValue;
        }
    }
}
