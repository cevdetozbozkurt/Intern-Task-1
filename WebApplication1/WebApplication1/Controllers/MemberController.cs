using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class MemberController : Controller
    {
        private readonly SignInManager<Member> _signIn;
        private readonly UserManager<Member> _userManager;
		private readonly IUserRepository _userRepository;
		private readonly ApplicationDbContext _context;
		public MemberController(UserManager<Member> userManager, SignInManager<Member> signIn, IUserRepository userRepository, ApplicationDbContext context)
        {
            _signIn = signIn;
            _userManager = userManager;
			_userRepository = userRepository;
			_context = context;
        }

        public IActionResult Register()
        {
			
			var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
			
			if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(model);
            }

            var member = new Member
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Address = model.Address,
                UserName = model.Email
            };
            var newUserResponse = await _userManager.CreateAsync(member, model.Password);
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(member, UserRoles.User);
				return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
           
        }

		public IActionResult Login()
		{
			
			var response = new LoginViewModel();
			return View(response);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			
			if (!ModelState.IsValid) { return View(model); }
			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user != null)
			{
				var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);

				if (passwordCheck)
				{
					var result = await _signIn.PasswordSignInAsync(user, model.Password, false, false);
					if (result.Succeeded)
					{
						return RedirectToAction("Index", "Home");
					}
					TempData["Error"] = "Wrong password. Please, try again.";
					return View(model);
				}
			}
			//User not found 
			TempData["Error"] = "User not found. Please, try again.";
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			ViewBag.Categories = _context.Categories.ToList();
			await _signIn.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public async Task<IActionResult> Detail()
		{
			
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return RedirectToAction("Detail", "Customer");
			}

			var userDetailViewModel = new EditProductViewModel()
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				Address = user.Address,
			};
			return View(userDetailViewModel);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> EditProfile(string id)
		{
			
			var user = await _userRepository.GetUserById(id);

			if (user == null)
			{
				return View("Error");
			}

			var editMV = new EditProductViewModel()
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				Address = user.Address,
			};
			return View(editMV);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> EditProfile(EditProductViewModel editVM, string id)
		{
			
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Failed to edit profile");
				return View("EditProfile", editVM);
			}

			var user = await _userRepository.GetUserById(id);

			if (user == null)
			{
				return View("Error");
			}

			user.FirstName = editVM.FirstName;
			user.LastName = editVM.LastName;
			user.Address = editVM.Address;
			user.Email = editVM.Email;
			await _userManager.UpdateAsync(user);

			return RedirectToAction("Detail", "Member", new { user.Id });
		}
		[HttpGet("users")]
		public async Task<IActionResult> ListUser()
		{
			
			var users = await _userRepository.GetAllUsers();
			List<UserListViewModel> result = new List<UserListViewModel>();
			foreach (var user in users)
			{
				var userViewModel = new UserListViewModel
				{
					Id = user.Id,
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email,
					Address = user.Address,
				};
				result.Add(userViewModel);
			}

			return View(result);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			
			var userDetail = await _userRepository.GetUserById(id);
			if (userDetail == null) return View("Error");
			return View(userDetail);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteUser(string id)
		{
			
			var userDetail = await _userRepository.GetUserById(id);
			if (userDetail == null) return View("Error");
			_userRepository.Delete(userDetail);
			return RedirectToAction("Users", "Dashboard");
		}

    }
}
