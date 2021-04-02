using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class delete : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        public string placeid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("login.aspx");
            }

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("https://localhost:44364/");
            placeid = Request.QueryString["placeid"];

            String url = "api/Tour";





            var deleteTask = client.DeleteAsync(url+"/" + placeid);
            deleteTask.Wait();

            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {
                Response.Redirect("admin.aspx");
            }
        }
    }
}