using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class About : BaseEntity
    {
        [StringLength(255)]
        public string IntroImage { get; set; }
        [StringLength(255)]
        public string IntroHeader { get; set; }
        [StringLength(1000)]
        public string IntroText { get; set; }

        [StringLength(255)]
        public string OfferImage { get; set; }
        [StringLength(255)]
        public string OfferHeader { get; set; }
        [StringLength(1000)]
        public string OfferText { get; set; }

        [StringLength(255)]
        public string AboutWineImage { get; set; }
        [StringLength(255)]
        public string AboutWineHeader { get; set; }
        [StringLength(1000)]
        public string AboutWineText { get; set; }
        [StringLength(255)]
        public string StaffHEader { get; set; }

        [NotMapped]
        public IFormFile IntroImageFile { get; set; }
        [NotMapped]
        public IFormFile OfferImageFile { get; set; }
        [NotMapped]
        public IFormFile AboutWineImageFile { get; set; }
    }
}
