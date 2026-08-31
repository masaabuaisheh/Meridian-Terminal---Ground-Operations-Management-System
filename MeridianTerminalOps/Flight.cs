using System;
using System.Collections.Generic;
using System.Text;

namespace MeridianTerminalOps
{
    internal class Flight
    {
        public string FlightNumber { get; set; }
        public FlightType Type { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public Gate? Gate { get; set; }
        public int SeatCapacity { get; set; }
        public FlightStatus Status { get; set; }

        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}

/*
 Flight MT-210
   │
   ├── Booking 1 → Passenger A → Confirmed
   ├── Booking 2 → Passenger B → Confirmed
   ├── Booking 3 → Passenger C → Confirmed
   └── Booking 4 → Passenger D → Standby
 
 */
