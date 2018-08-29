using AuctionsWeb.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuctionsWeb.Models
{

    public class SearchViewModel
    {

        [Display(Name = "Auction Name")]
        public string Name { get; set; }

        [Display(Name = "Min Price")]
        public int? MinPrice { get; set; }

        [Display(Name = "Max Price")]
        public int? MaxPrice { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Min Date")]
        public DateTime? MinDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Max Date")]
        public DateTime? MaxDate { get; set; }

        [Display(Name = "State")]
        public AuctionStates? State { get; set; }

        public List<Auction> Auctions { get; set; }

        public string SqlQuery { get; set; }

        public SearchViewModel()
        {
            Auctions = new List<Auction>();
        }
    }        

    
    public class AuctionViewModel
    {
        public Auction Auction { get; set; }
    }

    public class BidModalModel
    {
        public int AuctionId { get; set; }

        public string AuctionName { get; set; }

        [Display(Name = "Last Bid")]
        public decimal? LastBid { get; set; }

        [Display(Name = "New Bid")]
        public decimal NewBid { get; set; }

        public BidModalModel() { }

        public BidModalModel(int auctionId, string auctionName, decimal? lastBid)
        {
            AuctionId = auctionId;
            AuctionName = auctionName;
            LastBid = lastBid;
        }
    }

}