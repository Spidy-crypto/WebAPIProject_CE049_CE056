using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Net.Http;
using TourManagementApi.Models;
using System.Net.Http.Headers;

namespace Client
{
    public partial class favourite : System.Web.UI.Page
    {
        List<int> fplaces = new List<int>();

        HttpClient client = new HttpClient();
        
        Tour[] places;
        //List<Client.ServiceReference2.tour> places = new List<ServiceReference2.tour>();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["current_user"] == null)
            {
                Response.Redirect("login.aspx");
            }

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("https://localhost:44364/");
            

            if (!IsPostBack)
            {
                string email = Session["current_user"].ToString();

                //string email = "p@g.com";
                

                String url = "api/Tour/favourite"+"?email="+email;

                var res = client.GetAsync(url);
                res.Wait();
                var data = res.Result;
                User u = null;
                if (data.IsSuccessStatusCode)
                {
                    var destinations = data.Content.ReadAsAsync<Tour[]>();
                    destinations.Wait();


                    Repeater1.DataSource = destinations.Result;
                    Repeater1.DataBind();


                }

                //Label1.Text = client.getFPlace(email).Length.ToString();
            }



        }

        





        protected void Button1_Click(object sender, EventArgs e)
        {




        }

        protected void Button1_Command(object sender, CommandEventArgs e)
        {


        }

        protected void ImageButton2_Command(object sender, CommandEventArgs e)
        {
            string placeid = e.CommandArgument.ToString();
            HttpCookie cookie = new HttpCookie("pid", placeid);
            Response.Cookies.Add(cookie);
            Response.Redirect("place.aspx");
        }
    }
}