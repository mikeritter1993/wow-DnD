using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace sql_and_interactive_window
{
    class MultiplayerHostHelper
    {
        private List<TcpClient> clientList;
        private TcpListener host;

        public MultiplayerHostHelper()
        {
            clientList = new List<TcpClient>();
            host = new TcpListener(new IPAddress(System.Text.Encoding.ASCII.GetBytes("127.0.0.1")), 8888);
        }


        


    }
}
