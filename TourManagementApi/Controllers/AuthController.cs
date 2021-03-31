using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TourManagementApi.Models;

namespace TourManagementApi.Controllers
{
    public class AuthController : ApiController
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        String ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename = C:\USERS\RAJKA\ONEDRIVE\DESKTOP\CLIENT\APP_DATA\DATABASE1.MDF;Integrated Security = True";

        [HttpGet]
        public IHttpActionResult Get(String username)
        {
            User u = new User();
            try
            {
                con = new SqlConnection();
                con.ConnectionString = this.ConnectionString;
                using (con)
                {
                    string command = "select * from users where email = '" + username + "'";
                    cmd = new SqlCommand(command, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        u.email = rdr["email"].ToString();
                        u.fname = rdr["fname"].ToString();
                        u.lname = rdr["lname"].ToString();
                        u.password = rdr["password"].ToString();
                    }
                    rdr.Close();
                    return Ok(u);
                }
            }
            catch (Exception err)
            {
                return BadRequest();
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
        }

        // POST: api/Auth
        public HttpResponseMessage Post(User user)
        {                        
            try
            {
                con = new SqlConnection();
                con.ConnectionString = this.ConnectionString;

                using (con)
                {
                    string command = "INSERT INTO users(email,fname,lname,password)VALUES(@email,@fname,@lname,@password)";
                    cmd = new SqlCommand(command, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@email", user.email);
                    cmd.Parameters.AddWithValue("@fname", user.fname);
                    cmd.Parameters.AddWithValue("@lname", user.lname);
                    cmd.Parameters.AddWithValue("@password", user.password);
                    int res = cmd.ExecuteNonQuery();
                    if (res == 1)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Registered Successfully");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "");
                    }
                }
            }
            catch (Exception err)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "");
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
        }

        // PUT: api/Auth/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Auth/5
        public void Delete(int id)
        {
        }
    }
}
