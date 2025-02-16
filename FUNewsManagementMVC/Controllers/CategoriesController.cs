using FUNewsManagement.BusinessObjects;
using FUNewsManagement.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FUNewsManagementMVC.Controllers
{
    public class CategoriesController : Controller
    {
        // =================================
        // === Fields & Props
        // =================================

        private readonly ICategoryService _categoryService;

        // =================================
        // === Constructors
        // =================================

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // =================================
        // === Actions
        // =================================

        // GET: Categories?searchName=abc
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string? searchName)
        {
            var categories = await _categoryService.GetCategories(searchName);
            return View(categories);
        }

        // GET: Categories/Details/5
        [HttpGet]
        public async Task<IActionResult> Details([FromRoute] short id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["ParentCategoryId"] = new SelectList(await _categoryService.GetCategories(),
                                                            "CategoryId",
                                                            "CategoryName");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.AddCategory(category);
                    TempData["success"] = "Successfully created";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }

            }

            ViewData["ParentCategoryId"] = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        // GET: Categories/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] short id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["ParentCategoryId"] = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateCategory(category);
                    TempData["success"] = "Successfully updated";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }
            }
            ViewData["ParentCategoryId"] = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        // GET: Categories/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] short id)
        {
            try
            {
                await _categoryService.DeleteCategory(id);
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
