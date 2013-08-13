using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace infotouch
{
    class Schedule
    {
        public int ScheduleId { get;set; }
        public string ScheduleCode { get; set; }
        public string AmIn { get; set; }
        public string AmOut { get; set; }
        public string PmIn { get; set; }
        public string PmOut { get; set; }
    }

    class Attendance
    {
        public int AttendanceId { get; set; }
        public DateTime TrDate { get; set; }
        public int EmpNum { get; set; }
        public DateTime TrAmIn { get; set; }
        public DateTime TrAmOut { get; set; }
        public DateTime TrPmIn { get; set; }
        public DateTime TrPmOut { get; set; }
    }
}
