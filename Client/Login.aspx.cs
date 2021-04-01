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
    public partial class Login : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("https://localhost:44364/");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (username.Value == "admin" && password.Value == "admin")
            {
                Session["admin"] = "admin";
                Response.Redirect("admin.aspx");

            }

            String url = "api/auth";

            var res = client.GetAsync(url + "?username=" + username.Value.ToString());
            res.Wait();
            var data = res.Result;
            User u = null;
            if (data.IsSuccessStatusCode)
            {
                var user = data.Content.ReadAsAsync<User>();
                user.Wait();
                u = user.Result;

                if(username.Value == u.email && password.Value == u.password)
                {
                    Session["current_user"] = username.Value;
                    Response.Redirect("destinations.aspx");
                }

            }
        }
    }
}