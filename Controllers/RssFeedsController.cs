using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Projekt_DMP.Controllers
{


    public class RssFeedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RssFeedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var feeds = await _context.RssFeeds.ToListAsync();
            return View(feeds);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rssFeed = await _context.RssFeeds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rssFeed == null)
            {
                return NotFound();
            }

            return View(rssFeed);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create([Bind("Id,Name,Url")] RssFeed rssFeed)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rssFeed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rssFeed);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rssFeed = await _context.RssFeeds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rssFeed == null)
            {
                return NotFound();
            }

            return View(rssFeed);
        }


        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rssFeed = await _context.RssFeeds.FindAsync(id);
            _context.RssFeeds.Remove(rssFeed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
