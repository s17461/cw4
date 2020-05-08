using cw3.DTOs.Requests;
using cw3.DTOs.Responses;
using cw3.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.Services
{
    public interface IStudentsDal
    {
        public IEnumerable<Student> GetStudents();
        public IEnumerable<Student> GetStudents(string indexNumber);
        public IEnumerable<EnrollStudentResponse> EnrollStudent(EnrollStudentRequest request);
    }
}
