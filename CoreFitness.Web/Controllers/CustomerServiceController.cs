using CoreFitness.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Web.Controllers;

public class CustomerServiceController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(ContactFormDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        TempData["Success"] = "Your message has been sent!";
        return RedirectToAction("Index");
    }
}