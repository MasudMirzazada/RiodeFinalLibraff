using FinalProject.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Product : BaseEntity
    {
        [StringLength(255), Required]
        public string Name { get; set; }
        public string MainImage { get; set; }
        public string HoverImage { get; set; }
        [Column(TypeName = "money")]
        public double Price { get; set; }
        [Column(TypeName = "money")]
        public Nullable<double> DiscountPrice { get; set; }
        [Column(TypeName = "money")]
        public double ExTax { get; set; }
        public int Count { get; set; }
        [Required]
        public string Description { get; set; }
        public bool Availability { get; set; }
        public GenderType Gender { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int BrendId { get; set; }
        public Brend Brend { get; set; }

        public bool IsNewArrival { get; set; }
        public bool IsBestseller { get; set; }
        public bool IsFeatured { get; set; }

        public List<ProductImage> ProductImages { get; set; }
        public List<Review> Reviews { get; set; }
        public List<ProductColorSize> ProductColorSizes { get; set; }


        [NotMapped]
        public List<int> TagIds { get; set; } = new List<int>();
        [NotMapped]
        public List<int> ColorIds { get; set; } = new List<int>();
        [NotMapped]
        public List<int> SizeIds { get; set; } = new List<int>();
        [NotMapped]
        public List<int> Counts { get; set; } = new List<int>();
        //[NotMapped]
        //public List<double> Prices { get; set; } = new List<double>();
        //[NotMapped]
        //public List<double> DiscountPrices { get; set; } = new List<double>();


        [NotMapped]
        public IFormFile[] ProductImagesFile { get; set; }
        [NotMapped]
        public IFormFile MainImageFile { get; set; }
        [NotMapped]
        public IFormFile HoverImageFile { get; set; }
    }
}
