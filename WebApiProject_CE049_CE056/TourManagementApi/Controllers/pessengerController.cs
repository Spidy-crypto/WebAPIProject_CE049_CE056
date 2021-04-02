using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using TourManagementApi.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace TourManagementApi.Controllers
{
    public class pessengerController : ApiController
    {
        [HttpGet]
        public IHttpActionResult getpassengers(string email)
        {
            List<User> users = new List<User>();
            User u;
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

                using (con)
                {
                    string command = "select * from passenger where email = '" + email + "'";
                    cmd = new SqlCommand(command, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        u = new User();
                        u.email = rdr["email"].ToString();
                        u.fname = rdr["fname"].ToString();
                        u.lname = rdr["lname"].ToString();
                        users.Add(u);
                    }
                    rdr.Close();
                    return Ok(users);
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

        [HttpPost]
        public HttpResponseMessage post(pessenger p)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (con)
                {
                    string command = "INSERT INTO [passenger](email,fname,lname,age,trip_date)VALUES(@email,@fname,@lname,@age,@trip_date)";
                    cmd = new SqlCommand(command, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@email", p.email);
                    cmd.Parameters.AddWithValue("@fname", p.fname);
                    cmd.Parameters.AddWithValue("@lname", p.lname);
                    cmd.Parameters.AddWithValue("@age", p.age);
                    cmd.Parameters.AddWithValue("@trip_date", p.trip_date);
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
    }
}
