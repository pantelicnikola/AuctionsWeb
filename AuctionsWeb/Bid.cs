//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AuctionsWeb
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bid
    {
        public int Id { get; set; }
        public Nullable<int> IdAuction { get; set; }
        public string IdUser { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public Nullable<decimal> Amount { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Auction Auction { get; set; }
    }
}
