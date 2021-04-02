using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TourManagementApi.Models;

namespace Client
{
    public partial class destinations : System.Web.UI.Page
    {


        
        HttpClient client = new HttpClient();
        protected Tour[] places { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["current_user"] == null)
            {
                Response.Redirect("login.aspx");
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("https://localhost:44364/");

            //List<Client.ServiceReference2.tplaces = client.getAllPlace();

            if (!Page.IsPostBack)
            {
                String url = "api/Tour/all";

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


                
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string a = Request.Form["placeid"];
            //place.Value = btn.CommandArgument.ToString();
            Response.Write(a);
            Console.WriteLine(a);



        }

        protected void Button1_Command(object sender, CommandEventArgs e)
        {

            string email = Session["current_user"].ToString();
            //client.addToFav(email, Int32.Parse(e.CommandArgument.ToString()));

            //string email = "p@g.com";

            String url = "api/Tour/addtofavourite/" + e.CommandArgument.ToString();

            

            var res = client.PostAsJsonAsync(url,email).Result;
            Console.Out.WriteLine(res.IsSuccessStatusCode);
            if (res.IsSuccessStatusCode)
            {
                
            }
        }


    }
}