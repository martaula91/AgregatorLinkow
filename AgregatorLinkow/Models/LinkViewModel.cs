using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorLinkow.Models
{
    public class LinkViewModel
    {
        public LinkViewModel()
        {
            Links = new List<LinkExtendedModel>();
        }
        public List<LinkExtendedModel> Links { get; set; }
    }

    public class LinkExtendedModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TotalityOfPluses { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime AddingDate { get; set; }

        public bool CanAddPlus { get; set; }
        public bool Finish { get; set; }
    }
}
