using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Services.IService;
using FUNewsManagementMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using FUNewsManagementMVC.Helpers;

namespace FUNewsManagementMVC.Controllers
{
    public class SystemAccountsController : Controller
    {
        private readonly ISystemAccountService _systemAccountService;

        public SystemAccountsController(ISystemAccountService systemAccountService)
        {
            _systemAccountService = systemAccountService;
        }

        // GET: SystemAccounts
        public async Task<IActionResult> Index()
        {
            var accounts = _systemAccountService.GetAllAccounts();
            return View(accounts);
        }

        // GET: SystemAccounts/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: SystemAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
               var account = _systemAccountService.CheckLogin(loginVM.AccountEmail, loginVM.AccountPassword);
                if (account != null)
                {
                    HttpContext.Session.SetString("Username", account.AccountName);
                    HttpContext.Session.SetInt32("UserRole",(int) account.AccountRole);
                    HttpContext.Session.SetInt32("UserId",account.AccountId);
                    return RedirectToAction("Index", "NewsArticles");
                }
                else
                {
                    TempData["error"] = "Invalid username or password";
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction(nameof(Login));
        }

        // GET: SystemAccounts/Edit/5
        public async Task<IActionResult> Edit(short id)
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
                if(_systemAccountService.UpdateSystemAccount(systemAccount))
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

        // GET: SystemAccounts/Delete/5
        public async Task<IActionResult> Delete(short id)
        {
            var systemAccount = _systemAccountService.GetAccountById(id);
            if (systemAccount == null)
            {
                return NotFound();
            }

            if (_systemAccountService.DeleteSystemAccount(systemAccount))
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

        [AuthorizationAttribute]
        public async Task<IActionResult> Profile()
        {
            var userId = (short) HttpContext.Session.GetInt32("UserId");
            
            var user = _systemAccountService.GetAccountById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);  
        }
    }
}
