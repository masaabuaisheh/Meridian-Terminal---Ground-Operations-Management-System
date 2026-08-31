# Meridian Terminal - Ground Operations Management System

## Project Description

MeridianTerminalOps is a C# console application for managing basic airport ground operations for one terminal.

The system handles flights, gates, passengers, boarding, baggage, bookings and standby passengers, and ground staff assignments.

---

## How to Run the Program

1. Open the `MeridianTerminalOps` project in Visual Studio.
2. Make sure the project builds successfully.
3. Run the program using the Start button or `Ctrl + F5`.
4. The main menu will appear in the console.
5. Enter the number of the operation you want to perform and follow the instructions shown on the screen.

### Date and Time Input

When the program asks for an arrival time, departure time, or staff assignment time, enter both the date and time.

Example:

Arrival Time:
8/31/2026 18:00

Departure Time:
8/31/2026 20:00

The departure time must be after the arrival time.

---

## Implemented Features

### Flight and Gate Management
- Register domestic and international flights.
- Store flight arrival and departure times.
- Set the seat capacity of each flight.
- Register gates.
- Specify whether a gate supports international flights.
- Assign gates to flights.
- Prevent overlapping flights from using the same gate.
- Prevent international flights from using gates that do not support international flights.
- Update flight status to Scheduled, Delayed, Boarding, Departed, or Cancelled.

### Passenger and Boarding Management
- Register passengers as Standard, VIP, or Reduced Mobility.
- Optionally link a passenger to a connecting flight.
- Check whether the passenger has enough connection time.
- Minimum connection time is 45 minutes.
- Board passengers only when they have a confirmed booking and the flight status is Boarding.
- Prevent the same passenger from boarding twice.

### Baggage Management
- Register multiple bags for a passenger on a flight.
- Calculate cumulative baggage weight.
- Maximum baggage allowance is 30 kg.
- Prevent baggage registration when the total weight exceeds the allowance.
- Baggage can only be registered for a confirmed booking.
- Baggage cannot be registered for departed or cancelled flights.

### Booking and Standby Management
- Book registered passengers on flights.
- Confirm bookings while seats are available.
- Add passengers to the standby list when the flight reaches its seat capacity.
- Maximum standby capacity is 10 passengers.
- Cancel bookings.
- Automatically promote the earliest standby passenger when a confirmed booking is cancelled.
- Allow a passenger to book the same flight again after cancelling a previous booking.
- View the current standby list.

### Ground Staff Management
- Register ground staff.
- Assign staff to a flight or gate.
- Calculate cumulative daily duty hours.
- Maximum duty time is 8 hours per day.
- Prevent assignments that exceed the maximum daily duty hours.

---

## System Rules

The following values are used in the system:

- Minimum connection time: 45 minutes
- Maximum baggage allowance: 30 kg
- Maximum staff duty time: 8 hours per day
- Maximum standby capacity: 10 passengers
- Gate occupancy window: from the flight arrival time until its departure time
- Back-to-back flights can use the same gate when their time windows do not overlap

All data is stored in memory while the program is running. Data is not saved after the program is closed.

---

## Example Test Flow

The following example can be used to test the main system flow.

### 1. Register a Flight

Flight Number: F100  
Type: Domestic  
Arrival Time: 8/31/2026 18:00  
Departure Time: 8/31/2026 20:00  
Seat Capacity: 2

### 2. Register a Gate

Gate Identifier: G1  
Supports International Flights: Yes

Then assign `G1` to `F100`.

### 3. Register Passengers

Passenger 1:
- ID: 1
- Name: Ahmad
- Category: Standard
- Connecting Flight: No

Passenger 2:
- ID: 2
- Name: Sara
- Category: VIP
- Connecting Flight: No

Passenger 3:
- ID: 3
- Name: Ali
- Category: Standard
- Connecting Flight: No

### 4. Book the Passengers

Book Passenger 1 on F100.
Expected: Confirmed

Book Passenger 2 on F100.
Expected: Confirmed

Book Passenger 3 on F100.
Expected: Standby because the flight capacity is 2.

### 5. Test Standby Promotion

Cancel Passenger 1's booking.

Expected:
- Passenger 1 becomes Cancelled.
- Passenger 3 is automatically promoted from Standby to Confirmed.

### 6. Register Baggage

Register baggage for Passenger 3:

Bag ID: 101  
Weight: 15 kg  
Flight: F100

Register another bag:

Bag ID: 102  
Weight: 10 kg  
Flight: F100

Expected cumulative baggage weight: 25 kg.

Trying to add another 10 kg bag should be rejected because the total would become 35 kg, which exceeds the 30 kg allowance.

### 7. Test Boarding

First try to board Passenger 3 while the flight is still Scheduled.

Expected:
Passenger cannot board because the flight is not currently boarding.

Update F100 status to Boarding.

Try to board Passenger 3 again.

Expected:
Passenger boarded successfully.

Trying to board the same passenger again should be rejected because the passenger has already boarded.

---

## Technologies Used

- C#
- .NET
- Visual Studio
- Object-Oriented Programming (OOP)
- LINQ
