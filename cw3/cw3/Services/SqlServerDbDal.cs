using cw3.DTOs.Requests;
using cw3.DTOs.Responses;
using cw3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.Services
{
    public class SqlServerDbDal : IStudentsDal
    {

        private const string ConString = "Data Source=db-mssql;Initial Catalog=s17461;Integrated Security=True";
        public IEnumerable<Student> GetStudents()
        {
            var list = new List<Student>();

            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                //com.CommandText = "select * from student";
                com.CommandText = "select s.firstname, s.lastname, s.BirthDate, st.Name, e.Semester from student s join Enrollment e on s.IdEnrollment = e.IdEnrollment join studies st on e.IdStudy = st.IdStudy;";


                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();                  
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.NameSt = dr["Name"].ToString();
                    st.Semester = Int32.Parse(dr["Semester"].ToString());

                    list.Add(st);
                    //st.FirstName = dr["FirstName"].ToString();
                }

            }
            return list;
        }

        public IEnumerable<Student> GetStudents(string indexNumber)
        {
            var list = new List<Student>();

            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                //com.CommandText = "select * from student";
                com.CommandText = "select s.firstname, s.lastname, s.BirthDate, st.Name, e.Semester from student s join Enrollment e on s.IdEnrollment = e.IdEnrollment join studies st on e.IdStudy = st.IdStudy WHERE s.IndexNumber = @id";
                com.Parameters.AddWithValue("id", indexNumber);

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.NameSt = dr["Name"].ToString();
                    st.Semester = Int32.Parse(dr["Semester"].ToString());

                    list.Add(st);
                    //st.FirstName = dr["FirstName"].ToString();
                }

            }
            return list;
        }

        public IEnumerable<EnrollStudentResponse> EnrollStudent(EnrollStudentRequest request)
        {


            //DTOs-Data Transfer Objects
            var st = new EnrollStudentRequest();
            st.FirstName = request.FirstName;
            /*st.LastName = request.LastName;
            st.BirthDate = request.BirthDate;
            st.IndexNumber = request.LastName;
            st.Studies = request.Studies;*/
            //.. cos robie

            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
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
                        //return BadRequestObjectResult(Error.Add"Studia nie istnieja");
                        return null;
                             
                   
                        //...
                    }
                    int idstudies = (int)dr["IdStudies"];

                    //x.Dodanie studenta
                    com.CommandText = "INSERT INTO Student(IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) VALUES(@Index,@Fname,@Lname,@BDay,@IdEn)";
                    com.Parameters.AddWithValue("index", request.IndexNumber);
                    com.Parameters.AddWithValue("Fname", request.FirstName);
                    com.Parameters.AddWithValue("Lname", request.LastName);
                    com.Parameters.AddWithValue("BDay", request.BirthDate);
                    com.Parameters.AddWithValue("IdEn", 0);

                    //..

                    tran.Commit();

                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                }

            }


            var response = new EnrollStudentResponse();
            response.LastName = st.LastName;
            
            //...


            return new List<EnrollStudentResponse>(); // do zmiany
        }

        
    }
}
