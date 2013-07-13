using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckNameHelper
{
    class StuInfo
    {
        public StuInfo()
        {
            Grades = new int[20];
        }
        public int No { get; set; }
        public string Name { get; set; }
        public int StuNo { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        public int ExpGrade { get; set; }
        public int TestGrade { get; set; }
        public int TotalGrade { get; set; }
        public int[] Grades { get; set; }
    }
}
