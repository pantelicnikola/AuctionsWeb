using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuctionsWeb.Enums
{
    public enum AuctionStates
    {
        READY,
        OPEN,
        CLOSED
    }

    public enum Currencies
    {
        DIN,
        EUR,
        USD
    }

    public enum OrderStates
    {
        SUBMITTED,
        COMPLETED,
        CANCELED
    }
}