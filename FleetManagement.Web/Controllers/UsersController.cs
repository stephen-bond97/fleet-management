using FleetManagement.Data.Models;
using FleetManagement.Data.Services;
using FleetManagement.Web.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize(Roles = "admin, manager")]
        public ActionResult AddUser()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                var newUser = _userService.Register(user.FirstName, user.LastName, user.Email, user.Password, user.Role);
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
        [Authorize(Roles = "admin, manager")]
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
        [Authorize(Roles = "admin, manager")]
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
        [Authorize(Roles = "admin, manager")]
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

        [HttpGet]
        [Authorize(Roles = "admin, manager")]
        public ActionResult DeleteConfirm(int id)
        {
            _userService.DeleteUser(id);

            Alert($"User Deleted Successfully", AlertType.info);

            return RedirectToAction(nameof(UserList));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserLoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel m)
        {
            var user = _userService.Authenticate(m.Email, m.Password);

            // if no user was found with the given email and password combination, add errors to the model
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

            // once user has logged in, redirect to the homepage
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserRegisterViewModel());
        }

        [HttpPost]
        public IActionResult Register(UserRegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                // all users are registered as guests initially, admins can change their role if required
                var newUser = _userService.Register(user.FirstName, user.LastName, user.Email, user.Password, Role.Guest);
                Alert($"User Registered Successfully", AlertType.success);

                return RedirectToAction(nameof(Login));
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult ErrorNotAuthorised()
        {
            return View();
        }

        private ClaimsPrincipal BuildClaimsPrincipal(User user)
        {
            // define user claims - you can add as many as required
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            // build principal using claims
            return new ClaimsPrincipal(claims);
        }
    }
}
