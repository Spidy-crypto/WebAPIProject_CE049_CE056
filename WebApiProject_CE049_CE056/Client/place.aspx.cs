using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TourManagementApi.Models;

namespace Client
{
    public partial class place : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        public Tour dest { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (Session["current_user"] == null)
            {
                Response.Redirect("login.aspx");
            }*/

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("https://localhost:44364/");

            int id = Int32.Parse(Request.Cookies["pid"].Value);

            string email = "p@g.com";


            String url = "api/Tour/" + id;

            var res = client.GetAsync(url);
            res.Wait();
            var data = res.Result;
            User u = null;
            if (data.IsSuccessStatusCode)
            {
                var destinations = data.Content.ReadAsAsync<Tour>();
                destinations.Wait();


                dest = destinations.Result;
            }
        }
    }
}