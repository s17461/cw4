using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using cw3.DTOs.Requests;
using cw3.DTOs.Responses;
using cw3.Models;
using cw3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {

        private IStudentsDal _dbService;
        public EnrollmentsController(IStudentsDal dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]

        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            //return Ok(_dbService.GetStudents());
            return Ok(_dbService.EnrollStudent(request));
        }
        /*public IActionResult EnrollStudent(EnrollStudentRequest request)
        {

            
            //DTOs-Data Transfer Objects
            var st = new Student();
            st.FirstName = request.FirstName;
            //.. cos robie

            using(var con=new SqlConnection(""))
                using(var com=new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();

                try
                {
                    //czy istnieja?
                    com.CommandText = "select IdStudies from studies where name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        return BadRequest("Studia nie istnieja");
                        //...
                    }
                    int idstudies = (int)dr["IdStudies"];

                    //x.Dodanie studenta
                    com.CommandText = "INSERT INTO Student(IndexNumber, FirstName) VALUES(@Index,@Fname)";
                    com.Parameters.AddWithValue("index", request.IndexNumber);

                    //..

                    tran.Commit();

                }catch(SqlException exc)
                {
                    tran.Rollback();
                }

            }


            var response = new EnrollStudentResponse();
            response.LastName = st.LastName;
            //...


            return Ok(response);
        }*/
    }
}