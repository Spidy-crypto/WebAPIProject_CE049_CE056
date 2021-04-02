using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TourManagementApi.Models;

namespace TourManagementApi.Controllers
{
    public class TourController : ApiController
    {
        SqlConnection con = null;
        SqlCommand cmd = null;

        String ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        
        [HttpGet]
        [Route("api/Tour/{id}")]
        public IHttpActionResult Get(int id)
        {
            Tour t = new Tour();
            try
            {
                con = new SqlConnection();
                con.ConnectionString = this.ConnectionString;
                using (con)
                {
                    string command = "select * from Place where placeid = '" + id + "'";
                    cmd = new SqlCommand(command, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        t.name = rdr["name"].ToString();
                        t.desc = rdr["description"].ToString();
                        t.price = rdr["price"].ToString();
                        t.imagepath = rdr["imagepath"].ToString();
                        t.placeid = id;
                    }
                    rdr.Close();
                    return Ok(t);
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

        

        [HttpGet]
        [Route("api/Tour/all/")]

        public IHttpActionResult getAllPlace()
        {
            List<Tour> users = new List<Tour>();
            Tour u;
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection();
                con.ConnectionString = this.ConnectionString;
                using (con)
                {
                    string command = "select * from Place";
                    cmd = new SqlCommand(command, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        u = new Tour();
                        u.placeid = (int)rdr["placeid"];
                        u.name = rdr["name"].ToString();
                        u.desc = rdr["description"].ToString();
                        u.price = rdr["price"].ToString();
                        u.imagepath = rdr["imagepath"].ToString();
                        users.Add(u);
                    }
                    rdr.Close();
                    System.Console.WriteLine(users);
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

        List<int> fplaces = new List<int>();
        List<Tour> places = new List<Tour>();


        [HttpGet]
        [Route("api/Tour/favourite/")]

        public IHttpActionResult Getfavouriteplaces(string email)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection();
                con.ConnectionString = this.ConnectionString;
                using (con)
                {
                    string command = "select * from Fplace where email = '" + email + "'";
                    cmd = new SqlCommand(command, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        //Label1.Text += rdr["placeid"].ToString();
                        fplaces.Add((int)rdr["placeid"]);


                    }
                    rdr.Close();
                    con.Close();
                    getPlace1();
                    return Ok(places);
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

        void getPlace1()
        {
            List<Tour> users = new List<Tour>();
            Tour u;
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection();
                con.ConnectionString = this.ConnectionString;
                using (con)
                {
                    //Label1.Text += fplaces.Count;
                    for (int i = 0; i < fplaces.Count; i++)
                    {
                        string command = "select * from Place where placeid= '" + fplaces[i] + "'";
                        cmd = new SqlCommand(command, con);
                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            u = new Tour();
                            //Label2.Text += rdr["placeid"];
                            u.placeid = (int)rdr["placeid"];
                            u.name = rdr["name"].ToString();
                            u.desc = rdr["description"].ToString();
                            u.price = rdr["price"].ToString();
                            u.imagepath = rdr["imagepath"].ToString();
                            places.Add(u);
                        }

                        con.Close();
                        System.Console.WriteLine(users);

                    }


                }
            }
            catch (Exception err)
            {

            }
            finally
            {

                con.Close();

            }
        }



        [HttpGet]
        [Route("api/Tour/getpassengers")]


        public IHttpActionResult getpassengers(string email)
        {
            List<User> users = new List<User>();
            User u;
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection();
                con.ConnectionString = this.ConnectionString;
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
        [Route("api/Tour/addtofavourite/{placeid}")]

        public HttpResponseMessage postaddToFav([FromBody]String email, int placeid)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection();
                con.ConnectionString = this.ConnectionString;
                using (con)
                {
                    string command = "INSERT INTO [Fplace](email,placeid)VALUES(@email,@placeid)";
                    cmd = new SqlCommand(command, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@placeid", placeid);
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



        public HttpResponseMessage Post(Tour t)
        {
            try
            {
                con = new SqlConnection();
                con.ConnectionString = this.ConnectionString;
                using (con)
                {
                    string command = "INSERT INTO [Place](name,description,price,imagepath)VALUES(@name,@description,@price,@imagepath)";
                    cmd = new SqlCommand(command, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@name", t.name);
                    cmd.Parameters.AddWithValue("@description", t.desc);
                    cmd.Parameters.AddWithValue("@price", t.price);
                    cmd.Parameters.AddWithValue("@imagepath", t.imagepath);
                    int res = cmd.ExecuteNonQuery();
                    if (res == 1)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Tour added"); 
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

        public HttpResponseMessage Put(int id,Tour t)
        {
            try
            {
                con = new SqlConnection();
                con.ConnectionString = this.ConnectionString;
                using (con)
                {
                    string command = "UPDATE Place set name = @name, description = @description, price = @price, imagepath = @imagepath where placeid='" + id + "' ";
                    cmd = new SqlCommand(command, con);
                    con.Open();
                    cmd.Parameters.Add("@name", t.name);
                    cmd.Parameters.Add("@description", t.desc);
                    cmd.Parameters.Add("@price", t.price);
                    cmd.Parameters.Add("@imagepath", t.imagepath);
                    int res = cmd.ExecuteNonQuery();
                    if (res == 1)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Tour Updated");
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

        // DELETE: api/Tour/5
        public void Delete(int id)
        {
        }


    }
}
