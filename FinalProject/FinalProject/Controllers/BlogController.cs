using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.ViewModels.Blog;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly RiodeDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        public BlogController(RiodeDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int? tag,int page = 1)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();
            ViewBag.Blogs = await _context.Blogs.OrderByDescending(p => p.CreatedAt).Take(5).ToListAsync();
            IEnumerable<Blog> blogs = null;
            if (tag == null)
            {
                blogs = await _context.Blogs
                   .OrderByDescending(c => c.CreatedAt)
                   .Include(b => b.BlogTags).ThenInclude(b => b.Tag)
                   .ToListAsync();
            }
            else
            {
                ViewBag.TagId = tag;
                blogs = await _context.Blogs
                   .OrderByDescending(c => c.CreatedAt)
                   .Include(b => b.BlogTags).ThenInclude(b => b.Tag)
                .Where(p => p.BlogTags.Any(p => p.TagId == tag))
                   .ToListAsync();
            }
            ViewBag.PageIndex = page;
            ViewBag.PageCount = Math.Ceiling((double)blogs.Count() / 6);
            return View(blogs.Skip((page - 1) * 6).Take(6));
        }
        public async Task<IActionResult> Detail(int? bid)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();

            ViewBag.Blogs = await _context.Blogs.OrderByDescending(b => b.CreatedAt).Take(4).ToListAsync();

            if (bid == null) return BadRequest();
            Blog blog = await _context.Blogs
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(p => p.Id == (int)bid);
            if (blog == null) return NotFound();

            BlogVM blogVM = new BlogVM()
            {
                Blog = blog,
                Blogs = await _context.Blogs.ToListAsync(),
                Reviews = await _context.Reviews.Where(p=>p.BlogId == blog.Id).OrderByDescending(r=>r.CreatedAt).ToListAsync()
            };

            return View(blogVM);
        }
        public async Task<IActionResult> AddReview(string Message, int? bid)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return PartialView("_LoginPartial");
            }
            if (bid == null) return View();
            Review review = new Review();
            AppUser appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name && !u.IsAdmin);
            review.Email = appUser.Email;
            review.Name = appUser.UserName;
            BlogVM blogVM = new BlogVM()
            {
                Blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == bid),
                Blogs = await _context.Blogs.ToListAsync(),
                Reviews = await _context.Reviews
               .Where(p => p.BlogId == bid)
               .OrderByDescending(r => r.CreatedAt)
               .ToListAsync()
            };
            if (Message == null || Message == "" || Message.Trim() == null || Message.Trim() == "")
            {
                return PartialView("_AddReviewPartial", blogVM);
            }

            review.Message = Message.Trim();
            if (review.Star == null || review.Star < 0 || review.Star > 5)
            {
                review.Star = 1;
            }
            review.BlogId = (int)bid;
            review.CreatedAt = DateTime.UtcNow.AddHours(4);
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            blogVM = new BlogVM()
            {
                Blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == bid),
                Blogs = await _context.Blogs.ToListAsync(),
                Reviews = await _context.Reviews
                .Where(p => p.BlogId == bid)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync()
            };
            return PartialView("_AddReviewPartial", blogVM);
        }
        public async Task<IActionResult> Edit(string Message,int? id)
        {
            if (id == null) return BadRequest();
            Review dbReview = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (dbReview == null) return NotFound();

            if(Message != null && Message.Trim() != null && Message.Trim() != "")
            {
                dbReview.Message = Message.Trim();
            }
            dbReview.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();

            BlogVM blogVM = new BlogVM()
            {
                Blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == dbReview.BlogId),
                Blogs = await _context.Blogs.ToListAsync(),
                Reviews = await _context.Reviews
                .Where(p => p.BlogId == dbReview.BlogId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync()
            };
            return PartialView("_AddReviewPartial", blogVM);
        }
        public async Task<IActionResult> EditComment(int? id)
        {
            if (id == null) return BadRequest();
            Review dbReview = await _context.Reviews
                .Include(r => r.Blog)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (dbReview == null) return NotFound();

            return PartialView("_ReviewEditPartial", dbReview);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            Review review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.Id == id);
            if (review == null) return NotFound();
            review.IsDeleted = true;
            review.DeletedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();
            BlogVM blogVM = new BlogVM()
            {
                Blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == review.BlogId),
                Blogs = await _context.Blogs.ToListAsync(),
                Reviews = await _context.Reviews
                .Where(p => p.BlogId == review.BlogId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync()
            };
            return PartialView("_AddReviewPartial", blogVM);
        }
        public async Task<int> CommentCount(int? id)
        {
            IEnumerable<Review> reviews = await _context.Reviews.Where(r=>!r.IsDeleted && r.BlogId == id).ToListAsync();

            return reviews.Count();
        }
        public async Task<IActionResult> toTag( int? tag,int page = 1)
        {
            ViewBag.PageIndex = page;
            ViewBag.WhichName = await _context.Categories.FirstOrDefaultAsync(c => c.Id == tag);
            ViewBag.WhichName = _context.Categories.FirstOrDefault(c => c.Id == tag).Name;
            List<Blog> blogs = await _context.Blogs
                .Include(p => p.BlogTags)
                .ThenInclude(p => p.Tag)
                .Where(p=>p.BlogTags.Any(p=>p.TagId == tag))
                .ToListAsync();

            ViewBag.PageCount = Math.Ceiling((double)blogs.Count() / 6);

            return RedirectToAction("index",new { tag, page });
        }
    }
}
