using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadanie10.Models;
using Zadanie10.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Zadanie10.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IDbServices service;
        public StudentsController(IDbServices service)
        {
            this.service = service;
        }
        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = service.GetStudents();
            return Ok(students);
        }
        [HttpPost]
        [Route("modify")]
        public IActionResult Modify(Student student)
        {
            var result = service.modifyStudent(student);
            return Ok(result);

        }
        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(Student student)
        {
            var result = service.removeStudent(student);
            if (result == null) return BadRequest("Nie ma takiego studenta");
            return Ok("Student usuniety");
        }
    }
}
