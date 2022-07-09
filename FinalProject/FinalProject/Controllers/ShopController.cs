using FinalProject.DAL;
using FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly RiodeDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ShopController(RiodeDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string sortby,int? catidf=null, int countby = 1,int sizeId=1, int page = 1)
        {
            ViewBag.PageIndex = page;
            ViewBag.Categories = await _context.Categories.Where(p=>p.ParentId == null).ToListAsync();
            ViewBag.SubCategories = await _context.Categories.Where(p=>p.ParentId != null).ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();

            List<Product> products = null;
            List<Product> products2 = new List<Product>();


            if(sizeId != null)
            {

                products = await _context.Products
                    .ToListAsync();
            }
            else
            {
                products = await _context.Products
                    .ToListAsync();
            }

            ViewBag.PageCount = Math.Ceiling((double)products.Count() / 5);
            return View(products.Skip((page - 1) * 5).Take(5));
        }
    }
}
