using System.Diagnostics;

using EliteRealEstate.Models;
using EliteRealEstate.Services;
using Microsoft.AspNetCore.Mvc;

namespace EliteRealEstate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;
        private readonly IApiService _apiService;

        public HomeController(ILogger<HomeController> logger, IEmailService emailService, IApiService apiService)
        {
            _logger = logger;
            _emailService = emailService;
            _apiService = apiService;
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
        [Route("contact-us")]
        public async Task<IActionResult> ContactUs(ContactUs model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid form data!" });
            }
            try
            {
                // Send email
                  await _emailService.SendEmailAsync(model);
                // Send thank you email
                  await _emailService.SendThankYouEmailAsync(model.FirstName, model.Email);

                // Send data to API
                await _apiService.AddContactUs(model);

                return Json(new { success = true, message = "Message sent successfully!" });
            }
            catch (Exception ex) 
            {
                return Json(new { success = false, message = "Error sending message: " + ex.Message });
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Quotation(QuotationModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid form data!" });
            }
            try
            {
                // Send email
                await _emailService.QuotationEmailAsync(model);
                await _emailService.QuotationEmailResponse(model.FullName, model.Email);

                // Send data to API
                await _apiService.AddQuotation(model);

                return Json(new { success = true, message = "Quotation sent successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error sending message: " + ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> LandingPageQuotation(QuotationModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid form data!" });
            }
            try
            {
                // Send email
                await _emailService.QuotationEmailAsync(model);
                await _emailService.QuotationEmailResponse(model.FullName, model.Email);

                // Send data to API
                await _apiService.AddQuotation(model);

                return Json(new { success = true, message = "Quotation sent successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error sending message: " + ex.Message });
            }

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

        [Route("privacy-policy")]
        public IActionResult privacyPolicy()
        {
            return View();
        }

        [Route("term-and-condition")]
        public IActionResult termAndCondition()
        {
            return View();
        }

        public IActionResult project()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> project(ContactUs model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid form data!" });
            }
            try
            {
                // Send email
                await _emailService.SendEmailAsync(model);
                // Send thank you email
                await _emailService.SendThankYouEmailAsync(model.FirstName, model.Email);

                // Send data to API
                await _apiService.AddContactUs(model);

                return Json(new { success = true, message = "Message sent successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error sending message: " + ex.Message });
            }
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
