using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Picture { get; set; }
        public bool IsActive { get; set; }
    }
}
