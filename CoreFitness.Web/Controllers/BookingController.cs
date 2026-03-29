using CoreFitness.Application.Interfaces;
using CoreFitness.Domain.Entities;
using CoreFitness.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Web.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IGymClassRepository _gymClassRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingController(
            IBookingRepository bookingRepository,
            IGymClassRepository gymClassRepository,
            UserManager<ApplicationUser> userManager)
        {
            _bookingRepository = bookingRepository;
            _gymClassRepository = gymClassRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyBookings()
        {
            var userId = _userManager.GetUserId(User);
            var bookings = await _bookingRepository.GetByUserIdAsync(userId!);
            return View(bookings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(int gymClassId)
        {
            var userId = _userManager.GetUserId(User);
            var exists = await _bookingRepository.ExistsAsync(userId!, gymClassId);

            if (!exists)
            {
                var booking = new Booking
                {
                    UserId = userId!,
                    GymClassId = gymClassId,
                    BookedAt = DateTime.Now
                };
                await _bookingRepository.AddAsync(booking);
            }

            return RedirectToAction("Index", "GymClasses");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int gymClassId)
        {
            var userId = _userManager.GetUserId(User);
            var booking = await _bookingRepository.GetByUserAndClassAsync(userId!, gymClassId);

            if (booking != null)
                await _bookingRepository.DeleteAsync(booking.Id);

            return RedirectToAction(nameof(MyBookings));
        }
    }
}