using AutoMapper;
using FUNewsManagement.BusinessObjects;
using FUNewsManagement.Services.IServices;
using FUNewsManagementMVC.Helpers;
using FUNewsManagementMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Syncfusion.Pdf.Grid;

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

        // GET: NewsArticles?filterSelect=mine?searchTitle=vukimduy?filterSelect
        [HttpGet]
        public async Task<IActionResult> Index(
            [FromQuery] string filterSelect,
            [FromQuery] string searchTitle)
        {
            ViewBag.CurrentFilter = filterSelect;
            IEnumerable<NewsArticle> newsArticles;
            var userId = HttpContext?.Session?.GetInt32(AppCts.Session.UserId);

            if (userId != null && "mine".Equals(filterSelect))
            {
                newsArticles = await _contextNewsArticle.GetNewsArticlesByCreatedUserId((short)userId);
            }
            else
            {
                newsArticles = await _contextNewsArticle.GetNewsArticles(searchTitle);
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

        // GET: NewsArticles/Delete/5
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

        // GET
        [Authorization(AppCts.Roles.Admin)]
        public async Task<IActionResult> ExportStatistic(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            // Create a new PDF document
            using var document = new Syncfusion.Pdf.PdfDocument();
            var page = document.Pages.Add();
            var graphics = page.Graphics;
            var font = new Syncfusion.Pdf.Graphics.PdfStandardFont(Syncfusion.Pdf.Graphics.PdfFontFamily.Helvetica, 12);

            // Title
            graphics.DrawString("News Articles Report", font, Syncfusion.Pdf.Graphics.PdfBrushes.Black, 200, 20);

            // Create a table
            var pdfGrid = new Syncfusion.Pdf.Grid.PdfGrid();
            pdfGrid.Columns.Add(5); // Define number of columns
            pdfGrid.Headers.Add(1);

            // Add header row
            var header = pdfGrid.Headers[0];
            string[] headers = { "Title", "Headline", "Created Date", "Category", "Status" };

            for (int i = 0; i < headers.Length; i++)
            {
                header.Cells[i].Value = headers[i];
                header.Cells[i].StringFormat = new Syncfusion.Pdf.Graphics.PdfStringFormat()
                {
                    Alignment = Syncfusion.Pdf.Graphics.PdfTextAlignment.Center,
                    LineAlignment = Syncfusion.Pdf.Graphics.PdfVerticalAlignment.Middle
                };

                // Apply header styling
                header.Cells[i].Style.Font = new Syncfusion.Pdf.Graphics.PdfStandardFont(Syncfusion.Pdf.Graphics.PdfFontFamily.Helvetica, 12, Syncfusion.Pdf.Graphics.PdfFontStyle.Bold);
                header.Cells[i].Style.BackgroundBrush = Syncfusion.Pdf.Graphics.PdfBrushes.LightGray;
                header.Cells[i].Style.TextBrush = Syncfusion.Pdf.Graphics.PdfBrushes.Black;
            }

            // Add rows
            var articlesInRange = await _contextNewsArticle.GetNewsArticlesByDateRange(startDate, endDate);
            foreach (var article in articlesInRange)
            {
                PdfGridRow row = pdfGrid.Rows.Add();
                row.Cells[0].Value = article.NewsTitle ?? "Untitled";
                row.Cells[1].Value = article.Headline ?? "No Headline";
                row.Cells[2].Value = article.CreatedDate?.ToString("yyyy-MM-dd") ?? "N/A";
                row.Cells[3].Value = article.Category?.CategoryName ?? "Uncategorized";
                row.Cells[4].Value = article.NewsStatus ? "Active" : "Inactive";

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    row.Cells[i].StringFormat = new Syncfusion.Pdf.Graphics.PdfStringFormat()
                    {
                        Alignment = Syncfusion.Pdf.Graphics.PdfTextAlignment.Left,
                        LineAlignment = Syncfusion.Pdf.Graphics.PdfVerticalAlignment.Middle
                    };

                    // Apply padding & border
                    row.Cells[i].Style.Borders.All = new Syncfusion.Pdf.Graphics.PdfPen(Syncfusion.Pdf.Graphics.PdfBrushes.Black, 0.5f);
                    row.Cells[i].Style.CellPadding = new Syncfusion.Pdf.PdfPaddings(5, 5, 5, 5);
                }
            }

            // Draw the table on PDF
            pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(20, 50));

            // Save PDF to a stream
            // - dont dispose the stream since the file stored in-memmory
            // - EFCore will dispose it automatically when return the response
            var stream = new MemoryStream();

            document.Save(stream);
            document.Close(true);
            stream.Position = 0;

            // Return PDF file
            return File(stream, "application/pdf", "NewsArticlesReport.pdf");
        }
    }
}
