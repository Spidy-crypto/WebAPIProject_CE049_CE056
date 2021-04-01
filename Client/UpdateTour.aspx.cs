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
        private string placeid;
        protected void Page_Load(object sender, EventArgs e)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("https://localhost:44364/");
            placeid = Request.QueryString["placeid"];
            var res = client.GetAsync("api/tour/" +  int.Parse(placeid));
            res.Wait();
            var data = res.Result;                                                                                                                                                          
            
                var tour = data.Content.ReadAsAsync<Tour>();
                tour.Wait();
                Tour t = tour.Result;

                name.Value = t.name;
                price.Value = t.price;
                description.Value = t.desc;
                
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string imagefile = Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(@"C:\Users\rajka\OneDrive\Documents\GitHub\Tour-Management\Client\images\" + imagefile);
            String url = "api/Tour";

            Tour t = new Tour();
            t.name = name.Value;
            t.desc = description.Value;
            t.price = price.Value;
            t.imagepath = imagefile;

            var res = client.PutAsJsonAsync(url + "?id=" + int.Parse(placeid),t).Result;
            if (res.IsSuccessStatusCode)
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}