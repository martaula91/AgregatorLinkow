using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorLinkow.Models
{
    public class Plus
    {
        [Key]
        public int Id { get; set; }

        public virtual User User { get; set; }
        public virtual Link Link { get; set; }
    }
}
