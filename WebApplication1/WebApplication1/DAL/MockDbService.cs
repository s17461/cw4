using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using System.Data.SqlClient;

namespace WebApplication1.DAL
{
    public class MockDbService : IdbService
    {
        private const string DataSQLCon = "Data Source=db-mssql;Initial Catalog=s17461;Integrated Security=True";
        public IEnumerable<Student> GetStudents()
        {
            var list = new List<Student>();
            using (SqlConnection con = new SqlConnection(DataSQLCon))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from Student";

                con.Open();
                SqlDataReader sqlRead = com.ExecuteReader();
                while (sqlRead.Read())
                {
                    var st = new Student();
                    st.FirstName = sqlRead["FirstName"].ToString();
                    st.LastName = sqlRead["LastName"].ToString();
                    st.IndexNumber = sqlRead["IndexNumber"].ToString();
                    list.Add(st);
                }

            }
            return list;
        }

        public IEnumerable<Enrollment> GetStudentsEnrollment(string id)
        {

            var list = new List<Enrollment>();
            using (SqlConnection con = new SqlConnection(DataSQLCon))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT e.IdEnrollment,e.Semester,e.IdStudy,e.StartDate FROM Student AS s INNER JOIN Enrollment as e ON s.IdEnrollment = e.IdEnrollment WHERE s.IndexNumber = @id";
                com.Parameters.AddWithValue("id", id);

                con.Open();
                SqlDataReader sqlRead = com.ExecuteReader();

                while (sqlRead.Read())
                {
                    var en = new Enrollment();
                    en.IdEnrollment = Int32.Parse(sqlRead["IdEnrollment"].ToString());
                    en.Semester = Int32.Parse(sqlRead["Semester"].ToString());
                    en.IdStudy = Int32.Parse(sqlRead["IdStudy"].ToString());
                    en.StartDate = sqlRead["StartDate"].ToString();
                    list.Add(en);
                }

            }

            return list;
        }

    }
}