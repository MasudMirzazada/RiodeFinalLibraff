using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels.Product
{
    public class ProductVM
    {
        public FinalProject.Models.Product Product { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
