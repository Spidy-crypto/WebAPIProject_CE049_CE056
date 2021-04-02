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
    public partial class UpdateTour : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        public string placeid;
        Tour t1 = new Tour();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["admin"] == null)
            {
                Response.Redirect("login.aspx");
            }

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("https://localhost:44364/");
            placeid = Request.QueryString["placeid"];
            var res = client.GetAsync("api/tour/" +  int.Parse(placeid));
            res.Wait();
            var data = res.Result;                                                                                                                                                          
            
                var tour = data.Content.ReadAsAsync<Tour>();
                tour.Wait();
                t1 = tour.Result;

            if (!IsPostBack)
            {
                name.Value = t1.name;
                price.Value = t1.price;
                description.Value = t1.desc;
            }
                
                
                
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string imagefile = "";
            if (!FileUpload1.HasFile)
            {
                imagefile = t1.imagepath;
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
            t.price = price.Value;
            t.imagepath = imagefile;

            var res = client.PutAsJsonAsync(url + "/" + int.Parse(placeid),t).Result;
            if (res.IsSuccessStatusCode)
            {
                Response.Redirect("admin.aspx");
            }
        }
    }
}



