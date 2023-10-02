using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Projekt_DMP.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Akce pro zobrazení seznamu článků v RSS feedu
        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rssFeed = _context.RssFeeds.Include(r => r.Articles).FirstOrDefault(r => r.Id == id);

            if (rssFeed == null)
            {
                return NotFound();
            }

            return View(rssFeed);
        }

        // Akce pro filtrování článků podle data
        public IActionResult Filter(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filterViewModel = new FilterViewModel
            {
                RssFeedId = id.Value
            };

            return View(filterViewModel);
        }

        // POST: Articles/Filter
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Filter([Bind("RssFeedId,FromDate,ToDate")] FilterViewModel filterViewModel)
        {
            if (ModelState.IsValid)
            {
                var articles = _context.Articles
                    .Where(a => a.RssFeedId == filterViewModel.RssFeedId &&
                                a.PublishDate >= filterViewModel.FromDate &&
                                a.PublishDate <= filterViewModel.ToDate)
                    .ToList();

                return View("FilteredArticles", articles);
            }

            return View(filterViewModel);
        }

        // Akce pro obnovení článků v RSS feedu
        public IActionResult Reload(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rssFeed = _context.RssFeeds.FirstOrDefault(r => r.Id == id);

            if (rssFeed == null)
            {
                return NotFound();
            }

            var updatedArticles = YourRssFeedUpdateMethod(rssFeed.Url);

            foreach (var updatedArticle in updatedArticles)
            {
                var existingArticle = _context.Articles.FirstOrDefault(a => a.Title == updatedArticle.Title && a.RssFeedId == rssFeed.Id);

                if (existingArticle == null)
                {
                    updatedArticle.RssFeedId = rssFeed.Id;
                    _context.Articles.Add(updatedArticle);
                }
                else
                {
                    existingArticle.Content = updatedArticle.Content;
                    existingArticle.PublishDate = updatedArticle.PublishDate;
                }
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { id });
        }
    }
}