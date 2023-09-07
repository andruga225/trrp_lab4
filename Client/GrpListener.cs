using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using lab4.Services;

namespace lab4
{
    internal class GrpListener
    {
        public Server server;
        public Form1 game;

        public GrpListener(Form1 form)
        {
            game = form;
        }

        public void Start(string ipAddress, int port)
        {
            server = new Server
            {
                Services = { ScoreReceiver.ScoreState.BindService(new ScoreReceiverService(game)) },
                Ports = { new ServerPort(ipAddress, port, ServerCredentials.Insecure) }
            };
            server.Start();
        }

        public void Stop()
        {
            server.ShutdownAsync().Wait();
        }
    }
}
