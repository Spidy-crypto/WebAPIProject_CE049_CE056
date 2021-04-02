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

    public partial class payment : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        public User[] passengers { get; set; }
        public Tour place { get; set; }
        protected int nofpassenger { get; set; }
        protected int totalamount { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["current_user"] == null)
            {
                Response.Redirect("login.aspx");
            }

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("https://localhost:44364/");

            string email = Session["current_user"].ToString();
            //string email = "p@g.com";
            int placeid;


            if (Request.QueryString["placeid"] != null)
            {
                placeid = Int32.Parse(Request.QueryString["placeid"]);
            }
            else
            {
                placeid = Int32.Parse(Request.Cookies["pid"].Value);
            }
            




            String url = "api/Tour/getpassengers?email=" + email;

            var res = client.GetAsync(url);
            res.Wait();
            var data = res.Result;
            User u = null;
            if (data.IsSuccessStatusCode)
            {
                var users = data.Content.ReadAsAsync<User[]>();
                users.Wait();

                passengers = users.Result;
            }





            url = "api/Tour/" + placeid;

            res = client.GetAsync(url);
            res.Wait();
            data = res.Result;
            u = null;
            if (data.IsSuccessStatusCode)
            {
                var destinations = data.Content.ReadAsAsync<Tour>();
                destinations.Wait();


                place = destinations.Result;
            }



          

            nofpassenger = passengers.Length;
            totalamount = nofpassenger * (Int32.Parse(place.price));

        }
    }
}