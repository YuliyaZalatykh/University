using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.BO
{
    class Discipline
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Semest { get; set; }
        public int Hours { get; set; }
        public int LabH { get; set; }
        public int PracticeH { get; set; }
        public int CourseH { get; set; }
        public string ReportType { get; set; }
        public int SpecCode { get; set; }

        public Discipline(int Code, string Name, int Semest, int Hours, int LabH, int PracticeH, int CourseH, string ReportType, int SpecCode)
        {
            this.Code = Code;
            this.Name = Name;
            this.Semest = Semest;
            this.Hours = Hours;
            this.LabH = LabH;
            this.PracticeH = PracticeH;
            this.CourseH = CourseH;
            this.ReportType = ReportType;
            this.SpecCode = SpecCode;
        }
    }
}
