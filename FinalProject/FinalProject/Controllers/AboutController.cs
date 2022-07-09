using FinalProject.DAL;
using FinalProject.Models; 
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class AboutController : Controller
    {
        private readonly RiodeDbContext _context;
        public AboutController(RiodeDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            About about = _context.Abouts.FirstOrDefault();
            return View(about);
        }
    }
}
