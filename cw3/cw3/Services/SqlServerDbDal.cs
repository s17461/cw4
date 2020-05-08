using cw3.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
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
    }
}
