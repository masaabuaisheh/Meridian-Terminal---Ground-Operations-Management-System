using System;
using System.Collections.Generic;
using System.Text;

namespace MeridianTerminalOps
{
    internal class GroundStaff
    {
        public int StaffID { get; set; }
        public string Name { get; set; }
        public List<StaffAssignment> Assignments { get; set; } = new List<StaffAssignment>();
    }
}
