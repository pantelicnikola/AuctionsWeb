using AuctionsWeb.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuctionsWeb.Constants
{
    public static class SystemParameters
    {
        public static int DEFAULT_NUMBER_AUCTIONS { get; private set; } = 10;

        public static int TOKEN_VALUE { get; set; } = 10;

        public static Currencies DEFAULT_CURRENCY { get; set; } = Currencies.EUR;

        //[Authorize(Roles = "admin")] // zasto ovo ne radi???????
        //public static void setDEFAULT_NUMBER_AUCTIONS(int value)
        //{
        //    DEFAULT_NUMBER_AUCTIONS = value;
        //}
    }
}