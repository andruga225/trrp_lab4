namespace Dispatcher
{
    public class ServerConfig
    {
        public ServerConfig(string address, int port, int grpcPort)
        {
            this.Address = address;
            this.Port = port;
            this.GrpcPort = grpcPort;
        }
        public string Address { get; set; }
        public int Port { get; set; }
        public int GrpcPort { get; set; }
    }
}