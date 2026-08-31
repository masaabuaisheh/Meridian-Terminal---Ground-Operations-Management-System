using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeridianTerminalOps
{
    internal class TerminalOperations
    {
        public List<Flight> Flights { get; set; } = new List<Flight>();
        public List<Gate> Gates { get; set; } = new List<Gate>();
        public List<Passenger> Passengers { get; set; } = new List<Passenger>();
        public List<Baggage> Bags { get; set; } = new List<Baggage>();
        public List<GroundStaff> GroundStaff { get; set; } = new List<GroundStaff>();

        private const double BaggageAllowance = 30;
        private const double MinimumConnectionMinutes = 45;
        private const int MaximumDutyHours = 8;
        private const int StandbyCapacity = 10;
        private int nextAssignmentId = 1;
        private int nextBookingId = 1;


        // Flight & Gate Management
        public void RegisterFlight(string flightNumber, FlightType type, DateTime arrivalTime, DateTime departureTime, int seatCapacity)
        {
            if (string.IsNullOrWhiteSpace(flightNumber))
            {
                Console.WriteLine("Enter a valid Flight Number!");
                return;
            }

            if (departureTime <= arrivalTime)
            {
                Console.WriteLine("Departure time must be after arrival time.");
                return;
            }

            if (seatCapacity <= 0)
            {
                Console.WriteLine("Seat capacity must be greater than 0.");
                return;
            }

            if (Flights.Any(f => f.FlightNumber == flightNumber))
            {
                Console.WriteLine("A flight with this flight number already exists.");
                return;
            }

            Flight flight = new Flight();

            flight.FlightNumber = flightNumber;
            flight.Type = type;
            flight.ArrivalTime = arrivalTime;
            flight.DepartureTime = departureTime;
            flight.Gate = null;
            flight.SeatCapacity = seatCapacity;
            flight.Status = FlightStatus.Scheduled;

            Flights.Add(flight);

            Console.WriteLine("Flight registered successfully.");
        }

        public void RegisterGate(string gateIdentifier, bool supportsInternationalFlights)
        {
            if (string.IsNullOrWhiteSpace(gateIdentifier)) 
            {
                Console.WriteLine("Enter a valid gate identifier.");
                return;
            }
            if (Gates.Any(g => g.GateIdentifier == gateIdentifier))
            {
                Console.WriteLine("A gate with this identifier already exists.");
                return;
            }

            Gate gate = new Gate();

            gate.GateIdentifier = gateIdentifier;
            gate.SupportsInternationalFlights = supportsInternationalFlights;

            Gates.Add(gate);

            Console.WriteLine("Gate registered successfully.");

        }

        public void AssignGate(string flightNumber, string gateIdentifier) 
        {
            Flight? flight = Flights.FirstOrDefault(f => f.FlightNumber == flightNumber);
            Gate? gate = Gates.FirstOrDefault(g => g.GateIdentifier == gateIdentifier);

            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            if (gate == null)
            {
                Console.WriteLine("Gate not found.");
                return;
            }

            if (flight.Type == FlightType.International && !gate.SupportsInternationalFlights)
            {
                Console.WriteLine("Rejected! This gate does not support international flights.");
                return;
            }

            bool hasConflict = Flights.Any(otherFlight =>
                otherFlight.Gate == gate &&
                otherFlight != flight &&
                flight.ArrivalTime < otherFlight.DepartureTime &&
                flight.DepartureTime > otherFlight.ArrivalTime
            );

            if (hasConflict)
            {
                Console.WriteLine("Gate assignment rejected! This gate is occupied by another flight during this time.");
                return;
            }

            flight.Gate = gate;
            Console.WriteLine("Gate assigned successfully.");
        }

        public void UpdateFlightStatus(string flightNumber, FlightStatus flightStatus)
        {
            Flight? flight = Flights.FirstOrDefault(f => f.FlightNumber == flightNumber);

            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            flight.Status = flightStatus;
            Console.WriteLine("Flight status updated successfully.");
        }

        // Passenger & Boarding
        public void RegisterPassenger(int passengerId, string passengerName, PassengerCategory category, string? connectingFlightNumber)
        {
            if (passengerId <= 0)
            {
                Console.WriteLine("Enter a valid ID!");
                return;
            }

            if (Passengers.Any(p => p.PassengerID == passengerId))
            {
                Console.WriteLine("A passenger with this ID already exists.");
                return;
            }

            if (string.IsNullOrWhiteSpace(passengerName))
            {
                Console.WriteLine("Enter a valid Name!");
                return;
            }

            Flight? connectingFlight = null;

            if (!string.IsNullOrWhiteSpace(connectingFlightNumber))
            {
                connectingFlight = Flights.FirstOrDefault(f => f.FlightNumber == connectingFlightNumber);

                if (connectingFlight == null)
                {
                    Console.WriteLine("Connecting flight not found.");
                    return;
                }
            }

            Passenger passenger = new Passenger();

            passenger.PassengerID = passengerId;
            passenger.Name = passengerName;
            passenger.Category  = category;
            passenger.ConnectingFlight = connectingFlight;

            Passengers.Add(passenger);
            Console.WriteLine("Passenger registered successfully.");
        }

        public bool CheckConnectionEligibility(int passengerId, string flightNumber)
        {
            Passenger? passenger = Passengers.FirstOrDefault(p => p.PassengerID == passengerId);

            if (passenger == null)
            {
                Console.WriteLine("Passenger not found.");
                return false;
            }

            Flight? flight = Flights.FirstOrDefault(f => f.FlightNumber == flightNumber);

            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return false;
            }

            if (passenger.ConnectingFlight == null)
            {
                Console.WriteLine("Passenger has no connecting flight. Connection check passed.");
                return true;
            }

            if (passenger.ConnectingFlight.ArrivalTime >= flight.DepartureTime)
            {
                Console.WriteLine("Invalid connection. Connecting flight must arrive before the departure of this flight.");
                return false;
            }

            TimeSpan connectionTime = flight.DepartureTime - passenger.ConnectingFlight.ArrivalTime;


            if (connectionTime.TotalMinutes < MinimumConnectionMinutes)
            {
                Console.WriteLine("Passenger does not have enough connection time.");
                return false;
            }

            Console.WriteLine("Passenger has enough connection time.");
            return true;
        }

        public void BoardPassenger(int passengerId, string flightNumber)
        {
            Passenger? passenger = Passengers.FirstOrDefault(p => p.PassengerID == passengerId);

            if (passenger == null)
            {
                Console.WriteLine("Passenger not found.");
                return;
            }

            Flight? flight = Flights.FirstOrDefault(f => f.FlightNumber == flightNumber);

            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            Booking? booking = flight.Bookings.FirstOrDefault(b => b.Passenger.PassengerID == passengerId && b.Status != BookingStatus.Cancelled);
            if (booking == null)
            {
                Console.WriteLine("Passenger does not have a booking on this flight.");
                return;
            }

            if (booking.Status != BookingStatus.Confirmed)
            {
                Console.WriteLine("Passenger cannot board because the booking is not confirmed.");
                return;
            }

            if (flight.Status != FlightStatus.Boarding)
            {
                Console.WriteLine("Passenger cannot board because the flight is not currently boarding.");
                return;
            }

            if (booking.IsBoarded)
            {
                Console.WriteLine("Passenger has already boarded.");
                return;
            }

            if (!CheckConnectionEligibility(passengerId, flightNumber))
            {
                Console.WriteLine("Passenger cannot board because the connection time is insufficient.");
                return;
            }

            booking.IsBoarded = true;
            Console.WriteLine("Passenger boarded successfully.");
        }

        //Baggage
        public void RegisterBaggage(int bagId, double bagWeight, int passengerId, string flightNumber)
        {
            if (bagId <= 0)
            {
                Console.WriteLine("Enter a valid Bag Id!");
                return;
            }

            if (Bags.Any(p => p.BagId == bagId))
            {
                Console.WriteLine("Bag Id must not already exist");
                return;
            }

            if (bagWeight <= 0)
            {
                Console.WriteLine("Bag Weight must be greater than 0.");
                return;
            }

            Passenger? passenger = Passengers.FirstOrDefault(p => p.PassengerID == passengerId);
            if (passenger == null)
            {
                Console.WriteLine("Passenger not found.");
                return;
            }

            Flight? flight = Flights.FirstOrDefault( f => f.FlightNumber == flightNumber);
            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            if (flight.Status == FlightStatus.Departed || flight.Status == FlightStatus.Cancelled)
            {
                Console.WriteLine("Baggage cannot be registered for a departed or cancelled flight.");
                return;
            }

            Booking? booking = flight.Bookings.FirstOrDefault(b => b.Passenger.PassengerID == passengerId && b.Status != BookingStatus.Cancelled); 
            if (booking == null)
            {
                Console.WriteLine("Booking not found.");
                return;
            }

            if (booking.Status != BookingStatus.Confirmed)
            {
                Console.WriteLine("Baggage can only be registered for a confirmed booking.");
                return;
            }

            double currentWeight = GetCumulativeBaggageWeight(passengerId, flightNumber);
            double newTotalWeight = currentWeight + bagWeight;


            if (newTotalWeight > BaggageAllowance)
            {
                Console.WriteLine("Baggage allowance exceeded.");
                return;
            }

            Baggage baggage = new Baggage();

            baggage.BagId = bagId;
            baggage.BagWeight = bagWeight;
            baggage.Passenger = passenger;
            baggage.Flight = flight;

           

            Bags.Add(baggage);
            Console.WriteLine("Baggage registered successfully.");
        }

        public double GetCumulativeBaggageWeight(int passengerId, string flightNumber)
        {

            Flight? flight = Flights.FirstOrDefault(f => f.FlightNumber == flightNumber);
            Passenger? passenger = Passengers.FirstOrDefault(p => p.PassengerID == passengerId);

            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return 0;
            }

            if (passenger == null)
            {
                Console.WriteLine("Passenger not found.");
                return 0;
            }

            var passengerBags = Bags.Where(b =>
                b.Passenger == passenger
                &&
                b.Flight == flight
            );

            double totalWeight = passengerBags.Sum(b => b.BagWeight);

            return totalWeight;


        }


        // Booking & Standby
        public void BookPassenger(int passengerId, string flightNumber)
        {
            Passenger? passenger = Passengers.FirstOrDefault(p => p.PassengerID == passengerId);

            if (passenger == null)
            {
                Console.WriteLine("Passenger not found.");
                return;
            }

            Flight? flight = Flights.FirstOrDefault(f => f.FlightNumber == flightNumber);

            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            Booking? booking = flight.Bookings.FirstOrDefault(
                b => b.Passenger.PassengerID == passengerId &&
                     b.Status != BookingStatus.Cancelled
            );

            if (booking != null)
            {
                Console.WriteLine("Passenger already has a booking on this flight.");
                return;
            }

            int confirmedCount = flight.Bookings.Count(
                b => b.Status == BookingStatus.Confirmed
            );

            Booking newBooking = new Booking();

            newBooking.Passenger = passenger;
            newBooking.Flight = flight;

            if (confirmedCount < flight.SeatCapacity)
            {
                newBooking.Status = BookingStatus.Confirmed;
            }
            else
            {
                int standbyCount = flight.Bookings.Count(
                    b => b.Status == BookingStatus.Standby
                );

                if (standbyCount >= StandbyCapacity)
                {
                    Console.WriteLine("Booking rejected. Standby list is full.");
                    return;
                }

                newBooking.Status = BookingStatus.Standby;
            }

            newBooking.BookingId = nextBookingId;
            nextBookingId++;

            flight.Bookings.Add(newBooking);

            if (newBooking.Status == BookingStatus.Confirmed)
            {
                Console.WriteLine("Booking confirmed successfully.");
            }
            else
            {
                Console.WriteLine("Flight is full. Passenger added to standby list.");
            }
        }

        public void CancelBooking(int passengerId, string flightNumber)
        {
            Passenger? passenger = Passengers.FirstOrDefault(
                p => p.PassengerID == passengerId
            );

            if (passenger == null)
            {
                Console.WriteLine("Passenger not found.");
                return;
            }

            Flight? flight = Flights.FirstOrDefault(
                f => f.FlightNumber == flightNumber
            );

            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            Booking? booking = flight.Bookings.FirstOrDefault(
                b => b.Passenger.PassengerID == passengerId &&
                     b.Status != BookingStatus.Cancelled
            );

            if (booking == null)
            {
                Console.WriteLine("Active booking not found.");
                return;
            }

            BookingStatus oldStatus = booking.Status;
            booking.Status = BookingStatus.Cancelled;

            if (oldStatus == BookingStatus.Confirmed)
            {
                Booking? standbyBooking = flight.Bookings.FirstOrDefault(
                    b => b.Status == BookingStatus.Standby
                );

                if (standbyBooking != null)
                {
                    standbyBooking.Status = BookingStatus.Confirmed;
                    Console.WriteLine(
                        "First standby passenger has been promoted to confirmed."
                    );
                }
            }

            Console.WriteLine("Booking cancelled successfully.");
        }

        public void ViewStandbyList(string flightNumber)
        {
            Flight? flight = Flights.FirstOrDefault(f => f.FlightNumber == flightNumber);

            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            var standbyBookings = flight.Bookings.Where(b => b.Status == BookingStatus.Standby);

            if (!standbyBookings.Any())
            {
                Console.WriteLine("No passengers on the standby list.");
                return;
            }

            Console.WriteLine($"Standby List for Flight {flight.FlightNumber}:");

            int position = 1;
            foreach (var item in standbyBookings)
            {
                Console.WriteLine($"{position}. Passenger ID: {item.Passenger.PassengerID} | Name: {item.Passenger.Name}");
                position++;
            }
            
        }

        // Staff Management
        public void RegisterGroundStaff(int staffId, string name)
        {
            if (staffId <= 0)
            {
                Console.WriteLine("Enter a valid Staff Id");
                return;
            }

            if (GroundStaff.Any(s => s.StaffID == staffId))
            {
                Console.WriteLine("Staff ID already exists");
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Enter a valid Name!");
                return;
            }

            GroundStaff groundStaff = new GroundStaff();

            groundStaff.StaffID = staffId;
            groundStaff.Name = name;

            GroundStaff.Add(groundStaff);
            Console.WriteLine("Ground Staff registered successfully.");
        }

        public void AssignStaff(int staffId, DateTime startTime, DateTime endTime, string? flightNumber, string? gateIdentifier)
        {
            GroundStaff? groundStaff = GroundStaff.FirstOrDefault(s => s.StaffID == staffId);
            if (groundStaff == null)
            {
                Console.WriteLine("Ground Staff not found.");
                return;
            }

            if(endTime <= startTime)
            {
                Console.WriteLine("Invalid time. End time must be later than start time."); 
                return;
            }

            if (startTime.Date != endTime.Date)
            {
                Console.WriteLine("Staff assignment must start and end on the same day.");
                return;
            }

            Flight? flight = null;
            Gate? gate = null;

            if (!string.IsNullOrWhiteSpace(flightNumber))
            {
                flight = Flights.FirstOrDefault(f => f.FlightNumber == flightNumber);
                if (flight == null)
                {
                    Console.WriteLine("Flight not found.");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(gateIdentifier))
            {
                gate = Gates.FirstOrDefault(g => g.GateIdentifier == gateIdentifier);
                if (gate == null)
                {
                    Console.WriteLine("Gate not found.");
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(flightNumber) && string.IsNullOrWhiteSpace(gateIdentifier))
            {
                Console.WriteLine("Staff must be assigned to a flight or a gate.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(flightNumber) && !string.IsNullOrWhiteSpace(gateIdentifier))
            {
                Console.WriteLine("Staff cannot be assigned to both a flight and a gate in the same assignment.");
                return;
            }           

            TimeSpan assignmentDuration = endTime - startTime;
            double newAssignmentHours = assignmentDuration.TotalHours;

            double currentDutyHours = 0;

            foreach (var assignment in groundStaff.Assignments)
            {
                if (assignment.StartTime.Date == startTime.Date)
                {
                    TimeSpan duration = assignment.EndTime - assignment.StartTime;
                    currentDutyHours += duration.TotalHours;
                }
            }

            double totalDutyHours = currentDutyHours + newAssignmentHours;

            if(totalDutyHours > MaximumDutyHours)
            {
                Console.WriteLine("Staff assignment rejected. Maximum duty hours exceeded.");
                return;
            }

            StaffAssignment stAssignment = new StaffAssignment();

            stAssignment.AssignmentId = nextAssignmentId;
            nextAssignmentId++;
            stAssignment.StartTime = startTime;
            stAssignment.EndTime = endTime;
            stAssignment.Flight = flight;
            stAssignment.Gate = gate;

            groundStaff.Assignments.Add(stAssignment);
            Console.WriteLine("Staff assigned successfully.");
        }

        public void ViewCumulativeDutyHours(int staffId, DateTime date)
        {

            GroundStaff? groundStaff = GroundStaff.FirstOrDefault(s => s.StaffID == staffId);
            if (groundStaff == null)
            {
                Console.WriteLine("Ground Staff not found.");
                return;
            }

            double currentDutyHours = 0;

            foreach (var assignment in groundStaff.Assignments)
            {
                if (assignment.StartTime.Date == date.Date)
                {
                    TimeSpan duration = assignment.EndTime - assignment.StartTime;
                    currentDutyHours += duration.TotalHours;
                }
            }
            Console.WriteLine($"Ground Staff: {groundStaff.Name} | Date: {date:dd/MM/yyyy} | Total Duty Hours: {currentDutyHours}");
        }
    }

}
