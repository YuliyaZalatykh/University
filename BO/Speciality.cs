using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.BO
{
    class Speciality
    {
        public int code { get; set; }
        public string name { get; set; }
        public string qualification { get; set; }
        public string educationForm { get; set; }
        public int facultyCode { get; set; }

        public Speciality(int code, string name, string qualification, string educationForm, int facultyCode)
        {
            this.code = code;
            this.name = name;
            this.qualification = qualification;
            this.educationForm = educationForm;
            this.facultyCode = facultyCode;
        }
    }
}
