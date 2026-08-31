using System;
using System.Collections.Generic;
using System.Text;

namespace MeridianTerminalOps
{
    internal class Baggage
    {
        public int BagId { get; set; }
        public double BagWeight { get; set; }
        public Passenger Passenger { get; set; }
        public Flight Flight { get; set; }
    }
}
