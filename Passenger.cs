using System;
using System.Collections.Generic;
using System.Text;

namespace MeridianTerminalOps
{
    internal class Passenger
    {
        public int PassengerID { get; set; }
        public string Name { get; set; }
        public PassengerCategory Category { get; set; }
        public Flight? ConnectingFlight { get; set; }
    }
}
