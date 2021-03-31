using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Net.Http.Headers;
using TourManagementApi.Models;

namespace Client
{
    public partial class Register : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("https://localhost:44364/");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String url = "api/auth";
            User u = new User();
            u.fname = firstname.Value;
            u.lname = lastname.Value;
            u.email = email.Value;
            u.password = password.Value;

            var res = client.PostAsJsonAsync(url, u).Result;
            if (res.IsSuccessStatusCode)
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}