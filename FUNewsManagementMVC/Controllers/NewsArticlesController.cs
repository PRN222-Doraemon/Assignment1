using AutoMapper;
using FUNewsManagement.BusinessObjects;
using FUNewsManagement.Services.IServices;
using FUNewsManagementMVC.Helpers;
using FUNewsManagementMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FUNewsManagementMVC.Controllers
{
    public class NewsArticlesController : Controller
    {
        // ===========================
        // === Props & Fields
        // ===========================

        private readonly INewsArticleService _contextNewsArticle;
        private readonly ISystemAccountService _contextSystemAccount;
        private readonly ICategoryService _contextCategory;
        private readonly ITagService _contextTag;
        private readonly IMapper _mapper;
        private readonly INewsTagService _contextNewsTag;

        // ===========================
        // === Constructors
        // ===========================

        public NewsArticlesController(INewsArticleService contextNewsArticle, ISystemAccountService contextSystemAccount,
            ICategoryService contextCategory, ITagService contextTag, IMapper mapper, INewsTagService contextNewsTag)
        {
            _contextCategory = contextCategory;
            _contextNewsArticle = contextNewsArticle;
            _contextSystemAccount = contextSystemAccount;
            _contextTag = contextTag;
            _mapper = mapper;
            _contextNewsTag = contextNewsTag;
        }

        // ===========================
        // === Methods
        // ===========================

        // GET: NewsArticles?filterSelect=mine?searchName=vukimduy
        [HttpGet]
        public async Task<IActionResult> Index(
            [FromQuery] string filterSelect,
            [FromQuery] string searchName)
        {
            ViewBag.CurrentFilter = filterSelect;
            IEnumerable<NewsArticle> newsArticles;
            var userId = HttpContext?.Session?.GetInt32(AppCts.Session.UserId);

            if (userId != null)
            {
                switch (filterSelect)
                {
                    case "all":
                        newsArticles = await _contextNewsArticle.GetNewsArticles(searchName);
                        break;
                    case "mine":
                        newsArticles = await _contextNewsArticle.GetNewsArticlesByCreatedUserId((short)userId);
                        break;
                    default:
                        newsArticles = await _contextNewsArticle.GetNewsArticles(searchName);
                        break;
                }
            }
            else
            {
                newsArticles = await _contextNewsArticle.GetNewsArticles();
            }

            return View(newsArticles);
        }

        // GET: NewsArticles/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var newsArticle = await _contextNewsArticle.GetNewsArticleById(id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        // GET: NewsArticles/Create
        [Authorization(AppCts.Roles.Staff)]
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _contextCategory.GetCategories(), "CategoryId", "CategoryName");
            ViewData["TagId"] = new SelectList(await _contextTag.GetAllTags(), "TagId", "TagName");
            return View();
        }

        // POST: NewsArticles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorization(AppCts.Roles.Staff)]
        public async Task<IActionResult> Create(NewsArticleVM newsArticleVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newsArticle = _mapper.Map<NewsArticle>(newsArticleVM);
                    newsArticle.CreatedById = (short?)HttpContext.Session.GetInt32(AppCts.Session.UserId);
                    newsArticle.UpdatedById = (short?)HttpContext.Session.GetInt32(AppCts.Session.UserId);


                    await _contextNewsArticle.AddNewsArticle(newsArticle);

                    if (newsArticleVM.TagIds != null && newsArticleVM.TagIds.Any())
                    {
                        await _contextNewsTag.AddNewsTag(newsArticleVM.TagIds, newsArticle.NewsArticleId);
                    }

                    TempData["success"] = "Successfully added news article!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }

            }
            ViewData["CategoryId"] = new SelectList(await _contextCategory.GetCategories(), "CategoryId", "CategoryName", newsArticleVM.CategoryId);
            ViewData["TagId"] = new MultiSelectList(await _contextTag.GetAllTags(), "TagId", "TagName", newsArticleVM.TagIds);
            return View(newsArticleVM);
        }

        // GET: NewsArticles/Edit/5
        [Authorization(AppCts.Roles.Staff)]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var newsArticle = await _contextNewsArticle.GetNewsArticleById(id);
            var newsArticleVM = _mapper.Map<NewsArticleVM>(newsArticle);

            if (newsArticle == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _contextCategory.GetCategories(), "CategoryId", "CategoryName", newsArticle.CategoryId);
            ViewData["TagId"] = new MultiSelectList(await _contextTag.GetAllTags(), "TagId", "TagName", newsArticleVM.TagIds);
            return View(newsArticleVM);
        }

        // POST: NewsArticles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorization(AppCts.Roles.Staff)]
        public async Task<IActionResult> Edit(NewsArticleVM newsArticleVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newsArticle = _mapper.Map<NewsArticle>(newsArticleVM);
                    newsArticle.UpdatedById = (short)HttpContext.Session.GetInt32("UserId");
                    await _contextNewsArticle.UpdateNewsArticle(newsArticle);

                    if (newsArticleVM.TagIds != null && newsArticleVM.TagIds.Any())
                    {
                        await _contextNewsTag.UpdateNewsTags(newsArticleVM.TagIds, newsArticle.NewsArticleId);
                    }

                    TempData["success"] = "Successfully updated news article!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }

            }
            ViewData["CategoryId"] = new SelectList(await _contextCategory.GetCategories(), "CategoryId", "CategoryName", newsArticleVM.CategoryId);
            return View(newsArticleVM);
        }

        // GET: NewsArticles/DeleteAsync/5
        [Authorization(AppCts.Roles.Staff)]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                await _contextNewsArticle.DeleteNewsArticle(id);
                TempData["success"] = "Successfully deleted";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
