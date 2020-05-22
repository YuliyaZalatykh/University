using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.BO
{
    class Department
    {
        public int code { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public int facultyCode { get; set; }

        public Department(int code, string name, string phone, int facultyCode)
        {
            this.code = code;
            this.name = name;
            this.phone = phone;
            this.facultyCode = facultyCode;
        }
    }
}
