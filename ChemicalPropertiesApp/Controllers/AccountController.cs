using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ChemicalPropertiesApp.ViewModels;
using ChemicalPropertiesApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace ChemicalPropertiesApp.Controllers;

public class AccountController : Controller
{
    private MisisContext _misisContext;

    private UsersInfo _currentUser;

    public AccountController(MisisContext context)
    {
        _currentUser = new UsersInfo();
        _misisContext = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Profile()
    {
        _currentUser = _misisContext.UsersInfos.FirstOrDefault(u => u.Login == User.Identity.Name);
        return View(_currentUser);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _misisContext.UsersInfos.FirstOrDefaultAsync(u =>
                u.Login == model.Login && u.Password == model.Password);
            if (user != null)
            {
                await Authenticate(user); // аутентификация
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var user = await _misisContext.UsersInfos.FirstOrDefaultAsync(u => u.Email == model.Email);
        if (user == null)
        {
            _misisContext.UsersInfos.Add(new UsersInfo
            {
                Email = model.Email, Login = model.Login,
                Password = model.Password, FirstName = model.FirstName, LastName = model.LastName, Category = 2, RegDate = DateTime.Now
            });
            await _misisContext.SaveChangesAsync();

            if (user != null) await Authenticate(user);

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "Некорректные логин и(или) пароль");

        return View(model);
    }

    private async Task Authenticate(UsersInfo user)
    {
        var role = user.Category switch
        {
            1 => "privileged",
            2 => "basic",
            _ => "basic"
        };

        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.Login),
            new(ClaimsIdentity.DefaultRoleClaimType, role)
        };
        var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }

    public IActionResult Upgrade()
    {
        return RedirectToAction("Index", "Home");
    }
}