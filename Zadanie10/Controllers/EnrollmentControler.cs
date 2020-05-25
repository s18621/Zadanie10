using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadanie10.DTOs;
using Zadanie10.Services;

namespace Zadanie10.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IDbServices service;

        public EnrollmentController(IDbServices service)
        {
            this.service = service;
        }
        [HttpPost]
        [Route("promote")]
        public IActionResult Promote(PromoteRequest req)
        {
            return service.promote(req);
        }
        [HttpPost]
        [Route("enroll")]
        public IActionResult EnrollStudent(EnrollmentRequest req)
        {
            return service.enrollStudent(req);
        }
    }
}
