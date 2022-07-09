using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.ViewModels.Account;

namespace FinalProject.ViewModels.Account
{
    public class MemberProfileVM
    {
        public MemberUpdateVM Member { get; set; }
        public List<FinalProject.Models.Order> Orders { get; set; }
    }
}
