﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadanie10.DTOs;
using Zadanie10.Models;

namespace Zadanie10.Services
{
    public class SQLServices : IDbServices
    {
        private s18621Context context;

        public SQLServices([FromServices] DbContext context)
        {
            this.context = (s18621Context)context;
        }
        public List<Student> GetStudents()
        {
            return context.Student.ToList();
        }

        public Student modifyStudent(Student student)
        {
            try
            {
                context.Attach(student);
                context.Entry(student).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return student;
        }

        public Student removeStudent(Student student)
        {
            var st = context.Student.FirstOrDefault(s => s.IndexNumber == student.IndexNumber);
            if (st == null) return null;
            context.Attach(st);
            context.Remove(st);
            context.SaveChanges();
            return st;
        }
        public IActionResult promote(PromoteRequest req)
        {
            var res = context.Enrollment.Join(context.Studies, enrollment => enrollment.IdStudy,
                studies => studies.IdStudy, ((enrollment, studies) => new
                {
                    studies.Name,
                    enrollment.Semester
                })).Where(res => res.Name == req.studies && res.Semester == req.semester);
            if (!res.Any()) return new BadRequestResult();
            context.Database.ExecuteSqlInterpolated($"exec promoteStudent {req.studies}, {req.semester}");
            context.SaveChanges();
            return new OkObjectResult(req);
        }
        public IActionResult enrollStudent(EnrollmentRequest req)
        {
            Student student = new Student
            {
                IndexNumber = req.IndexNumber,
                LastName = req.LastName,
                FirstName = req.FirstName,
                BirthDate = req.BirthDate
            };
            if (!context.Studies.Any(s => s.Name.Equals(req.StudyName))) return null;
            if (context.Student.Any(s => s.IndexNumber.Equals(student.IndexNumber))) return null;
            Student addStudent = new Student
            {
                IndexNumber = student.IndexNumber,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                FirstName = student.FirstName,
                IdEnrollment = 1
            };
            context.Student.Add(addStudent);
            context.SaveChanges();
            int idStudy = context.Studies.Single(s => s.Name.Equals(req.StudyName)).IdStudy;
            int idEnrollment = context.Enrollment.Where(e => e.Semester == 1 && e.IdStudy == idStudy)
                .OrderByDescending(e => e.StartDate).First().IdEnrollment;
            if (idEnrollment == 0)
            {
                idEnrollment = context.Enrollment.Max(e => e.IdEnrollment) + 1;
                Enrollment newEnrollment = new Enrollment
                {
                    IdEnrollment = idEnrollment,
                    Semester = 1,
                    IdStudy = idStudy,
                    StartDate = DateTime.Now
                };
                context.Enrollment.Add(newEnrollment);
                context.SaveChanges();
            }
            addStudent.IdEnrollment = idEnrollment;
            context.SaveChanges();
            var resp = context.Enrollment.Single(e => e.IdEnrollment == idEnrollment);
            return new OkObjectResult(new EnrollmentResult
            {
                Semester = resp.Semester,
                IdStudy = resp.IdStudy,
                StartDate = resp.StartDate,
                IdEnrollment = resp.IdEnrollment
            });
        }
    }
}
