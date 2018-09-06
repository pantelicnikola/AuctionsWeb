using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AuctionsWeb.Models;
using AuctionsWeb.Enums;
using AuctionsWeb.Constants;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using AuctionsWeb.Views.MyHub;
using Microsoft.AspNet.Identity;


namespace AuctionsWeb.Controllers
{
    public class HomeController : Controller
    {

        public IQueryable globalQuery { get; set; }
        public List<Auction> list { get; set; }
        public int auctionID { get; set; } = 0;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpGet]
        public ActionResult Search()
        {
            ViewBag.Message = "Your Search page.";

            HttpCookie aCookie = new HttpCookie("searchInfo");
            aCookie.Values["name"] = "";
            aCookie.Values["minPrice"] = "";
            aCookie.Values["maxPrice"] = "";
            aCookie.Values["minDate"] = "";
            aCookie.Values["maxDate"] = "";
            aCookie.Values["state"] = "";
            //aCookie.Path = "/Application1";
            Response.Cookies.Add(aCookie);

            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel model)
        {

            HttpCookie aCookie = new HttpCookie("searchInfo");
            aCookie.Values["name"] = model.Name;
            aCookie.Values["minPrice"] = model.MinPrice.ToString();
            aCookie.Values["maxPrice"] = model.MaxPrice.ToString();
            aCookie.Values["minDate"] = model.MinDate.ToString();
            aCookie.Values["maxDate"] = model.MaxDate.ToString();
            aCookie.Values["state"] = model.State.ToString();
            //aCookie.Path = "/Application1";
            Response.Cookies.Add(aCookie);
            
            return View();

        }

        public ActionResult InflateCards()
        {
            var auctionsList = new List<Auction>();
            
            var entity = new auctiondbEntities();
            var auctions = entity.Auctions.AsQueryable();
            var cookieValues = Request.Cookies["searchInfo"].Values;

            if (!(Server.HtmlEncode(cookieValues["name"]) == ""))
            {
                string asd = Server.HtmlEncode(cookieValues["name"]);
                auctions = auctions.Where(a => a.Name.Contains(asd));
            }
            if (!(Server.HtmlEncode(cookieValues["minPrice"]) == ""))
            {
                int asd = Int32.Parse(Server.HtmlEncode(cookieValues["minPrice"]));
                auctions = auctions.Where(a => a.PriceNow.Value > asd);
            }
            if (!(Server.HtmlEncode(cookieValues["maxPrice"]) == ""))
            {
                int asd = Int32.Parse(Server.HtmlEncode(cookieValues["maxPrice"]));
                auctions = auctions.Where(a => a.PriceNow.Value < asd);
            }
            if (!(Server.HtmlEncode(cookieValues["minDate"]) == ""))
            {
                DateTime asd = DateTime.Parse(Server.HtmlEncode(cookieValues["minDate"]));
                auctions = auctions.Where(a => a.TimeCreate > asd);
            }
            if (!(Server.HtmlEncode(cookieValues["maxDate"]) == ""))
            {
                DateTime asd = DateTime.Parse(Server.HtmlEncode(cookieValues["maxDate"]));
                auctions = auctions.Where(a => a.TimeCreate < asd);
            }
            if (!(Server.HtmlEncode(cookieValues["state"]) == ""))
            {
                string asd = Server.HtmlEncode(cookieValues["state"]);
                auctions = auctions.Where(a => a.State.Equals(asd));
            }
            if (auctions.Equals(entity.Auctions))
            {
                auctions = auctions.Where(a => a.State.Equals(AuctionStates.OPEN.ToString())).OrderBy(a => a.TimeOpen).Take(SystemParameters.DEFAULT_NUMBER_AUCTIONS);
            }

            auctionsList = auctions.ToList();
            addDependency(entity.Auctions.ToString());

            return PartialView("Cards", auctionsList);
        }


        public void addDependency(string commandText)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(commandText, connection)) 
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.Notification = null;
                    var dependency = new SqlDependency(sqlCommand);
                    dependency.OnChange += (sender, sqlNotificationEvents) =>
                    {
                        if (sqlNotificationEvents.Type == SqlNotificationType.Change)
                        {
                            MyHub.Show();
                        }
                    };
                    sqlCommand.ExecuteReader();
                    
                }
            }
        }

        //public ActionResult Auction(int id)
        //{
        //    return AuctionKurac(id);
        //}

        public ActionResult Auction(int id)
        {
            AuctionViewModel model = new AuctionViewModel();
            var entity = new auctiondbEntities();
            model.Auction =  entity.Auctions.Single<Auction>(a => a.Id.Equals(id));
            return View(model);
        }

        public ActionResult InflateTable(int idAuction)
        {
            var entity = new auctiondbEntities();
            ICollection<Bid> bids = entity.Auctions.Single(a => a.Id.Equals(idAuction)).Bids;
            addDependency(entity.Bids.ToString());

            return PartialView("AuctionTable", bids);
        }

        public ActionResult InflateCard(int idAuction)
        {
            var entity = new auctiondbEntities();
            var auction = entity.Auctions.Single(a => a.Id == idAuction);
            var auctionList = new List<Auction>();
            auctionList.Add(auction);
            addDependency(entity.Auctions.ToString());
            return PartialView("Card", auction);
        }

        [HttpGet]
        public void ModalAction(int auctionId)
        {
            auctionID = auctionId;
            TempData["auctionId"] = auctionId;
            ViewBag.selectedId = "" + auctionId;
            HttpCookie aCookie = new HttpCookie("selectedAuction");
            aCookie.Value = "" + auctionId;
            Response.Cookies.Add(aCookie);

        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateBid(BidModalModel model)
        {
            var db = new auctiondbEntities();

            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            var auctionId = (int)TempData["auctionId"];
            var auction = db.Auctions.First(a => a.Id == auctionId);


            if (model.BidAmount > 0)
            {
                if (user.NumTokens >= model.BidAmount)
                {
                    //if (auction.TimeEnd < DateTime.Now)
                    //{
                    //    ViewBag.ErrorMessage = "This auction has just finished";
                    //}
                    var bid = new Bid()
                    {
                        IdUser = User.Identity.GetUserId(),
                        IdAuction = auctionId,
                        Amount = model.BidAmount,
                        Time = System.DateTime.Now
                    };

                
                    db.Bids.Add(bid);
                    auction.PriceNow += model.BidAmount * SystemParameters.TOKEN_VALUE;

                    user.NumTokens -= model.BidAmount;

                    db.SaveChanges();


                } else
                {
                    ViewBag.ErrorMessage = "Insufficient number of tokens";
                }
                
            } else
            {
                ViewBag.ErrorMessage = "Your bid amount must be a positive number";
            }
            return View("Search");
        }

        public ActionResult InflateTimer()
        {
            return null;
        }

    }

    
}