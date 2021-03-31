using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TourManagementApi.Models;

namespace Client
{
    public partial class AddTour : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("https://localhost:44364/");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String url = "api/Tour";

            string imagefile = Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(@"C:\Users\rajka\OneDrive\Documents\GitHub\Tour-Management\Client\images\" + imagefile);

            Tour t = new Tour();
            t.name = name.Value;
            t.desc = description.Value;
            t.imagepath = imagefile;
            t.price = price.Value;

            var res = client.PostAsJsonAsync(url, t).Result;
            if (res.IsSuccessStatusCode)
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}