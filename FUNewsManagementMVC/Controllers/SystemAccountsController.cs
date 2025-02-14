using FUNewsManagement.BusinessObjects;
using FUNewsManagement.Services.IServices;
using FUNewsManagementMVC.Helpers;
using FUNewsManagementMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace FUNewsManagementMVC.Controllers
{
    public class SystemAccountsController : Controller
    {
        // ===========================
        // === Props & Fields
        // ===========================

        private readonly ISystemAccountService _systemAccountService;

        // ===========================
        // === Constructors
        // ===========================

        public SystemAccountsController(ISystemAccountService systemAccountService)
        {
            _systemAccountService = systemAccountService;
        }

        // ===========================
        // === Methods
        // ===========================

        // GET: SystemAccounts
        public async Task<IActionResult> Index()
        {
            var accounts = await _systemAccountService.GetAllAccounts();
            return View(accounts);
        }

        // GET: SystemAccounts/Login
        public IActionResult Login()
        {
            return View();
        }

        // GET: SystemAccounts/AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }

        // POST: SystemAccounts/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var account = await _systemAccountService.CheckLogin(loginVM.AccountEmail, loginVM.AccountPassword);
                if (account != null)
                {
                    HttpContext.Session.SetString(AppCts.Session.UserName, account.AccountName);
                    HttpContext.Session.SetInt32(AppCts.Session.UserRole, (int)account.AccountRole);
                    HttpContext.Session.SetInt32(AppCts.Session.UserId, account.AccountId);
                    return RedirectToAction("Index", "NewsArticles");
                }
                else
                {
                    TempData["error"] = "Invalid username or password";
                }
            }
            return View(loginVM);
        }

        // GET: SystemAccounts/Login
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        // GET: SystemAccounts/Edit/5
        public async Task<IActionResult> Edit([FromRoute] short id)
        {
            var systemAccount = _systemAccountService.GetAccountById(id);
            if (systemAccount == null)
            {
                return NotFound();
            }
            //ViewData["AccountRoles"] = new SelectList(new List());
            return View(systemAccount);
        }

        // POST: SystemAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SystemAccount systemAccount)
        {
            if (ModelState.IsValid)
            {
                if (await _systemAccountService.UpdateSystemAccount(systemAccount))
                {
                    TempData["success"] = "Successfully updated!";
                }
                else
                {
                    TempData["error"] = "Fail to update!";
                }
            }
            return View(systemAccount);
        }

        // GET: SystemAccounts/DeleteAsync/5
        public async Task<IActionResult> Delete([FromRoute] short id)
        {
            var systemAccount = await _systemAccountService.GetAccountById(id);
            if (systemAccount == null)
            {
                return NotFound();
            }

            if (await _systemAccountService.DeleteSystemAccount(systemAccount))
            {
                TempData["success"] = "Successfully deleted!";
            }
            else
            {
                TempData["error"] = "Fail to delete!";
            }
            ;
            return RedirectToAction(nameof(Index));
        }

        // GET: SystemAccounts/Profile
        [AuthorizationAttribute]
        public async Task<IActionResult> Profile()
        {
            var userId = (short)HttpContext.Session.GetInt32(AppCts.Session.UserId);

            var user = _systemAccountService.GetAccountById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
}
