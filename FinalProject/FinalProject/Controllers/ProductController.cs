using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.ViewModels.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly RiodeDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        public ProductController(RiodeDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int? id, int colorid = 1, int sizeid = 1)
        {
            bool have = true;
            if (id == null) return BadRequest();
            Product product = await _context.Products
                .Include(p => p.Brend)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id );


            ProductVM productVM = new ProductVM()
            {
                Product = product,
                Reviews = await _context.Reviews.Where(p => p.ProductId == id && !p.IsDeleted).ToListAsync()
            };
            return View(productVM);
        }

        public async Task<IActionResult> AddProdReview(string Message,int star, int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return PartialView("_LoginPartial");
            }
            if (id == null) return View();
            Review review = new Review();
            AppUser appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name && !u.IsAdmin);
            review.Email = appUser.Email;
            review.Name = appUser.UserName;
            ProductVM productVM = new ProductVM()
            {
                Product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id),
                Reviews = await _context.Reviews.Where(p => p.ProductId == id && !p.IsDeleted).ToListAsync()
            };
            if (Message == null || Message == "" || Message.Trim() == null || Message.Trim() == "")
            {
                return PartialView("_AddReviewForProductPartial", productVM);
            }

            review.Message = Message.Trim();
            if (star == 0 || star < 0 || star > 5)
            {
                review.Star = 1;
            }
            else
            {
                review.Star = star;
            }
            review.ProductId = (int)id;
            review.CreatedAt = DateTime.UtcNow.AddHours(4);
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            productVM = new ProductVM()
            {
                Product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id),
                Reviews = await _context.Reviews.Where(p => p.ProductId == id && !p.IsDeleted).ToListAsync()
            };
            return PartialView("_AddReviewForProductPartial", productVM);
        }
        public async Task<IActionResult> Edit(string Message, int? id)
        {
            if (id == null) return BadRequest();
            Review dbReview = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (dbReview == null) return NotFound();

            if (Message != null && Message.Trim() != null && Message.Trim() != "")
            {
                dbReview.Message = Message.Trim();
            }
            dbReview.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();

            ProductVM productVM = new ProductVM()
            {
                Product = await _context.Products.FirstOrDefaultAsync(P=>P.Id == dbReview.ProductId),
                Reviews = await _context.Reviews.Where(p => p.ProductId == dbReview.ProductId && !p.IsDeleted).ToListAsync()
            };
            return PartialView("_AddReviewForProductPartial", productVM);
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
            ProductVM productVM = new ProductVM()
            {
                Product = await _context.Products.FirstOrDefaultAsync(P => P.Id == review.ProductId),
                Reviews = await _context.Reviews.Where(p => p.ProductId == review.ProductId && !p.IsDeleted).ToListAsync()
            };
            return PartialView("_AddReviewForProductPartial", productVM);
        }
        public async Task<int> ProdCommentCount(int? id)
        {
            IEnumerable<Review> reviews = await _context.Reviews.Where(r => !r.IsDeleted && r.ProductId == id).ToListAsync();

            return reviews.Count();
        }
        public async Task<IActionResult> GeneralStar(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = await _context.Products.FirstOrDefaultAsync(p=>p.Id == id),
                Reviews = await _context.Reviews.Where(p => p.ProductId == id && !p.IsDeleted).ToListAsync()
            };
            return PartialView("_ProductReviewStars",productVM);
        }
    }
}
