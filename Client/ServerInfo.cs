using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class ServerData
    {
        public string address { get; init; }
        
        public int port { get; init; }

        public int roomNumber { get; init; }

        public int userInRoom { get; init; }

        public override string ToString()
        {
            return $"Комната {roomNumber} ({userInRoom}/2)";
        }
    }
}
