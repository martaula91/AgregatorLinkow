using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorLinkow.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Link> Links { get; set; }
        public virtual ICollection<Plus> Plus { get; set; }
    }
}
