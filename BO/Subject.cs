using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.BO
{
    class Subject
    {
        public int code { get; set; }
        public string name { get; set; }
        public int departmentCode { get; set; }
        public int semester { get; set; }
        public int hours { get; set; }
        public int totalHours { get; set; }
        public int labHours { get; set; }
        public int practiseHours { get; set; }
        public int courseHours { get; set; }
        public string report { get; set; }

        public Subject(int code, string name, int departmentCode)
        {
            this.code = code;
            this.name = name;
            this.departmentCode = departmentCode;
        }
    }
}
