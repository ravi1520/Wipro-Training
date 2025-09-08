using Microsoft.AspNetCore.Mvc;
using FeedbackPortalApp.Models;

namespace FeedbackPortalApp.Controllers
{
    public class FeedbackController : Controller
    {
        private static List<Feedback> feedbacks = new List<Feedback>();

        public IActionResult Index()
        {
            return View(feedbacks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedbacks.Add(feedback);
                return RedirectToAction("Index");
            }
            return View(feedback);
        }
    }
}
