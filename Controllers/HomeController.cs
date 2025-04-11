using System.Diagnostics;
using System.Net.Mail;
using EliteRealEstate.Models;
using Microsoft.AspNetCore.Mvc;

namespace EliteRealEstate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SmtpSettings _smtpSettings;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _smtpSettings = configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [Route("about-us")]
        public IActionResult AboutUs()
        {
            return View();
        }

        [Route("contact-us")]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(ContactUs contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //  Send Email
                    MailMessage msz = new MailMessage
                    {
                        From = new MailAddress(_smtpSettings.FromEmail), // Authenticated email
                        Subject = "Contact Us Form",
                        Body = $"First Name: {contact.FirstName}\n" +
                               $"Last Name: {contact.LastName}\n" +
                               $"Phone Number: {contact.PhoneNumber}\n" +
                               $"Email: {contact.Email}\n" +
                               $"Message: {contact.Message}"
                    };
                    msz.To.Add("hassan.kp222@gmail.com"); // Where mail will be sent 
                    msz.ReplyToList.Add(new MailAddress(contact.Email)); // User's email as the Reply-To address

                    SmtpClient smtp = new SmtpClient
                    {
                        Host = _smtpSettings.Host,
                        Port = _smtpSettings.Port,
                        Credentials = new System.Net.NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                        EnableSsl = _smtpSettings.EnableSsl
                    };

                    smtp.Send(msz);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = $"Sorry, we are facing a problem: {ex.Message}" });
                }
            }

            return View();
        
        }
         


        [Route("off-plan")]
        public IActionResult OffPlan()
        {
            return View();
        }

        [Route("rental")]
        public IActionResult Rental()
        {
            return View();
        }

        [Route("secondary")]
        public IActionResult Secondry()
        {
            return View();
        }


        [Route("property-details")]
        public IActionResult PropertyDetails()
        {
            return View();
        }

        [Route("blog-list")]
        public IActionResult BlogList()
        {
            return View();
        }

        [Route("blog-details")]
        public IActionResult BlogDetails()
        {
            return View();
        }

        [Route("project")]
        public IActionResult project()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
