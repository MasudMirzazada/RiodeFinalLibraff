using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class ProductColorSize
    {
        public int Id { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Product Product { get; set; }
        public Nullable<int> ColorId { get; set; }
        public Color Color { get; set; }
        public Nullable<int> SizeId { get; set; }
        public Size Size { get; set; }
        public int Count { get; set; }

        //[Column(TypeName = "money")]
        //public double Price { get; set; }
        //[Column(TypeName = "money")]
        //public double DiscountPrice { get; set; }

        public int StockCount { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
