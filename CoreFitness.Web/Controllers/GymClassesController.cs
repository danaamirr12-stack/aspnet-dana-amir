using CoreFitness.Application.Interfaces;
using CoreFitness.Domain.Entities;
using CoreFitness.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Web.Controllers
{
    [Authorize]
    public class GymClassesController : Controller
    {
        private readonly IGymClassRepository _repository;

        public GymClassesController(IGymClassRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var classes = await _repository.GetAllAsync();
            return View(classes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GymClassViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var gymClass = new GymClass
            {
                Name = vm.Name,
                DateTime = vm.DateTime,
                Instructor = vm.Instructor,
                Category = vm.Category,
                MaxCapacity = vm.MaxCapacity
            };

            await _repository.AddAsync(gymClass);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var gymClass = await _repository.GetByIdAsync(id);
            if (gymClass == null) return NotFound();

            var vm = new GymClassViewModel
            {
                Id = gymClass.Id,
                Name = gymClass.Name,
                DateTime = gymClass.DateTime,
                Instructor = gymClass.Instructor,
                Category = gymClass.Category,
                MaxCapacity = gymClass.MaxCapacity
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GymClassViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var gymClass = new GymClass
            {
                Id = vm.Id,
                Name = vm.Name,
                DateTime = vm.DateTime,
                Instructor = vm.Instructor,
                Category = vm.Category,
                MaxCapacity = vm.MaxCapacity
            };

            await _repository.UpdateAsync(gymClass);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var gymClass = await _repository.GetByIdAsync(id);
            if (gymClass == null) return NotFound();
            return View(gymClass);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}