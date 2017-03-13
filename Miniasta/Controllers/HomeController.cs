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
using Miniasta.Models;

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
            var cookie = Request.Cookies["AccessToken"];
            var token = TempData["accessToken"];
            //"https://api.instagram.com/v1/users/self/follows?access_token=1706565675.81749a0.2c651b4c8a984264bef64c2a005607a5"
            Uri follows = new Uri(" https://api.instagram.com/v1/users/self/follows?access_token=" + token);
            Uri follower = new Uri(" https://api.instagram.com/v1/users/self/followed-by?access_token=" + token);
            Uri requestedby = new Uri(" https://api.instagram.com/v1/users/self/requested-by?access_token=" + token);

            //{
            //    "data": [{
            //        "username": "kevin",
            //                    "profile_picture": "http://images.ak.instagram.com/profiles/profile_3_75sq_1325536697.jpg",
            //                    "full_name": "Kevin Systrom",
            //                    "id": "3"
            //    },
            //    {
            //        "username": "instagram",
            //        "profile_picture": "http://images.ak.instagram.com/profiles/profile_25025320_75sq_1340929272.jpg",
            //        "full_name": "Instagram",
            //        "id": "25025320"
            //    }]
            //}
            //request profile image
            string content = "{ 'data': [{ 'username': 'kevin','profile_picture': 'http://images.ak.instagram.com/profiles/profile_3_75sq_1325536697.jpg','full_name': 'Kevin Systrom','id': '3'},{'username': 'instagram','profile_picture': 'http://images.ak.instagram.com/profiles/profile_25025320_75sq_1340929272.jpg','full_name': 'Instagram','id': '25025320'}]}";

            try
            {
                dynamic result;
                dynamic json;
                using (var webClient = new WebClient())
                {
                    json = webClient.DownloadString(follows);
                    result = JsonConvert.DeserializeObject<InstaUsersVM>(content);
                    ViewBag.follows = result;
                }

                using (var webClient = new WebClient())
                {
                    json = webClient.DownloadString(follower);
                    result = JsonConvert.DeserializeObject<InstaUsersVM>(content);
                    ViewBag.follower = result;
                }

                using (var webClient = new WebClient())
                {
                    json = webClient.DownloadString(requestedby);
                    result = JsonConvert.DeserializeObject<InstaUsersVM>(content);
                    ViewBag.requestedby = requestedby;
                }

            }
            catch
            {

            }
            return View();
        }
    }
}