using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSAAttenderAPI.Data.DAL.Model
{
    public class AttendanceAPIModel
    {
        public string userName { get; set; }
        public int numberOfAttendance { get; set; }
        public List<long> daysAttended { get; set; }
        public List<bool> daysDidAttend { get; set; }
        public List<string> trainingAttended { get; set; }
    }
}
