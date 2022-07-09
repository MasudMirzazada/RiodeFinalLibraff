using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        [StringLength(255)]
        public string Image { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<BlogTag> BlogTags { get; set; }
        public IEnumerable<Review> Reviews { get; set; }


        [NotMapped]
        public List<int> TagIds { get; set; } = new List<int>();
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
