using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AuctionsWeb.Models
{
    public class CreateAuctionModel
    {
        [Display(Name = "Auction Name")]
        public string AuctionName { get; set; }

        [Display(Name = "Photo URL")]
        public string PhotoURL { get; set; }

        [Display(Name = "Duration")]
        public int Duration{ get; set; }

        [Display(Name = "Price Start")]
        public decimal PriceStart { get; set; }

    }
}