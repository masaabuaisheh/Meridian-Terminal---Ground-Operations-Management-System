using System;
using System.Collections.Generic;
using System.Text;

namespace MeridianTerminalOps
{
    internal class Booking
    {
        public int BookingId { get; set; }
        public Passenger Passenger { get; set; }
        public Flight Flight { get; set; }
        public BookingStatus Status { get; set; }
        public bool IsBoarded { get; set; }
    }
}
