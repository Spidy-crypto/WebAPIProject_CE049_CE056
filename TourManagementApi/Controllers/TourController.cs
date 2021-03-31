using System;
using System.Collections.Generic;
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

        String ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename = C:\USERS\RAJKA\ONEDRIVE\DESKTOP\WEBAPIPROJECT_CE049_CE056\CLIENT\APP_DATA\DATABASE1.MDF;Integrated Security = True";


        // GET: api/Tour
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Tour/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Tour
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
                        return Request.CreateResponse(HttpStatusCode.OK, "Tour added"); ;
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

        // PUT: api/Tour/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Tour/5
        public void Delete(int id)
        {
        }
    }
}
