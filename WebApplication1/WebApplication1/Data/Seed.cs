using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;

namespace WebApplication1.Data
{
	public class Seed
	{


		//public static void SeedData(IApplicationBuilder applicationBuilder)
		//{
		//	using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
		//	{
		//		var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

		//		context.Database.EnsureCreated();
		//		//Product
		//		if (!context.Products.Any())
		//		{
		//			context.Products.AddRange(new List<Product>()
		//			{
		//				new Product()
		//				{
		//					ProductName = "Product 1",
		//					ProductDescription = "This is a description",
		//					Price = 120.00,
		//					CategoryId = 1
		//				},
		//				new Product()
		//				{
		//					ProductName = "Product 2",
		//					ProductDescription = "This is a description",
		//					Price = 200.00,
		//					CategoryId = 2
		//				}
		//			});
		//			context.SaveChanges();
		//		}
		//		//Category
		//		if (!context.Categories.Any())
		//		{
		//			context.Categories.AddRange(new List<Category>()
		//			{
		//				new Category()
		//				{
		//					CategoryName="Electronics"
		//				},
		//				new Category()
		//				{
		//					CategoryName = "Beatuy and self care"
		//				}
		//			});
		//			context.SaveChanges();
		//		}
		//	}
		//}

		public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				//Roles
				var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
					await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
				if (!await roleManager.RoleExistsAsync(UserRoles.User))
					await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

				////Users
				//var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Customer>>();
				//string adminUserEmail = "test@admin.com";

				//var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
				//if (adminUser == null)
				//{
				//	var newAdminUser = new Customer()
				//	{
				//		UserName = "admin-user",
				//		Name = "Cevdet Eren",
				//		Surname = "Ozbozkurt",
				//		Email = adminUserEmail,
				//		EmailConfirmed = true,
				//		Wallet = new Wallet()
				//		{
				//			WalletBalance = 100000
				//		},
				//		Street = "123 Main St",
				//		City = "Adana",
				//		State = "TR"
				//	};
				//	await userManager.CreateAsync(newAdminUser, "Coding@1234?");
				//	await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
				//}

				//string appUserEmail = "test@user.com";

				//var appUser = await userManager.FindByEmailAsync(appUserEmail);
				//if (appUser == null)
				//{
				//	var newAppUser = new Customer()
				//	{
				//		UserName = "usualuser",
				//		Name = "User",
				//		Surname = "Surname",
				//		Email = appUserEmail,
				//		EmailConfirmed = true,
				//		Wallet = new Wallet(),
				//		Street = "123 Main St",
				//		City = "Charlotte",
				//		State = "NC"

				//	};
				//	await userManager.CreateAsync(newAppUser, "Coding@1234?");
				//	await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
				//}
			}
		}

	}

}
