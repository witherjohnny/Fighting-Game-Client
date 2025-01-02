using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Fighting_Game_Client.Data
{
    internal class UdpClientSingleton
    {
        private static UdpClient _instance;
        private static readonly object _lock = new object();
        private UdpClientSingleton() { }

        public static UdpClient Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new UdpClient();
                    }
                    return _instance;
                }
            }
        }
    }
}
