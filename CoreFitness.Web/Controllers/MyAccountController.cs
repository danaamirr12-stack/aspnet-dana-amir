using CoreFitness.Application.DTOs;
using CoreFitness.Domain.Entities;
using CoreFitness.Infrastructure.Data;
using CoreFitness.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreFitness.Web.Controllers;

[Authorize]
public class MyAccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IWebHostEnvironment _env;
    private readonly ApplicationDbContext _context;

    public MyAccountController(UserManager<ApplicationUser> userManager, IWebHostEnvironment env, ApplicationDbContext context)
    {
        _userManager = userManager;
        _env = env;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction("Login", "Account");

        var dto = new UserProfileDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email ?? string.Empty,
            Phone = user.Phone,
            ProfileImagePath = user.ProfileImagePath
        };

        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Index(UserProfileDto dto, IFormFile? profileImage)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction("Login", "Account");

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Phone = dto.Phone;

        if (profileImage != null && profileImage.Length > 0)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "images", "profiles");
            Directory.CreateDirectory(uploadsFolder);
            var fileName = $"{user.Id}_{profileImage.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await profileImage.CopyToAsync(stream);
            user.ProfileImagePath = $"/images/profiles/{fileName}";
        }

        await _userManager.UpdateAsync(user);
        TempData["Success"] = "Profile updated!";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Membership()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction("Login", "Account");

        var membership = await _context.Memberships
            .FirstOrDefaultAsync(m => m.UserId == user.Id);

        return View(membership);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMembership(string type)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction("Login", "Account");

        var existing = await _context.Memberships
            .FirstOrDefaultAsync(m => m.UserId == user.Id);

        if (existing == null)
        {
            var membership = new Membership
            {
                Type = type,
                StartDate = DateTime.Now,
                IsActive = true,
                UserId = user.Id
            };
            _context.Memberships.Add(membership);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Membership");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveAccount()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
            await _userManager.DeleteAsync(user);

        return RedirectToAction("Index", "Home");
    }
}