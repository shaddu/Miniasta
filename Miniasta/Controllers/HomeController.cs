using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using Microsoft.Owin.Security;
using Newtonsoft.Json;

namespace Miniasta.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public  ActionResult Insights()
        {
            ViewBag.Message = "Check your latest followers and insights.";
            var token = TempData["accessToken"];
            //"https://api.instagram.com/v1/users/self/follows?access_token=1706565675.81749a0.2c651b4c8a984264bef64c2a005607a5"
            Uri apiRequestUri = new Uri(" https://api.instagram.com/v1/users/self/follows?access_token=" + token);



            //request profile image
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString(apiRequestUri);
                dynamic result = JsonConvert.DeserializeObject(json);
                var userPicture = result.picture;
            }
            return View();
        }
    }
}