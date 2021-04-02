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
    public partial class Admin : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        protected Tour[] places { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
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
        }

        protected void Button1_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("UpdateTour.aspx?placeid=" + e.CommandArgument.ToString());
        }

        protected void Button2_Command(object sender, CommandEventArgs e)
        {
            //Response.Redirect("delete.aspx?placeid=" + e.CommandArgument.ToString());

            String url = "api/Tour";
            int placeid = Int32.Parse(e.CommandArgument.ToString());
            var deleteTask = client.DeleteAsync(url + "/" + placeid);
            deleteTask.Wait();

            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {
                Response.Redirect("admin.aspx");
            }

        }
    }
}