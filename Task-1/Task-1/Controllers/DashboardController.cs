using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Task_1.Interfaces;
using Task_1.Models;
using Task_1.Repository;
using Task_1.ViewModels;
using Task_1.VıewModels;

namespace Task_1.Controllers
{
    public class DashboardController : Controller
    {

        private readonly IDashboardRepository _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<Customer> _userManager;
        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor, UserManager<Customer> userManager)
        {
            _repository = dashboardRepository;
            _contextAccessor = httpContextAccessor;
            _userManager = userManager;

        }

		public async Task<IActionResult> Index()
		{
			return View();
		}

		[HttpGet]
        public async Task<IActionResult> EditUserProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }

            var editMV = new EditProfileViewModel()
            {
                Name = user.Name,
                Surname = user.Surname,
                City = user.City,
                State = user.State,
                Street = user.Street,
            };
            return View(editMV);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditProfileViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditProfile", editVM);
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }

            user.Name = editVM.Name;
            user.Surname = editVM.Surname;
            user.City = editVM.City;
            user.State = editVM.State;
            user.Street = editVM.Street;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Detail", "User", new { user.Id });
        }
    }
}

