using Microsoft.AspNet.Identity;
using MJC_Blogs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MJC_Blogs.Controllers
{
    [RequireHttps]
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

        //GET
        public ActionResult Contact()
        {
            EmailModel model = new EmailModel();
            return View(model);
        }
        
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var body = "<p>Email From: <bold>{0}</bold>" + "({1})</p><p>Subject: {3} Message:</p><p>{2}</p>";
                    //model.Body = "This is a message from your blog site. The name and" + " the email of the contacting person is above.";

                    var svc = new EmailService();
                    var msg = new IdentityMessage()
                    {
                        Subject = model.Subject,
                        Body = string.Format(body, model.FromName, model.FromEmail, model.Body, model.Subject),
                        Destination = "MarkCorumDev@gmail.com"
                    };
                    await svc.SendAsync(msg);
                    return RedirectToAction("Index","Blogs",null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                    return View(model);
                }
            }
            else { return View(model); }
        }
    }
}