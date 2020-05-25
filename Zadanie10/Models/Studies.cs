using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadanie10.Models
{
    public class Studies
    {
        public int IdStudy { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Enrollment> EnrollmentList { get; set; }
        public Studies()
        {
            EnrollmentList = new HashSet<Enrollment>();
        }
    }
}
