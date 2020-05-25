using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadanie10.Models;
using Zadanie10.DTOs;

namespace Zadanie10.Services
{
    public interface IDbServices
    {
        public List<Student> GetStudents();
        public Student modifyStudent(Student student);
        public Student removeStudent(Student student);
        public IActionResult enrollStudent(EnrollmentRequest req);
        public IActionResult promote(PromoteRequest req);
    }
}
