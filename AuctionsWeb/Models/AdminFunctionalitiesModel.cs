using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AuctionsWeb.Models
{
    public class OpenAuctionsModel
    {
        public List<Auction> auctions { get; set; } 
    }
}