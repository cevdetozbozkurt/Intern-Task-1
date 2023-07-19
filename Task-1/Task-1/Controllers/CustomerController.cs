using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_1.ViewModels;
using Task_1.Interfaces;
using Task_1.Models;

namespace Task_1.Controllers
{
    public class CustomerController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly UserManager<Customer> _userManager;

        public CustomerController(IUserRepository userRepository, UserManager<Customer> userManager)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Detail()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Detail", "Customer");
            }

            var userDetailViewModel = new CustomerDetailViewModel()
            {
                Id = user.Id,

            };
            return View(userDetailViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
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
                Country = user.Country,
            };
            return View(editMV);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileViewModel editVM)
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
            user.Country = editVM.Country;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Detail", "Customer", new { user.Id });
        }

    }
}
