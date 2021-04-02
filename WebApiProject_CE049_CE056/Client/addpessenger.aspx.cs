using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using TourManagementApi.Models;

namespace Client
{
    public partial class addpessenger : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("https://localhost:44364/");
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            String url = "api/pessenger";
            pessenger p = new pessenger();
            p.fname = Request.Form["fname"];
            p.lname = Request.Form["lname"];
            p.age = Request.Form["age"];
            p.trip_date = trip_date.Text;
            //string email = Session["current_user"].ToString();
            p.email = "p@gmail.com";

            //client.addpessenger(fname, lname, email, age, date);

            var res = client.PostAsJsonAsync(url, p).Result;
            Console.Out.WriteLine(res.IsSuccessStatusCode);
            if (res.IsSuccessStatusCode)
            {
                Response.Redirect("Login.aspx");
            }
        }



        /*protected void submit_Click1(object sender, EventArgs e)
        {
            String url = "api/pessenger";
            pessenger p = new pessenger();
            p.fname = Request.Form["fname"];
            p.lname = Request.Form["lname"];
            p.age = Request.Form["age"];
            p.trip_date = trip_date.Text;
            //string email = Session["current_user"].ToString();
            p.email = "p@gmail.com";

            //client.addpessenger(fname, lname, email, age, date);

            var res = client.PostAsJsonAsync(url, p).Result;
            if (res.IsSuccessStatusCode)
            {
                //Response.Redirect("Login.aspx");
            }
        }*/
    }
}