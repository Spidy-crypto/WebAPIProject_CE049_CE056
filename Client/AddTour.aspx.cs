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
            if (Session["admin"] == null)
            {
                Response.Redirect("login.aspx");
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("https://localhost:44364/");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string imagefile = "";
            if (!FileUpload1.HasFile)
            {
                imagefile = "destination_1.jpg";
            }
            else
            {
                imagefile = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(@"C:\Users\Dells\Documents\GitHub\WebApiProject_CE049_CE056\Client\images\" + imagefile);

            }
            String url = "api/Tour";


           
            Tour t = new Tour();
            t.name = name.Value;
            t.desc = description.Value;
            t.imagepath = imagefile;
            t.price = price.Value;

            var res = client.PostAsJsonAsync(url, t).Result;
            if (res.IsSuccessStatusCode)
            {
                Response.Redirect("admin.aspx");
            }
        }
    }
}