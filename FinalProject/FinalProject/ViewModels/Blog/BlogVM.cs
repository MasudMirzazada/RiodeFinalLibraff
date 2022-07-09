using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels.Blog
{
    public class BlogVM
    {
        public List<Review> Reviews { get; set; }
        public FinalProject.Models.Blog Blog { get; set; }
        public List<FinalProject.Models.Blog> Blogs { get; set; }
    }
}
