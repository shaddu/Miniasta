using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net;
using Newtonsoft.Json;
using Miniasta.Models;
using System.Web;
using System.Security.Claims;
using System.Collections.Generic;

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
        private Microsoft.Owin.Security.IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        [Authorize]
        public ActionResult Insights()
        {
            ViewBag.Message = "Check your latest followers and insights.";
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var token = identity.FindAll("urn:instagram:accesstoken").First().Value;
            if(token == null && User.Identity.GetUserId() != null)
            {
                return RedirectToAction("Index", "Manage");
            }
            Uri follows = new Uri(" https://api.instagram.com/v1/users/self/follows?access_token=" + token);
            Uri follower = new Uri(" https://api.instagram.com/v1/users/self/followed-by?access_token=" + token);
            Uri requestedby = new Uri(" https://api.instagram.com/v1/users/self/requested-by?access_token=" + token);

            string content = "{ 'data': [{ 'username': 'kevin','profile_picture': 'https://scontent.cdninstagram.com/t51.2885-19/10986015_849401375116939_2129532908_a.jpg','full_name': 'Kevin Systrom','id': '3'},{'username': 'instagram','profile_picture': 'https://scontent.cdninstagram.com/t51.2885-19/10986015_849401375116939_2129532908_a.jpg','full_name': 'Instagram','id': '25025320'}]}";

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
                    ViewBag.requestedby = result;
                }

            }
            catch
            {

            }
            return View();
        }
    }
}