using Assignment3_Torres_JoseDavid.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

            ViewBag.SampleTopics = new List<SelectListItem>
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

            return View(contact);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
