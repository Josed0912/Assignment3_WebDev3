using Assignment3_Torres_JoseDavid.Models;
using Assignment3_Torres_JoseDavid.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3_Torres_JoseDavid.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;

        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IEmailSender emailSender)
        {
            _logger = logger;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("contact")]

        public IActionResult Contact()
        {
            ViewBag.Header = "Contact Us Today";

            ContactModel contact = new ContactModel();

            contact.CreateDate = DateTime.Now;

            ViewBag.SampleTopics = GetSampleTopics();

            ViewBag.ReCaptchaSiteKey = _configuration["GoogleReCAPTCHA:SiteKey"];

            if (TempData.ContainsKey("SelectedTopic"))
            {
                contact.Topic = TempData["SelectedTopic"].ToString();
            }

            return View(contact);
        }

        [HttpPost("contact")]
        public async Task<IActionResult> Contact(ContactModel contact, [FromServices] ReCaptchaValidationService reCaptchaValidationService)
        {
            var recaptchaResponse = Request.Form["g-recaptcha-response"];

            var isReCaptchaValid = await reCaptchaValidationService.IsReCaptchaValid(recaptchaResponse);

            if (!isReCaptchaValid)
            {
                ModelState.AddModelError("ReCaptcha", "reCAPTCHA validation failed");

                TempData["SelectedTopic"] = contact.Topic;

                ViewBag.SampleTopics = GetSampleTopics();
                ViewBag.ReCaptchaSiteKey = _configuration["GoogleReCAPTCHA:SiteKey"];

                return View(contact);
            }

            if (ModelState.IsValid)
            {
                TempData.Remove("SelectedTopic");

                (_emailSender as SendGridEmailSender).Contact = contact; 

                await _emailSender.SendEmailAsync(contact.Email, contact.Topic, contact.Comments);

                return View("Success", contact);
            }
            else
            {
                return View();
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<SelectListItem> GetSampleTopics()
        {
            return new List<SelectListItem>
            {
            new SelectListItem{
                Text= "My order",
                Value = "My order"
            },
            new SelectListItem{
                Text= "Feedback",
                Value ="Feedback"
            },
             new SelectListItem{
                Text= "Product questions",
                Value ="Product questions"
            },
              new SelectListItem{
                Text= "Customer service and feedback",
                Value ="Customer service and feedback"
            },
               new SelectListItem{
                Text= "Technical questions, specifications, geometry, sizing and historical information",
                Value ="Technical questions, specifications, geometry, sizing and historical information"
            },
            new SelectListItem{
                Text= "Warranty",
                Value ="Warranty"
            },
            new SelectListItem
            {
                Text="Registration",
                Value="Registration"
            },
            new SelectListItem
            {
                Text="Catalogue requests",
                Value="Catalogue requests"
            },
            new SelectListItem
            {
                Text="Owner's manuals",
                Value="owner's manuals"
            },
            new SelectListItem
            {
                Text="Media inquiries",
                Value= "Media inquiries"
            },
            new SelectListItem
            {
                Text="Sponsorship and donations",
                Value="Sponsorship and donations"
            } };
        }
    }
}
