using FleetManagement.Data.Services;
using FleetManagement.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using FleetManagement.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace FleetManagement.Web.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
    
        [HttpGet]
        public ActionResult UserList()
        {
            var users = _userService.GetUsers();

            var um = new UsersViewModel
            {
                Users = users
            };

            return View(um);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddUser(string name, string email, string password, Role role)
        {
            if (ModelState.IsValid)
            {
                var newUser = _userService.Register(name, email, password, role);
                Alert($"User Added Successfully", AlertType.success);

                return RedirectToAction(nameof(UserList));
            }

            return View();
        }

        [HttpGet]
        public ActionResult UserDetails(int id)
        {
            try
            {
                var user = _userService.GetUser(id);

                return View(user);
            }
            catch
            {
                return RedirectToAction(nameof(UserList));
            }
        }

        [HttpGet]
        public ActionResult UpdateUser(int id)
        {
            try
            {
                var user = _userService.GetUser(id);

                return View(user);
            }
            catch
            {
                return RedirectToAction(nameof(UserList));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult UpdateUser(int id, User u)
        {
            if (ModelState.IsValid)
            {
                _userService.UpdateUser(u);
                Alert("User Updated Successfully", AlertType.info);

                return RedirectToAction(nameof(UserList));
            }

            return View(u);
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            var u = _userService.GetUser(id);

            if (u == null)
            {
                Alert($"User {id} not found", AlertType.warning);
                return RedirectToAction(nameof(UserList));
            }

            return View(u);
        }

        public ActionResult DeleteConfirm(int id)
        {
            _userService.DeleteUser(id);

            Alert($"User Deleted Successfully", AlertType.info);

            return RedirectToAction(nameof(UserList));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel m)
        {
            var user = _userService.Authenticate(m.Email, m.Password);

            if (user == null)
            {
                ModelState.AddModelError("Email", "Invalid Login Credentials");
                ModelState.AddModelError("Password", "Invalid Login Credentials");
                return View(m);
            }

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                BuildClaimsPrincipal(user)
            );
            return RedirectToAction("Index", "Home");
        }

        private ClaimsPrincipal BuildClaimsPrincipal(User user)
        {
            // define user claims - you can add as many as required
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            // build principal using claims
            return new ClaimsPrincipal(claims);
        }
    }
}
