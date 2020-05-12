using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.BO
{
    class Speciality
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Qualification { get; set; }
        public string StudyForm { get; set; }
        public int DepartmentCode { get; set; }
        public int Duration { get; set; }

        public Speciality(int Code, string Name, string Qualification, string StudyForm, int DepartmentCode, int Duration)
        {
            this.Code = Code;
            this.Name = Name;
            this.Qualification = Qualification;
            this.StudyForm = StudyForm;
            this.DepartmentCode = DepartmentCode;
            this.Duration = Duration;
        }
    }
}
