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
