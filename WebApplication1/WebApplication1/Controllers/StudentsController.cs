using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.StudentsController
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IdbService idbService;

        public StudentsController(IdbService dbService)
        {
            idbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            return Ok(idbService.GetStudents());
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int deleteId)
        {
            return Ok("Usuwanie ukończone");
        }

        [HttpPut("{id}")]
        public IActionResult PutStudent(int PutId)
        {
            return Ok("Aktualizacja dokończona");
        }

    }
}
