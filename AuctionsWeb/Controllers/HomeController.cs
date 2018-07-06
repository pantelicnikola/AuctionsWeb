

using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AuctionsWeb.Models;
using AuctionsWeb.Enums;
using AuctionsWeb.Constants;

namespace AuctionsWeb.Controllers
{
    public class HomeController : Controller
    { 

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
            //SearchViewModel model = new SearchViewModel();
            //var entity = new auctiondbEntities();
            //model.Auctions = entity.Auctions.Take(10).ToList<Auction>();
            return View(new SearchViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Search(SearchViewModel model)
        {
            var entity = new auctiondbEntities();
            var auctions = entity.Auctions.AsQueryable();
            //if (model.Name == null && model.MinPrice == null && model.MaxDate == null && model.MaxPrice == null && model.MinDate == null && model.State == null)
            //{
            //    auctions = auctions.Where(a => a.State.Equals())
            //}
            if (!(model.Name == null))
            {
                auctions = auctions.Where(a => a.Name.Contains(model.Name));
            }
            if (!(model.MinPrice == null))
            {
                auctions = auctions.Where(a => a.PriceNow.Value > model.MinPrice);
            }
            if (!(model.MaxPrice == null))
            {
                auctions = auctions.Where(a => a.PriceNow.Value < model.MaxPrice);
            }
            if (!(model.MinDate == null))
            {
                auctions = auctions.Where(a => a.TimeCreate > model.MinDate);
            }
            if (!(model.MaxDate == null))
            {
                auctions = auctions.Where(a => a.TimeCreate < model.MaxDate);
            }
            if (!(model.State == null))
            {
                auctions = auctions.Where(a => a.State.Equals(model.State.ToString()));
            }
            if (auctions.Equals(entity.Auctions))
            {
                auctions = auctions.Where(a => a.State.Equals(AuctionStates.OPEN.ToString())).OrderBy(a => a.TimeOpen).Take(SystemParameters.DEFAULT_NUMBER_AUCTIONS);
            }
            model.Auctions = await auctions.ToListAsync();
            return View(model);
        }

        public ActionResult Auction(int id)
        {
            AuctionViewModel model = new AuctionViewModel();
            var entity = new auctiondbEntities();
            model.Auction =  entity.Auctions.Single<Auction>(a => a.Id.Equals(id));

            return View(model);
        }

    }
}