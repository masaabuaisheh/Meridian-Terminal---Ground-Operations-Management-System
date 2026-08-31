namespace MeridianTerminalOps
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TerminalOperations terminal = new TerminalOperations();

            while (true)
            {

                    Console.WriteLine("\n=== Meridian Terminal — Ground Operations System ===");
                    Console.WriteLine("1. Register Flight");
                    Console.WriteLine("2. Assign Gate");
                    Console.WriteLine("3. Register Passenger");
                    Console.WriteLine("4. Check Boarding Eligibility");
                    Console.WriteLine("5. Register Baggage");
                    Console.WriteLine("6. Manage Bookings & Standby");
                    Console.WriteLine("7. Assign Staff");
                    Console.WriteLine("8. Exit");
                    Console.Write("Choose an option: ");

                    string? choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            {
                                Console.WriteLine();
                                Console.WriteLine("1. Register New Flight");
                                Console.WriteLine("2. Update Flight Status");
                                Console.Write("Choose an option: ");
                                string? flightChoice = Console.ReadLine();

                                if (flightChoice == "1")
                                {
                                    Console.WriteLine();
                                    Console.Write("Enter Flight Number: ");
                                    string? flightNumber = Console.ReadLine();

                                    Console.WriteLine("Select Flight Type:");
                                    Console.WriteLine("1. Domestic");
                                    Console.WriteLine("2. International");
                                    Console.Write("Choose type: ");

                                    string? typeChoice = Console.ReadLine();

                                    FlightType flightType;

                                    if (typeChoice == "1")
                                    {
                                        flightType = FlightType.Domestic;
                                    }
                                    else if (typeChoice == "2")
                                    {
                                        flightType = FlightType.International;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid flight type.");
                                        break;
                                    }

                                    Console.Write("Enter Arrival Time: ");
                                    DateTime arrivalTime;

                                    if (!DateTime.TryParse(Console.ReadLine(), out arrivalTime))
                                    {
                                        Console.WriteLine("Invalid arrival time.");
                                        break;
                                    }

                                    Console.Write("Enter Departure Time: ");
                                    DateTime departureTime;

                                    if (!DateTime.TryParse(Console.ReadLine(), out departureTime))
                                    {
                                        Console.WriteLine("Invalid departure time.");
                                        break;
                                    }

                                    Console.Write("Enter Seat Capacity: ");
                                    int seatCapacity;

                                    if (!int.TryParse(Console.ReadLine(), out seatCapacity))
                                    {
                                        Console.WriteLine("Invalid seat capacity.");
                                        break;
                                    }

                                    Console.WriteLine();

                                    terminal.RegisterFlight(
                                        flightNumber,
                                        flightType,
                                        arrivalTime,
                                        departureTime,
                                        seatCapacity
                                    );
                                }
                                else if (flightChoice == "2")
                                {
                                    Console.WriteLine();
                                    Console.Write("Enter Flight Number: ");
                                    string? flightNumber = Console.ReadLine();

                                    Console.WriteLine("Select New Flight Status:");
                                    Console.WriteLine("1. Scheduled");
                                    Console.WriteLine("2. Delayed");
                                    Console.WriteLine("3. Boarding");
                                    Console.WriteLine("4. Departed");
                                    Console.WriteLine("5. Cancelled");
                                    Console.Write("Choose status: ");

                                    string? statusChoice = Console.ReadLine();

                                    FlightStatus flightStatus;

                                    if (statusChoice == "1")
                                    {
                                        flightStatus = FlightStatus.Scheduled;
                                    }
                                    else if (statusChoice == "2")
                                    {
                                        flightStatus = FlightStatus.Delayed;
                                    }
                                    else if (statusChoice == "3")
                                    {
                                        flightStatus = FlightStatus.Boarding;
                                    }
                                    else if (statusChoice == "4")
                                    {
                                        flightStatus = FlightStatus.Departed;
                                    }
                                    else if (statusChoice == "5")
                                    {
                                        flightStatus = FlightStatus.Cancelled;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid flight status.");
                                        break;
                                    }

                                    Console.WriteLine();

                                    terminal.UpdateFlightStatus(
                                        flightNumber,
                                        flightStatus
                                    );
                                }
                                else
                                {
                                    Console.WriteLine("Invalid option.");
                                }

                                break;
                            }

                    case "2":
                            {
                                Console.WriteLine();
                                Console.WriteLine("1. Register Gate");
                                Console.WriteLine("2. Assign Gate");
                                Console.Write("Choose an option: ");
                                string? gateChoice = Console.ReadLine();

                                if (gateChoice == "1")
                                {
                                    Console.Write("Enter Gate Identifier: ");
                                    string? gateIdentifier = Console.ReadLine();

                                    Console.WriteLine("Does this gate support international flights?");
                                    Console.WriteLine("1. Yes");
                                    Console.WriteLine("2. No");
                                    Console.Write("Choose an option: ");
                                    string? supportChoice = Console.ReadLine();

                                    bool supportsInternationalFlights;

                                    if (supportChoice == "1")
                                    {
                                        supportsInternationalFlights = true;
                                    }
                                    else if (supportChoice == "2")
                                    {
                                        supportsInternationalFlights = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid option.");
                                        break;
                                    }

                                    Console.WriteLine();
                                    terminal.RegisterGate(
                                        gateIdentifier,
                                        supportsInternationalFlights
                                    );
                                }
                                else if (gateChoice == "2")
                                {
                                    Console.Write("Enter Flight Number: ");
                                    string? flightNumber = Console.ReadLine();

                                    Console.Write("Enter Gate Identifier: ");
                                    string? gateIdentifier = Console.ReadLine();

                                    Console.WriteLine();
                                    terminal.AssignGate(
                                        flightNumber,
                                        gateIdentifier
                                    );
                                }
                                else
                                {
                                    Console.WriteLine("Invalid option.");
                                }
                            }
                            break;

                        case "3":
                            {
                                Console.WriteLine();

                                Console.Write("Enter Passenger ID: ");
                                int passengerId;

                                if (!int.TryParse(Console.ReadLine(), out passengerId))
                                {
                                    Console.WriteLine("Invalid Passenger ID.");
                                    break;
                                }

                                Console.Write("Enter Passenger Name: ");
                                string? passengerName = Console.ReadLine();

                                Console.WriteLine("Select Passenger Category:");
                                Console.WriteLine("1. Standard");
                                Console.WriteLine("2. VIP");
                                Console.WriteLine("3. Reduced Mobility");
                                Console.Write("Choose category: ");

                                string? categoryChoice = Console.ReadLine();

                                PassengerCategory category;

                                if (categoryChoice == "1")
                                {
                                    category = PassengerCategory.Standard;
                                }
                                else if (categoryChoice == "2")
                                {
                                    category = PassengerCategory.VIP;
                                }
                                else if (categoryChoice == "3")
                                {
                                    category = PassengerCategory.ReducedMobility;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid passenger category.");
                                    break;
                                }

                                Console.Write("Does the passenger have a connecting flight? (y/n): ");
                                string? hasConnection = Console.ReadLine();

                                string? connectingFlightNumber = null;

                                if (hasConnection == "y" || hasConnection == "Y")
                                {
                                    Console.Write("Enter Connecting Flight Number: ");
                                    connectingFlightNumber = Console.ReadLine();
                                }
                                else if (hasConnection != "n" && hasConnection != "N")
                                {
                                    Console.WriteLine("Invalid option.");
                                    break;
                                }

                                Console.WriteLine();
                                terminal.RegisterPassenger(
                                    passengerId,
                                    passengerName,
                                    category,
                                    connectingFlightNumber
                                );
                            }
                            break;

                        case "4":
                            {
                                Console.WriteLine();

                                Console.WriteLine("1. Check Connection Eligibility");
                                Console.WriteLine("2. Board Passenger");
                                Console.Write("Choose an option: ");
                                string? boardingChoice = Console.ReadLine();

                                if (boardingChoice == "1")
                                {
                                    Console.Write("Enter Passenger ID: ");
                                    int passengerId;

                                    if (!int.TryParse(Console.ReadLine(), out passengerId))
                                    {
                                        Console.WriteLine("Invalid Passenger ID.");
                                        break;
                                    }

                                    Console.Write("Enter Flight Number: ");
                                    string? flightNumber = Console.ReadLine();

                                    Console.WriteLine();
                                    terminal.CheckConnectionEligibility(
                                        passengerId,
                                        flightNumber
                                    );
                                }
                                else if (boardingChoice == "2")
                                {
                                    Console.Write("Enter Passenger ID: ");
                                    int passengerId;

                                    if (!int.TryParse(Console.ReadLine(), out passengerId))
                                    {
                                        Console.WriteLine("Invalid Passenger ID.");
                                        break;
                                    }

                                    Console.Write("Enter Flight Number: ");
                                    string? flightNumber = Console.ReadLine();

                                    Console.WriteLine();
                                    terminal.BoardPassenger(
                                        passengerId,
                                        flightNumber
                                    );
                                }
                                else
                                {
                                    Console.WriteLine("Invalid option.");
                                }
                            }
                            break;

                        case "5":
                            {
                                Console.WriteLine();

                                Console.WriteLine("1. Register Baggage");
                                Console.WriteLine("2. View Cumulative Baggage Weight");
                                Console.Write("Choose an option: ");
                                string? baggageChoice = Console.ReadLine();

                                if (baggageChoice == "1")
                                {
                                    Console.Write("Enter Bag ID: ");
                                    int bagId;

                                    if (!int.TryParse(Console.ReadLine(), out bagId))
                                    {
                                        Console.WriteLine("Invalid Bag ID.");
                                        break;
                                    }

                                    Console.Write("Enter Bag Weight: ");
                                    double bagWeight;

                                    if (!double.TryParse(Console.ReadLine(), out bagWeight))
                                    {
                                        Console.WriteLine("Invalid Bag Weight.");
                                        break;
                                    }

                                    Console.Write("Enter Passenger ID: ");
                                    int passengerId;

                                    if (!int.TryParse(Console.ReadLine(), out passengerId))
                                    {
                                        Console.WriteLine("Invalid Passenger ID.");
                                        break;
                                    }

                                    Console.Write("Enter Flight Number: ");
                                    string? flightNumber = Console.ReadLine();

                                    Console.WriteLine();
                                    terminal.RegisterBaggage(
                                        bagId,
                                        bagWeight,
                                        passengerId,
                                        flightNumber
                                    );
                                }
                                else if (baggageChoice == "2")
                                {
                                    Console.Write("Enter Passenger ID: ");
                                    int passengerId;

                                    if (!int.TryParse(Console.ReadLine(), out passengerId))
                                    {
                                        Console.WriteLine("Invalid Passenger ID.");
                                        break;
                                    }

                                    Console.Write("Enter Flight Number: ");
                                    string? flightNumber = Console.ReadLine();

                                    double totalWeight = terminal.GetCumulativeBaggageWeight(
                                        passengerId,
                                        flightNumber
                                    );

                                    Console.WriteLine($"Total Baggage Weight: {totalWeight} kg");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid option.");
                                }
                            }
                            break;

                        case "6":
                            {
                                Console.WriteLine();

                                Console.WriteLine("1. Book Passenger");
                                Console.WriteLine("2. Cancel Booking");
                                Console.WriteLine("3. View Standby List");
                                Console.Write("Choose an option: ");
                                string? bookingChoice = Console.ReadLine();

                                if (bookingChoice == "1")
                                {
                                    Console.Write("Enter Passenger ID: ");
                                    int passengerId;

                                    if (!int.TryParse(Console.ReadLine(), out passengerId))
                                    {
                                        Console.WriteLine("Invalid Passenger ID.");
                                        break;
                                    }

                                    Console.Write("Enter Flight Number: ");
                                    string? flightNumber = Console.ReadLine();

                                    Console.WriteLine();
                                    terminal.BookPassenger(
                                        passengerId,
                                        flightNumber
                                    );
                                }
                                else if (bookingChoice == "2")
                                {
                                    Console.Write("Enter Passenger ID: ");
                                    int passengerId;

                                    if (!int.TryParse(Console.ReadLine(), out passengerId))
                                    {
                                        Console.WriteLine("Invalid Passenger ID.");
                                        break;
                                    }

                                    Console.Write("Enter Flight Number: ");
                                    string? flightNumber = Console.ReadLine();

                                    Console.WriteLine();
                                    terminal.CancelBooking(
                                        passengerId,
                                        flightNumber
                                    );
                                }
                                else if (bookingChoice == "3")
                                {
                                    Console.Write("Enter Flight Number: ");
                                    string? flightNumber = Console.ReadLine();

                                    Console.WriteLine();
                                    terminal.ViewStandbyList(flightNumber);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid option.");
                                }
                            }
                            break;

                        case "7":
                            {
                                Console.WriteLine();

                                Console.WriteLine("1. Register Ground Staff");
                                Console.WriteLine("2. Assign Staff");
                                Console.WriteLine("3. View Cumulative Duty Hours");
                                Console.Write("Choose an option: ");
                                string? staffChoice = Console.ReadLine();

                                if (staffChoice == "1")
                                {
                                    Console.Write("Enter Staff ID: ");
                                    int staffId;

                                    if (!int.TryParse(Console.ReadLine(), out staffId))
                                    {
                                        Console.WriteLine("Invalid Staff ID.");
                                        break;
                                    }

                                    Console.Write("Enter Staff Name: ");
                                    string? staffName = Console.ReadLine();

                                    Console.WriteLine();
                                    terminal.RegisterGroundStaff(
                                        staffId,
                                        staffName
                                    );
                                }
                                else if (staffChoice == "2")
                                {
                                    Console.Write("Enter Staff ID: ");
                                    int staffId;

                                    if (!int.TryParse(Console.ReadLine(), out staffId))
                                    {
                                        Console.WriteLine("Invalid Staff ID.");
                                        break;
                                    }

                                    Console.Write("Enter Start Time: ");
                                    DateTime startTime;

                                    if (!DateTime.TryParse(Console.ReadLine(), out startTime))
                                    {
                                        Console.WriteLine("Invalid start time.");
                                        break;
                                    }

                                    Console.Write("Enter End Time: ");
                                    DateTime endTime;

                                    if (!DateTime.TryParse(Console.ReadLine(), out endTime))
                                    {
                                        Console.WriteLine("Invalid end time.");
                                        break;
                                    }

                                    Console.WriteLine("Assign staff to:");
                                    Console.WriteLine("1. Flight");
                                    Console.WriteLine("2. Gate");
                                    Console.Write("Choose an option: ");

                                    string? assignmentChoice = Console.ReadLine();

                                    string? flightNumber = null;
                                    string? gateIdentifier = null;

                                    if (assignmentChoice == "1")
                                    {
                                        Console.Write("Enter Flight Number: ");
                                        flightNumber = Console.ReadLine();
                                    }
                                    else if (assignmentChoice == "2")
                                    {
                                        Console.Write("Enter Gate Identifier: ");
                                        gateIdentifier = Console.ReadLine();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid option.");
                                        break;
                                    }

                                    Console.WriteLine();
                                    terminal.AssignStaff(
                                        staffId,
                                        startTime,
                                        endTime,
                                        flightNumber,
                                        gateIdentifier
                                    );
                                }
                                else if (staffChoice == "3")
                                {
                                    Console.Write("Enter Staff ID: ");
                                    int staffId;

                                    if (!int.TryParse(Console.ReadLine(), out staffId))
                                    {
                                        Console.WriteLine("Invalid Staff ID.");
                                        break;
                                    }

                                    Console.Write("Enter Date: ");
                                    DateTime date;

                                    if (!DateTime.TryParse(Console.ReadLine(), out date))
                                    {
                                        Console.WriteLine("Invalid date.");
                                        break;
                                    }

                                    Console.WriteLine();
                                    terminal.ViewCumulativeDutyHours(
                                        staffId,
                                        date
                                    );
                                }
                                else
                                {
                                    Console.WriteLine("Invalid option.");
                                }
                            }
                            break;

                        case "8":
                            Console.WriteLine("Exiting system...");
                            return;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    
                }
            }

        }
    }
}
