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

        public DateTime? TimeEnd { get; set; }

        [Display(Name = "Price Now")]
        public decimal? PriceNow { get; set; }

        [Display(Name = "Bid Amount")]
        public int BidAmount { get; set; }

        public BidModalModel() { }

        public BidModalModel(int auctionId, string auctionName, decimal? priceNow, DateTime? endTime)
        {
            AuctionId = auctionId;
            AuctionName = auctionName;
            PriceNow = priceNow;
            TimeEnd = endTime;
        }
    }

}