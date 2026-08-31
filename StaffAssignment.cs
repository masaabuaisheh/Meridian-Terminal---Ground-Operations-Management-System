namespace MeridianTerminalOps
{
    internal class StaffAssignment
    {
        public int AssignmentId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Flight? Flight { get; set; }
        public Gate? Gate { get; set; }
    }
}