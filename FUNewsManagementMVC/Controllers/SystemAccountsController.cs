using AutoMapper;
using FUNewsManagement.BusinessObjects;
using FUNewsManagement.Services.IServices;
using FUNewsManagementMVC.Authentications;
using FUNewsManagementMVC.Extensions;
using FUNewsManagementMVC.Helpers;
using FUNewsManagementMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FUNewsManagementMVC.Controllers
{
    public class SystemAccountsController : Controller
    {
        // ===========================
        // === Props & Fields
        // ===========================

        private readonly ISystemAccountService _systemAccountService;
        private readonly AdminCredentials _adminCredentials;
        private readonly IMapper _mapper;

        // ===========================
        // === Constructors
        // ===========================

        public SystemAccountsController(ISystemAccountService systemAccountService, AdminCredentials adminCredentials, IMapper mapper)
        {
            _systemAccountService = systemAccountService;
            _adminCredentials = adminCredentials;
            _mapper = mapper;
        }

        // ===========================
        // === Methods
        // ===========================

        // GET: SystemAccounts
        public async Task<IActionResult> Index()
        {
            var accounts = await _systemAccountService.GetAllAccounts();
            var systemAccountVM = _mapper.Map<List<SystemAccount>, List<SystemAccountVM>>(accounts);
            return View(systemAccountVM);
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
                // Check Admin login first
                if (loginVM.AccountEmail.Equals(_adminCredentials.AccountEmail) &&
                    loginVM.AccountPassword.Equals(_adminCredentials.AccountPassword))
                {
                    HttpContext.Session.SetString(AppCts.Session.UserName, _adminCredentials.AccountName);
                    HttpContext.Session.SetInt32(AppCts.Session.UserRole, _adminCredentials.AccountRole);
                    HttpContext.Session.SetInt32(AppCts.Session.UserId, _adminCredentials.AccountId);
                    return RedirectToAction("Index", "NewsArticles");
                }

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
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        // GET: SystemAccounts/Edit/5
        [HttpGet]
        [Authorization(AppCts.Roles.Admin)]
        public async Task<IActionResult> Edit([FromRoute] short id)
        {
            var existingSystemAccount = await _systemAccountService.GetAccountById(id);
            if (existingSystemAccount == null)
            {
                return NotFound();
            }

            SystemAccountVM systemAccountVM = _mapper.Map<SystemAccount, SystemAccountVM>(existingSystemAccount);

            ViewData["AccountRoleId"] = new SelectList(RolesExtensions.GetRoleList(), "Value", "Text");
            return View(systemAccountVM);
        }

        // POST: SystemAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SystemAccountVM systemAccountVM)
        {
            if (ModelState.IsValid)
            {
                // Check existing account, and to prevent update password
                var existingAccount = await _systemAccountService.GetAccountById(systemAccountVM.AccountId);
                if (existingAccount == null)
                {
                    TempData["error"] = "Account is not existed. Please try again.";
                    return NotFound();
                }

                // Update fields partially
                _mapper.Map<SystemAccountVM, SystemAccount>(systemAccountVM, existingAccount);

                if (await _systemAccountService.UpdateSystemAccount(existingAccount))
                {
                    TempData["success"] = "Successfully updated!";
                }
                else
                {
                    TempData["error"] = "Fail to update!";
                }
            }

            ViewData["AccountRoleId"] = new SelectList(RolesExtensions.GetRoleList(), "Value", "Text");
            return View(systemAccountVM);
        }

        // GET: SystemAccounts/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] short id)
        {
            var systemAccount = await _systemAccountService.GetAccountById(id);
            if (systemAccount == null)
            {
                TempData["error"] = "Account is not existed. Please try again.";
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

            return RedirectToAction(nameof(Index));
        }

        // GET: SystemAccounts/Profile
        [Authorization]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = (short)HttpContext.Session.GetInt32(AppCts.Session.UserId);

            // If current user is Admin
            if (userId == int.Parse(AppCts.Roles.Admin))
            {
                return View(_mapper.Map<AdminCredentials, SystemAccountVM>(_adminCredentials));
            }

            // If Staff or Lecturer
            var user = await _systemAccountService.GetAccountById(userId);

            if (user == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<SystemAccount, SystemAccountVM>(user));
        }
    }
}
