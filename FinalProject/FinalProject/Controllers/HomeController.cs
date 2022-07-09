using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.ViewModels;
using FinalProject.ViewModels.Basket;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly RiodeDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(RiodeDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddToBasket(int? id, int colorid = 1, int sizeid = 1,double DisPrice = 0,double Price = 0, int count = 1,int StockCount = 0)
        {
            if (id == null) return BadRequest();
            Product dBproduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            if (dBproduct == null) return NotFound();

            //List<Product> products = null;
            List<BasketVM> basketVMs = null;

            string cookie = HttpContext.Request.Cookies["basket"];

            if (cookie != "" && cookie != null)
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookie);
                if (basketVMs.Any(b => b.ProductId == id && b.ColorId == colorid && b.SizeId == sizeid))
                {
                    basketVMs.Find(b => b.ProductId == id && b.ColorId == colorid && b.SizeId == sizeid).Count = count;
                }
                else
                {
                    basketVMs.Add(new BasketVM
                    {
                        ProductId = (int)id,
                        Count = count,
                        Price = Price,
                        DiscountPrice = DisPrice,
                    });
                }
            }
            else
            {
                basketVMs = new List<BasketVM>();

                basketVMs.Add(new BasketVM()
                {
                    ProductId = (int)id,
                    Count = count,
                    Price = Price,
                    DiscountPrice = DisPrice,
                });
            }


            HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketVMs));

            foreach (BasketVM basketVM in basketVMs)
            {
                Product dbProduct = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == basketVM.ProductId);
                basketVM.Image = dbProduct.MainImage;
                basketVM.Name = dbProduct.Name;
                basketVM.ExTax = dbProduct.ExTax;
            }
            cookie = JsonConvert.SerializeObject(basketVMs);


            if (User.Identity.IsAuthenticated && !string.IsNullOrWhiteSpace(cookie))
            {
                AppUser appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name.ToUpper() && !u.IsAdmin);
                List<BasketVM> BasketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookie);
                List<Basket> baskets = new List<Basket>();
                List<Basket> existedBasket = await _context.Baskets.Where(b => b.AppUserId == appUser.Id).ToListAsync();
                for (int i = 0; i < BasketVMs.Count(); i++)
                {
                    if (existedBasket.Any(b => b.ProductId == basketVMs[i].ProductId && b.SizeId == basketVMs[i].SizeId && b.ColorId == basketVMs[i].ColorId))
                    {
                        existedBasket.Find(b => b.ProductId == basketVMs[i].ProductId && b.SizeId == basketVMs[i].SizeId && b.ColorId == basketVMs[i].ColorId).Count = basketVMs[i].Count;
                    }
                    else
                    {
                        Basket basket = new Basket
                        {
                            AppUserId = appUser.Id,
                            ProductId = basketVMs[i].ProductId,
                            Count = basketVMs[i].Count,
                            ColorId = basketVMs[i].ColorId,
                            SizeId = basketVMs[i].SizeId,
                            Price = basketVMs[i].Price,
                            DiscountPrice = basketVMs[i].DiscountPrice,
                            CreatedAt = DateTime.UtcNow.AddHours(4)
                        };

                        baskets.Add(basket);
                    }

                }
                
                await _context.Baskets.AddRangeAsync(baskets);
                await _context.SaveChangesAsync();
            }



            return PartialView("_BasketPartial", basketVMs);
        }

        public async Task<IActionResult> DetailModal(int? id, int? color = 1, int? size = 1, int count = 0)
        {
            if (id == null) return BadRequest();

            Product product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (product == null) return NotFound();

            return PartialView("_ProductModalPartial", product);
        }

        public async Task<int> Count()
        {
            string cookieBasket = HttpContext.Request.Cookies["basket"];

            List<BasketVM> basketVMs = null;

            if (!string.IsNullOrWhiteSpace(cookieBasket))
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookieBasket);
            }
            else
            {
                basketVMs = new List<BasketVM>();
            }

            foreach (BasketVM basketVM in basketVMs)
            {
                Product dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == basketVM.ProductId);
                basketVM.Image = dbProduct.MainImage;
                //basketVM.Price = dbProduct.DiscountPrice > 0 ? dbProduct.DiscountPrice : dbProduct.Price;
                basketVM.Name = dbProduct.Name;
                basketVM.ExTax = dbProduct.ExTax;
            }

            return basketVMs.Count();
        }

        public async Task<IActionResult> SearchInput(string key)
        {
            List<Product> products = new List<Product>();
            if (key != null)
            {
                products = await _context.Products
                .Where(p => p.Name.Contains(key)
                || p.Description.Contains(key)
                || p.Category.Name.Contains(key)
                )
                .ToListAsync();
            }
            return PartialView("_ProductListPartial", products);
        }
    }
}
