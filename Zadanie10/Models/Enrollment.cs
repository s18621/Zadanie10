﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadanie10.Models
{
    public partial class Enrollment
    {
        public int IdEnrollment { get; set; }
        public int Semester { get; set; }
        public int IdStudy { get; set; }
        public DateTime StartDate { get; set; }
        public virtual Studies Nav { get; set; }
        public virtual ICollection<Student> StudentList { get; set; }
        public Enrollment()
        {
            this.StudentList = new HashSet<Student>();
        }
    }
}